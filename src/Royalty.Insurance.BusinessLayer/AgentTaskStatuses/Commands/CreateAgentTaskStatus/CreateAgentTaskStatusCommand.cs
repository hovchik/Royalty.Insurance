using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class CreateAgentTaskStatusCommand : IRequest<AgentTaskStatusResponse>
    {
        public string Name { get; set; }
    }
}
