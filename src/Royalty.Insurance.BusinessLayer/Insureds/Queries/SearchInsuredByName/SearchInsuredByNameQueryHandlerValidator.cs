using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Insureds.Queries
{
    public class SearchInsuredByNameQueryHandlerValidator : AbstractValidator<SearchInsuredByNameQuery>
    {
        public SearchInsuredByNameQueryHandlerValidator()
        {
            When(item => !string.IsNullOrEmpty(item.SearchTerm), () =>
            {
                RuleFor(x => x.SearchTerm)
                    .NotEmpty().WithMessage("Search term should not be null.");
                RuleFor(x => x.SearchTerm.Length < 3)
                    .Empty().WithMessage("Search term should not be less than 3 character.");
            });
            RuleFor(x => x.SearchTerm)
                .NotNull().WithMessage("Search term should not be null.");

        }
    }
}
