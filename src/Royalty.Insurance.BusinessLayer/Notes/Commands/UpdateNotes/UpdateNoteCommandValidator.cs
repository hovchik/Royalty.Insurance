using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(x => x.Request.Note).NotNull().NotEmpty().MaximumLength(1024);
        }
    }
}
