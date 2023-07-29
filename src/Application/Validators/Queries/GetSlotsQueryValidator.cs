using FluentValidation;
using Application.UseCases.Queries.GetSlots;
using System.Globalization;

namespace Application.Validators.Queries
{
    public class GetSlotsQueryValidator : AbstractValidator<GetSlotsQuery>
    {
        public GetSlotsQueryValidator()
        {
            RuleFor(q => q.Date)
                .NotNull().WithMessage("'Date' must not be null")
                .NotEmpty().WithMessage("'Date' must not be empty")
                .Must(BeValidDateFormat).WithMessage("'Date' format must be yyyy-MM-dd")
                .Configure(config => config.CascadeMode = CascadeMode.Stop);
        }

        private bool BeValidDateFormat(string input)
            => DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
