using Application.Shared.ResultType;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Shared.Pipeline
{
    public class ErrorHandlingBehavior<TUseCase, TResult> : IPipelineBehavior<TUseCase, TResult>
        where TResult : class
    {
        public Task<TResult> Handle(TUseCase request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
        {
            try
            {
                return next();
            }
            // add more specific error catching here
            catch(Exception ex)
            {
                return Task.FromResult(Result.Fail<TResult>(ex.Message) as TResult);
            }
        }
    }
}
