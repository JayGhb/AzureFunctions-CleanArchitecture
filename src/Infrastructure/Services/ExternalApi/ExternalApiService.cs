using SlottingMock.Application.Common.Interfaces;

namespace SlottingMock.Infrastructure.Services.ExternalApi
{
    public class ExternalApiService : IExternalApiService
    {
        public async Task<IEnumerable<string>> GetDataAsync(CancellationToken cancellationToken) 
        { 
            return Enumerable.Empty<string>(); 
        }
    }
}
