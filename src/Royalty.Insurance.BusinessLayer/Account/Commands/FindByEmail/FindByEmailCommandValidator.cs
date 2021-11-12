using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class FindByEmailCommandValidator : AbstractValidator<FindByEmailCommand>
    {
        public FindByEmailCommandValidator()
        {
            RuleFor(x => x.Email).MaximumLength(256).EmailAddress();
        }
    }
}
