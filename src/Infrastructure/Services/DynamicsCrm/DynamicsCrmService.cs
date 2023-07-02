using SlottingMock.Application.Common.Interfaces;

namespace SlottingMock.Infrastructure.Services.DynamicsCrm
{
    public class DynamicsCrmService : IDynamicsCrmService
    {
        private readonly HttpClient _httpClient;

        public DynamicsCrmService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetDataAsync(CancellationToken cancellationToken) 
        { 
            return Enumerable.Empty<string>(); 
        }
    }
}
