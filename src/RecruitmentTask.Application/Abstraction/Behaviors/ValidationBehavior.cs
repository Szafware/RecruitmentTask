﻿using RecruitmentTask.Application.Abstraction.Messaging;
using RecruitmentTask.Application.Exceptions;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.Abstraction.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationContext = new ValidationContext<TRequest>(request);

        var validationErrors = _validators.Select(v => v.Validate(validationContext))
                                          .Where(vr => vr.Errors.Any())
                                          .SelectMany(vr => vr.Errors)
                                          .Select(vf => new ValidationError(vf.PropertyName, vf.ErrorMessage))
                                          .ToList();

        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        return await next();
    }
}
