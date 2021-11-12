using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class GetAgentTasksQueryValidator : AbstractValidator<GetAgentTasksQuery>
    {
        public GetAgentTasksQueryValidator()
        {
            RuleFor(v => v.PageIndex)
                .GreaterThan(0);
            RuleFor(v => v.PageSize)
                .GreaterThan(0);
        }
    }
}
