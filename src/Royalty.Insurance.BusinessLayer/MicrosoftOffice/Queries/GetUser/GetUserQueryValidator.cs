using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
