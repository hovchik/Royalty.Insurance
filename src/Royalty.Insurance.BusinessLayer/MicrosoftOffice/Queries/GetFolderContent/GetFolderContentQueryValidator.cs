using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Queries
{
    public class GetFolderContentQueryValidator : AbstractValidator<GetFolderContentQuery>
    {
        public GetFolderContentQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.ParentFolderId).NotEmpty();
        }
    }
}
