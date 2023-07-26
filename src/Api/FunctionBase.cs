using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api
{
    public class FunctionBase : IFunctionInvocationFilter
    {
        private readonly ILogger _logger;
        private IDisposable loggerScope;

        public FunctionBase(ILogger logger)
        {
            _logger = logger;
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            bool hasValidCorrelationId = false;
            StringValues correlationId = string.Empty;

            var httpRequest = executingContext?.Arguments?.Values.OfType<HttpRequest>().FirstOrDefault();
            if (!hasValidCorrelationId && httpRequest != null)
            {
                httpRequest.Headers.TryGetValue("x-correlation-id", out correlationId);
                if (!string.IsNullOrWhiteSpace(correlationId))
                {
                    hasValidCorrelationId = true;
                    loggerScope = _logger.BeginScope(new Dictionary<string, object> { { "CorrelationId", correlationId } });
                }
            }

            if (!hasValidCorrelationId)
            {
                string guid = Guid.NewGuid().ToString();
                loggerScope = _logger.BeginScope(new Dictionary<string, object> { { "CorrelationId", guid } });
            }

            return Task.CompletedTask;
        }

        public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            loggerScope?.Dispose();
            return Task.CompletedTask;
        }
    }
}
