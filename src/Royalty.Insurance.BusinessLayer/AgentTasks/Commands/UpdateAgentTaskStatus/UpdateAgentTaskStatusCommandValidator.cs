using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class UpdateAgentTaskStatusCommandValidator : AbstractValidator<UpdateAgentTaskTaskStatusCommand>
    {
        public UpdateAgentTaskStatusCommandValidator()
        {

            RuleFor(x => x.AgentTaskStatusId)
                .NotEmpty().WithMessage("AgentTaskStatus is required");
        }
    }
}
