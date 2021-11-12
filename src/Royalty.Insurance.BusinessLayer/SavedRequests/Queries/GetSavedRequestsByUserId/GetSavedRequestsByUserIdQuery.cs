using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class GetSavedRequestsByUserIdQuery : IRequest<PaginationResponse<SavedRequestResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}