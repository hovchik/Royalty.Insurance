using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class CheckUserFileExistsQueryValidator : AbstractValidator<CheckUserFileExistsQuery>
    {
        public CheckUserFileExistsQueryValidator()
        {
            RuleFor(x => x.FileName).NotEmpty();
        }
    }
}
