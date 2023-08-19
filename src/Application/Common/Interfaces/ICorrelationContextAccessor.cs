
namespace Application.Common.Interfaces
{
    public interface ICorrelationContextAccessor
    {
        string CorrelationId { get; set; }
    }
}
