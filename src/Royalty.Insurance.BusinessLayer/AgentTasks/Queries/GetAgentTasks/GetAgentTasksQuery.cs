using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class GetAgentTasksQuery : IRequest<PaginationResponse<AgentTaskResponse>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
