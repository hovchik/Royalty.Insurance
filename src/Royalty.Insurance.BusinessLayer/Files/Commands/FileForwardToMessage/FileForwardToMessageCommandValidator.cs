using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Files.Queries.GetFileById
{
    public class FileForwardToMessageCommandValidator : AbstractValidator<FileForwardToMessageCommand>
    {
        public FileForwardToMessageCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.GroupId).GreaterThan(0);
        }
    }
}
