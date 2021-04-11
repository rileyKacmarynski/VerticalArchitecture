﻿using Application.Shared.ResultType;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Shared.Pipeline
{
    public class ValidationBehavior<TUseCase, TResult> : IPipelineBehavior<TUseCase, TResult>
        where TResult : Result<TResult>
    {
        private readonly IEnumerable<IValidator<TUseCase>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TUseCase>> validators)
        {
            _validators = validators;
        }

        public Task<TResult> Handle(TUseCase useCase, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
        {
            var errors = _validators
                .Select(v => v.Validate(useCase))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!errors.Any())
            {
                return next();
            }

            var errorTuples = errors
                .Select(e => (e.PropertyName, e.ErrorMessage));

            return Result.Invalid<TResult>(errorTuples).AsTask<TResult>();
        }
    }
}
