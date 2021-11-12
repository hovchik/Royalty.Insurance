using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class VerifyAuthenticatorCodeCommandValidator : AbstractValidator<VerifyAuthenticatorCodeCommand>
    {
        public VerifyAuthenticatorCodeCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Email).MaximumLength(256).EmailAddress();
        }
    }
}
