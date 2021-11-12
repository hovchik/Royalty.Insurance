using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Files.Queries
{
    public class GetFilesWithPaginationQueryValidator : AbstractValidator<GetFilesWithPaginationQuery>
    {
        public GetFilesWithPaginationQueryValidator()
        {
            When(x => !string.IsNullOrEmpty(x.FileName), () =>
            {
                RuleFor(x => x.FileName).NotEmpty().MaximumLength(50);
            });
            
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
