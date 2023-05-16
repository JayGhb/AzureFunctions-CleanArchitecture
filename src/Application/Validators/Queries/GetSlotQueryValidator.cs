using FluentValidation;
using SlottingMock.Application.UseCases.Queries.GetSlots;

namespace SlottingMock.Application.Validators.Queries
{
    public class GetSlotQueryValidator : AbstractValidator<GetSlotsQuery>
    {
        public GetSlotQueryValidator()
        {
            RuleFor(q => q.QueryPropertyA)
                .NotNull().WithMessage("'QueryPropertyA' must not be null")
                .NotEmpty().WithMessage("'QueryPropertyA' must not be empty");

        }
    }
}
