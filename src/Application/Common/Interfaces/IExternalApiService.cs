
namespace SlottingMock.Application.Common.Interfaces
{
    public interface IExternalApiService
    {
        Task<IEnumerable<string>> GetDataAsync(CancellationToken cancellationToken);
    }
}
