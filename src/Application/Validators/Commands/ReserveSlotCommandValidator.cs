using Application.UseCases.Commands.ReserveSlot;
using FluentValidation;

namespace Application.Validators.Commands
{
    public class ReserveSlotCommandValidator : AbstractValidator<ReserveSlotCommand>
    {
        public ReserveSlotCommandValidator()
        { 
            RuleFor(c => c.SlotId)
                .NotNull().WithMessage("'Slot Id' must not be null")
                .NotEmpty().WithMessage("'Slot Id' must not be empty")
                .Configure(config => config.CascadeMode = CascadeMode.Stop);
        }
    }
}
