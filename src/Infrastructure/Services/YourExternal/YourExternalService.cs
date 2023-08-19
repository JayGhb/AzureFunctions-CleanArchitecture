using Microsoft.Extensions.Logging;
using Application.Common.Interfaces;

namespace Infrastructure.Services.YourExternal
{
    public class YourExternalService : IYourExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _getDataEndpoint;
        private ILogger<YourExternalService> _logger;
        //private ICorrelationContextAccessor _correlationContextAccessor;

        public YourExternalService(HttpClient httpClient, ILogger<YourExternalService> logger, ICorrelationContextAccessor correlationContextAccessor)
        {
            _getDataEndpoint = Environment.GetEnvironmentVariable("GetDataEndpoint");
            if (_getDataEndpoint == null) throw new ArgumentNullException(nameof(_getDataEndpoint), $"Environment variable for GetDataEndpoint is not set.");
            _httpClient = httpClient;
            _logger = logger;
            //_correlationContextAccessor = correlationContextAccessor;
            _httpClient.DefaultRequestHeaders.Add("x-correlation-id", correlationContextAccessor.CorrelationId);
        }

        public async Task<IEnumerable<string>> GetDataAsync(CancellationToken cancellationToken) 
        {
            string requestUrl = _httpClient.BaseAddress + _getDataEndpoint;
            _logger.LogInformation("Requesting data from external service {request}", requestUrl);
            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl, cancellationToken);
            string responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("External service response {response}", responseContent);

            //handle response as necessary

            return Enumerable.Empty<string>(); 
        }
    }
}
