using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class GetInsuredsNotesQuery : IRequest<PaginationResponse<InsuredsNotesResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
