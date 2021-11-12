using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class CreateAgentTaskStatusCommandValidator : AbstractValidator<CreateAgentTaskStatusCommand>
    {
        public CreateAgentTaskStatusCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
