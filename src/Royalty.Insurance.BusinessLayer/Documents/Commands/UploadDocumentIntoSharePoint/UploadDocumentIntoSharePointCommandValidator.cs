using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Documents.Commands.UploadDocumentIntoSharePoint
{
    public class UploadDocumentIntoSharePointCommandValidator : AbstractValidator<UploadDocumentIntoSharePointCommand>
    {
        public UploadDocumentIntoSharePointCommandValidator()
        {
            RuleFor(x => x.DocumentFile).NotNull();
        }
    }
}
