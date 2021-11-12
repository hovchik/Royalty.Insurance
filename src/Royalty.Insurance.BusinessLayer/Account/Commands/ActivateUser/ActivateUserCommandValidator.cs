using System.Text.RegularExpressions;
using FluentValidation;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ActivateUserCommandValidator : AbstractValidator<ActivateUserCommand>
    {
        public ActivateUserCommandValidator()
        {
            var regex = new Regex(SystemConstants.PasswordValidationRegex);
            RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8).Must(x => regex.IsMatch(x));
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).Must(x => regex.IsMatch(x));
            RuleFor(x => x.Email).MaximumLength(256).EmailAddress();
        }
    }
}
