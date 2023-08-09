using Application.UseCases.Commands.ReserveSlot;
using MediatR;

namespace Application.UseCases.Commands
{
    public class ReserveSlotCommandHandler : IRequestHandler<ReserveSlotCommand, Unit>
    {
        public Task<Unit> Handle(ReserveSlotCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
