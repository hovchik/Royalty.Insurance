using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class CreateIndividualGroupCommandValidator : AbstractValidator<CreateIndividualGroupCommand>
    {
        public CreateIndividualGroupCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
        }
    }
}
