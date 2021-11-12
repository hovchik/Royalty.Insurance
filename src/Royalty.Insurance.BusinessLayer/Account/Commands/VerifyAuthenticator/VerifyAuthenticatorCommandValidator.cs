using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Account.Commands.VerifyAuthenticator
{
    public class VerifyAuthenticatorCommandValidator : AbstractValidator<VerifyAuthenticatorCommand>
    {
        public VerifyAuthenticatorCommandValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Email).MaximumLength(256).EmailAddress();
        }
    }
}
