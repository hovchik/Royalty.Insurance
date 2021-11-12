using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class GetAgentTaskByIdQuery : IRequest<AgentTaskResponse>
    {
        public int Id { get; set; }
    }
}
