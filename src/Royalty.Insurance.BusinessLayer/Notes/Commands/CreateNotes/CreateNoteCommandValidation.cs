using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class CreateNoteCommandValidation : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidation()
        {
            RuleFor(x => x.Request.Note).NotEmpty().MaximumLength(1024);
        }
    }
}
