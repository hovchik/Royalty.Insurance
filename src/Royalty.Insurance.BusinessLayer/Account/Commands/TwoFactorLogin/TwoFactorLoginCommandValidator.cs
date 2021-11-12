using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class TwoFactorLoginCommandValidator : AbstractValidator<TwoFactorLoginCommand>
    {
        public TwoFactorLoginCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
