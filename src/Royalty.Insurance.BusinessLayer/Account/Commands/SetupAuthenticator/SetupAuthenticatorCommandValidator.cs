using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetupAuthenticatorCommandValidator : AbstractValidator<SetupAuthenticatorCommand>
    {
        public SetupAuthenticatorCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
