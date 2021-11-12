using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    public class GetCallHistoryQuery : IRequest<PaginationResponse<UserPhoneCallHistoryResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}