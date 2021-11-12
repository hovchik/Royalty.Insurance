using FluentValidation;
using System;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class GetNotesByDateRangeQueryValidation : AbstractValidator<GetNotesByDateRangeQuery>
    {
        public GetNotesByDateRangeQueryValidation()
        {
            RuleFor(x => x.From).NotNull().NotEmpty().Must(BeAValidDate);
            RuleFor(x => x.To).NotNull().NotEmpty().Must(BeAValidDate);
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
