
namespace Application.Exceptions
{
    public class SlotNotFoundException : Exception
    {
        public SlotNotFoundException() : base() { }
        public SlotNotFoundException(string message) : base(message) { }
        public SlotNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected SlotNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
