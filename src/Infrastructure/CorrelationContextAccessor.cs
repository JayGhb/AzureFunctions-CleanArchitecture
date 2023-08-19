using Application.Common.Interfaces;

namespace Infrastructure
{
    public class CorrelationContextAccessor : ICorrelationContextAccessor
    {
        public string CorrelationId
        {
            get => CorrelationContext.CorrelationId;
            set => CorrelationContext.CorrelationId = value;
        }
    }
}
