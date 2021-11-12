using FluentValidation;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class CreateAgentTaskCommandValidator : AbstractValidator<CreateAgentTaskCommand>
    {
        public CreateAgentTaskCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title should be ess than or equal 255 character");

            RuleFor(x => x.AgentTaskStatusId)
                .NotEmpty().WithMessage("AgentTaskStatus is required");
            byte startPoint = (int) AgentTaskTypeCode.Endorsement;
            byte endPoint = (int)AgentTaskTypeCode.Renew;
            RuleFor(x => x.AgentTaskTypeId)
                .GreaterThanOrEqualTo(startPoint)
                .LessThanOrEqualTo(endPoint)
                .WithMessage($"AgentTaskTypeId should be greater than or equal {startPoint} and less than or equal {endPoint}");

            RuleFor(x => x.Description)
                .MaximumLength(1024).WithMessage("Description should have less than or equal 1024 character");

            RuleFor(x => x.CanceledReason)
                .MaximumLength(255).WithMessage("Fax Number  should be less or equal 255");
        }
    }
}
