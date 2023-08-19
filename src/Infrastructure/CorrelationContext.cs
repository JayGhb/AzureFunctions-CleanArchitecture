
namespace Infrastructure
{
    internal class CorrelationContext
    {
        private static readonly AsyncLocal<string> _correlationId = new AsyncLocal<string>();

        internal static string CorrelationId
        {
            get => _correlationId.Value;
            set => _correlationId.Value = value;
        }
        
    }
}
