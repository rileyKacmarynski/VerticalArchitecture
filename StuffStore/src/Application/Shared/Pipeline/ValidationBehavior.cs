using Application.Shared.ResultType;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Shared.Pipeline
{
    public class ValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
        where TResult : Result<TResult>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResult> Handle(TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResult> next)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (!errors.Any())
            {
                return next();
            }

            throw new ValidationException(errors);
        }
    }
}
