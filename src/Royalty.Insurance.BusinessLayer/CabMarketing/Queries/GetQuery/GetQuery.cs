using MediatR;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetQuery : IRequest<PaginationResponse<DetailedSearch>>
    {
        public MarketingRequest Request { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int CabIndex { get; set; } = 0;
    }
}

