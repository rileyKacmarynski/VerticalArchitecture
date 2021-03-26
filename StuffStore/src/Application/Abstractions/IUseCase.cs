using Application.Shared.ResultType;
using MediatR;

namespace Application.Abstractions
{
    public interface IUseCase : IRequest<Result> { }

    public interface IUseCase<TResult> : IRequest<Result<TResult>> { }

    public interface IUseCaseHandler<TUseCase, TResult> : IRequestHandler<TUseCase, Result<TResult>> 
        where TUseCase : IUseCase<TResult> { }

    public interface IUseCaseHandler<TUseCase> :
        IRequestHandler<TUseCase, Result> where TUseCase : IUseCase { }
}
