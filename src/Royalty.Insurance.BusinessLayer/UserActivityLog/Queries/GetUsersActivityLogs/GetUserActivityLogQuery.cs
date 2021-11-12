using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.UserActivityLogs
{
    public class GetUserActivityLogQuery : IRequest<PaginationResponse<UserActivityLogResponse>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
