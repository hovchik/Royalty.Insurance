using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class UpdateAgentTaskTaskStatusCommand : IRequest<AgentTaskResponse>
    {
        public int Id { get; set; }
        public int AgentTaskStatusId { get; set; }
    }
}
