using Application.Shared.ResultType;

namespace IntegrationTests
{
    public static class ResultHelper
    {
        public static TValue Get<TValue>(this Result<TValue> result) where TValue : new()
        {
            return result.GetValue(r => r, error => new TValue());
        }
    }
}
