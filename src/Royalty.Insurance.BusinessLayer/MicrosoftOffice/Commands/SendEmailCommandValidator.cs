using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Commands
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.FromEmail).NotEmpty();
            RuleFor(x => x.CcRecipients).NotEmpty();
        }
    }
}
