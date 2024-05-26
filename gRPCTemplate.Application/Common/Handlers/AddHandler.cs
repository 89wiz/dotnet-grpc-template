using gRPCTemplate.Application.Common;
using gRPCTemplate.Domain.Common;
using gRPCTemplate.Domain.Entities.Common;
using MediatR;

namespace gRPCTemplate.Application._Common.Handlers;

public abstract class AddHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class, new()
    where TEntity : class, IEntity, new()
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ValidationResult _validationResult;

    public AddHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validationResult = new ValidationResult();
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransaction(cancellationToken);

        var entity = _mapper.Map<TEntity>(request);
        entity.Id = Guid.NewGuid();
        await _unitOfWork.DbSet<TEntity>().AddAsync(entity, cancellationToken);

        if (!_validationResult.IsValid)
        {
            await _unitOfWork.Rollback(cancellationToken);
            return _validationResult;
        }
        await _unitOfWork.Commit(cancellationToken);
        return _mapper.Map<TResponse>(entity);
    }
}
