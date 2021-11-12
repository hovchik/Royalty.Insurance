using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.GroupId).GreaterThan(0);
            RuleFor(x => x.Content).MaximumLength(1024);
        }
    }
}
