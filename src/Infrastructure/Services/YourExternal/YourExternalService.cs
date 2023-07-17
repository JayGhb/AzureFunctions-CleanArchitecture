﻿using Microsoft.Extensions.Logging;
using SlottingMock.Application.Common.Interfaces;

namespace SlottingMock.Infrastructure.Services.YourExternal
{
    public class YourExternalService : IYourExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _getDataEndpoint;
        private ILogger<YourExternalService> _logger;

        public YourExternalService(HttpClient httpClient, ILogger<YourExternalService> logger)
        {
            _getDataEndpoint = Environment.GetEnvironmentVariable("GetDataEndpoint");
            if(_getDataEndpoint == null) throw new ArgumentNullException(nameof(_getDataEndpoint), $"Environment variable for GetDataEndpoint is not set.");
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<string>> GetDataAsync(CancellationToken cancellationToken) 
        {
            string requestUrl = _httpClient.BaseAddress + _getDataEndpoint;
            _logger.LogInformation("Requesting data from Dynamics {request}", requestUrl);
            HttpResponseMessage result = await _httpClient.GetAsync(requestUrl, cancellationToken);

            return Enumerable.Empty<string>(); 
        }
    }
}