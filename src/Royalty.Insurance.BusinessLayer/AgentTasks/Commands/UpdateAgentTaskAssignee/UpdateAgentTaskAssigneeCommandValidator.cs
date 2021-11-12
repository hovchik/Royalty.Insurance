using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class UpdateAgentTaskAssigneeCommandValidator : AbstractValidator<UpdateAgentTaskAssigneeCommand>
    {
        public UpdateAgentTaskAssigneeCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0);
        }
    }
}
