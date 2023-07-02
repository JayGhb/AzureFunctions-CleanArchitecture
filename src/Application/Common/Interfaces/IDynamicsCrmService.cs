
namespace SlottingMock.Application.Common.Interfaces
{
    public interface IDynamicsCrmService
    {
        Task<IEnumerable<string>> GetDataAsync(CancellationToken cancellationToken);
    }
}
