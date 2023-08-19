using Application.Common.Interfaces;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICorrelationContextAccessor _correlationContextAccessor;
        private StringValues correlationId;
        private IDisposable loggerScope;
        private const string correlationIdHeaderName = "x-correlation-id";

        public FunctionBase(ILogger logger, IHttpContextAccessor httpContextAccessor, ICorrelationContextAccessor correlationContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _correlationContextAccessor = correlationContextAccessor;
        }

        public Task OnExecutingAsync(FunctionExecutingContext executingContext, CancellationToken cancellationToken)
        {
            bool hasValidCorrelationId = false;

            HttpRequest httpRequest = executingContext?.Arguments?.Values.OfType<HttpRequest>().FirstOrDefault();
            if (!hasValidCorrelationId && httpRequest != null)
            {
                httpRequest.Headers.TryGetValue(correlationIdHeaderName, out correlationId);
                if (!string.IsNullOrWhiteSpace(correlationId))
                {
                    hasValidCorrelationId = true;
                    loggerScope = _logger.BeginScope(new Dictionary<string, object> { { "CorrelationId", correlationId } });
                    _correlationContextAccessor.CorrelationId = correlationId;
                }
            }

            if (!hasValidCorrelationId)
            {
                string guid = Guid.NewGuid().ToString();
                correlationId = guid;
                loggerScope = _logger.BeginScope(new Dictionary<string, object> { { "CorrelationId", guid } });
                _correlationContextAccessor.CorrelationId = correlationId;
            }

            return Task.CompletedTask;
        }

        public Task OnExecutedAsync(FunctionExecutedContext executedContext, CancellationToken cancellationToken)
        {
            loggerScope?.Dispose();
            //It is advised not to add correlation id in response headers if the client application
            //is is not within your organization
            _httpContextAccessor.HttpContext.Response.Headers.Add(correlationIdHeaderName, correlationId);
            return Task.CompletedTask;
        }
    }
}
