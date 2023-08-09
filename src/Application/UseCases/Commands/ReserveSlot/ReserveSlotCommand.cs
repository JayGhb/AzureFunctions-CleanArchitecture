using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Commands.ReserveSlot
{
    public class ReserveSlotCommand : IRequest<Unit>
    {
        public string SlotId { get; private set; }

        public ReserveSlotCommand(string slotId)
        {
            SlotId = slotId;
        }
    }
}
