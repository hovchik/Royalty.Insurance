using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class UpdateAgentTaskAssigneeCommand : IRequest<AgentTaskResponse>
    {
        public int Id { get; set; }

        public int? AssigneeId { get; set; }
    }
}
