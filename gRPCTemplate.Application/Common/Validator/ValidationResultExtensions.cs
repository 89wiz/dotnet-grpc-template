﻿using gRPCTemplate.Domain.Common;
using static gRPCTemplate.Domain.Common.ValidationResult;

namespace gRPCTemplate.Application.Common.Validator;

internal static class ValidationResultExtensions
{
    internal static ValidationResult AddFluentResult(this ValidationResult validationResult, FluentValidation.Results.ValidationResult fluentValidationResult)
    {
        if (validationResult.IsValid) return validationResult;

        validationResult.ErrorCode = ValidationErrorCode.InvalidEntity;
        validationResult.Errors.AddRange(fluentValidationResult.Errors.Select(e => new ValidationError($"{e.PropertyName}: {e.ErrorMessage}")));

        return validationResult;
    }
}
