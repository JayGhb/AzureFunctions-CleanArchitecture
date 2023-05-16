using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlottingMock.Application.Exceptions
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
