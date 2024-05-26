using gRPCTemplate.Application.Common;
using gRPCTemplate.Domain.Common;
using OneOf;

namespace gRPCTemplate.Application.User.Register;

public class RegisterHandler : ICommandHandler<RegisterCommand, RegisterResult>
{
    private readonly IUnitOfWork unitOfWork;

    public RegisterHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<OneOf<RegisterResult, ErrorMessage>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransaction(cancellationToken);

        if (!command.Password.Equals(command.ConfirmPassword))
            return new ErrorMessage { ErrorCode = 2, Message = "Password didn't match" };

        var entity = command.AsEntity();
        entity.Id = Guid.NewGuid();

        entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);

        try
        {
            await unitOfWork.DbSet<Domain.Entities.User>().AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            await unitOfWork.Rollback(cancellationToken);
            return new ErrorMessage
                {
                    ErrorCode = 3,
                    Message = "Error saving user",
                    ErrorDetails =
                    [
                        new ErrorMessage { Message = ex.Message }
                    ]
                };
        }

        await unitOfWork.Commit(cancellationToken);
        return entity.AsResult();
    }
}
