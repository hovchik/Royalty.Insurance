using MediatR;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class DeleteAgentTaskStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
