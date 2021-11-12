using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetTwoFactorEnabledCommandValidator : AbstractValidator<SetTwoFactorEnabledCommand>
    {
        public SetTwoFactorEnabledCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
