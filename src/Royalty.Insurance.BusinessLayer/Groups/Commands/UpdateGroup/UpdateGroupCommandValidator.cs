using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
