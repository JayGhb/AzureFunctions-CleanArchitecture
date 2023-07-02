using Polly;
using System.Net;

namespace Infrastructure.Services.DynamicsCrm
{
    public class DynamicsCrmServicePolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy
            .HandleResult<HttpResponseMessage>(r =>
                r.StatusCode == HttpStatusCode.TooManyRequests && r.Headers.RetryAfter != null)
            .WaitAndRetryAsync(3,
                sleepDurationProvider: (_, result, _) => result.Result.Headers.RetryAfter.Delta.Value,
                onRetryAsync: async (_, _, _, _) => { });
        }
        
    }
}
