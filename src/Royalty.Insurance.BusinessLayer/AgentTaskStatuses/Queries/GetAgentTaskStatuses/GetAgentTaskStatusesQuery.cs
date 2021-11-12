using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class GetAgentTaskStatusesQuery : IRequest<PaginationResponse<AgentTaskStatusResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }
}
