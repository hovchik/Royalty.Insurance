using System.Linq;
using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Messages.Commands.CreateMessageWithAttachment
{
    public class CreateMessageWithAttachmentCommandValidator : AbstractValidator<CreateMessageWithAttachmentCommand>
    {
        public CreateMessageWithAttachmentCommandValidator()
        {
            RuleFor(x => x.Files.Select(x => x.FileName)).Must(x => x.Any(filename => filename.Length <= 50));
        }
    }
}
