using System.Text.RegularExpressions;
using FluentValidation;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            var regex = new Regex(SystemConstants.PasswordValidationRegex);
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                RuleFor(x => x.Password).NotEmpty().MinimumLength(8).Must(x => regex.IsMatch(x));
            }
            RuleFor(x => x.Email).MaximumLength(256).EmailAddress();
        }
    }
}
