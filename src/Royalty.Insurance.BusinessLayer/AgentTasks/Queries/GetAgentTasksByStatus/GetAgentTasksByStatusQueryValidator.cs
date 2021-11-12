using System;
using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class GetAgentTasksByStatusQueryValidator : AbstractValidator<GetAgentTasksByStatusQuery>
    {
        public GetAgentTasksByStatusQueryValidator()
        {
            RuleFor(v => v.AgentTaskStatusId)
                .GreaterThan(0);
            RuleFor(v => v.PageIndex)
                .GreaterThan(0);
            RuleFor(v => v.PageSize)
                .GreaterThan(0);
        }
    }
}
