using Polly;
using System.Net;

namespace Infrastructure.Services.YourExternal
{
    public class YourExternalServicePolicies
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="onRetryAsync">Parameterized to assist with unit testing</param>
        /// <returns></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(Func<DelegateResult<HttpResponseMessage>, TimeSpan, int, Context, Task> onRetryAsync)
        {
            return Policy
            .HandleResult<HttpResponseMessage>(r =>
                r.StatusCode == HttpStatusCode.TooManyRequests && r.Headers.RetryAfter != null)
            .WaitAndRetryAsync(3,
                sleepDurationProvider: (_, result, _) => result.Result.Headers.RetryAfter.Delta.Value,
                onRetryAsync: onRetryAsync);
        }
        
    }
}
