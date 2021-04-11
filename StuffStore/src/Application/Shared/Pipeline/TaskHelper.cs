using Application.Shared.ResultType;
using System.Threading.Tasks;

namespace Application.Shared.Pipeline
{
    public static class TaskHelper
    {
        public static Task<TResult> AsTask<TResult>(this Result result) where TResult : class
        {
            return Task.FromResult(result as TResult);
        }
    }
}
