using FluentValidation;

namespace gRPCTemplate.Application.Common.Validator;

public class DummyValidator<TRequest> : AbstractValidator<TRequest>, IValidator<TRequest>
{
    public DummyValidator() { }
}
