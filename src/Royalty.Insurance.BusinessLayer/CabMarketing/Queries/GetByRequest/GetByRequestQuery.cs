using MediatR;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetByRequestQuery : IRequest<PaginationResponse<DetailedSearch>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Request { get; set; }
        public int CabIndex { get; set; } = 0;
    }
}
