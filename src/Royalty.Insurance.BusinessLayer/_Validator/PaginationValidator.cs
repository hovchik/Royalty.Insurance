using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Validator
{
    public class PaginationValidator : AbstractValidator<PaginationCommand>
    {
        public PaginationValidator()
        {
            RuleFor(v => v.PageIndex)
                .GreaterThan(0).WithMessage("Page index is required.");
            RuleFor(v => v.PageSize)
                .GreaterThan(0).WithMessage("Page size is required.");
        }
    }
}
