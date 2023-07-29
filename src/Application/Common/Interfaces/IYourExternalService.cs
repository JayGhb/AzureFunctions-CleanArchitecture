
namespace Application.Common.Interfaces
{
    public interface IYourExternalService
    {
        Task<IEnumerable<string>> GetDataAsync(CancellationToken cancellationToken);
    }
}
