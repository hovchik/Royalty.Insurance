using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Files.Commands.UploadFile
{
    public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValidator()
        {
            RuleFor(x => x.File).NotNull();
            RuleFor(x => x.File.FileName).NotEmpty().MaximumLength(50);
            RuleFor(x => (int)x.FileFormatId).GreaterThan(0);
        }
    }
}
