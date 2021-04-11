using Application.Shared.Exceptions;
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
            catch(EntityNotFoundException ex)
            {
                // todo: test if I need TResult here
                return Result.NotFound<TResult>(ex.Message).AsTask<TResult>();
            }
            catch(Exception ex)
            {
                return Result.Fail<TResult>(ex.Message).AsTask<TResult>();
            }
        }
    }
}
