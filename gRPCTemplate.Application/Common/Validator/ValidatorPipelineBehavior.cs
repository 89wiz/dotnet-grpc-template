using FluentValidation;
using gRPCTemplate.Domain.Common;
using MediatR;

namespace gRPCTemplate.Application.Common.Validator;

public class ValidatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidatorPipelineBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResult().AddFluentResult(_validator.Validate(request));

        //if (!validationResult.IsValid)
        //    return validationResult;

        return await next();
    }
}
