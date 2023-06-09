
namespace Application.Common
{
    public class RequestContext
    {
        private static readonly AsyncLocal<string> _correlationId = new AsyncLocal<string>();

        public static string CorrelationId
        {
            get => _correlationId.Value;
            set => _correlationId.Value = value;
        }
    }
}
