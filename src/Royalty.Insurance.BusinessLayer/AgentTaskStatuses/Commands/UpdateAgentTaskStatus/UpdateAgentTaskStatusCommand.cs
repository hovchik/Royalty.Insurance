using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class UpdateAgentTaskStatusCommand : CreateAgentTaskStatusCommand, IRequest<AgentTaskStatusResponse>
    {
        public int Id { get; set; }
    }
}
