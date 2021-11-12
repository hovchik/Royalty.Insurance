using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
    {
        public ForgetPasswordCommandValidator()
        {
            RuleFor(x => x.Email).MaximumLength(256).EmailAddress();
        }
    }
}
