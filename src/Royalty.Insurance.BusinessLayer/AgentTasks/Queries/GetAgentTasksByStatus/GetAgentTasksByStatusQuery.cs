using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class GetAgentTasksByStatusQuery : IRequest<PaginationResponse<AgentTaskResponse>>
    {
        public int AgentTaskStatusId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
