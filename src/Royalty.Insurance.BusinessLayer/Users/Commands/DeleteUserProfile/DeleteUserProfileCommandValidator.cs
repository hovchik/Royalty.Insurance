using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class DeleteUserProfileCommandValidator : AbstractValidator<DeleteUserProfileCommand>
    {
        public DeleteUserProfileCommandValidator()
        {
            RuleFor(x => x.FileContainer).NotEmpty();
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
