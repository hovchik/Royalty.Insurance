using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class RefundAgaveCommand : IRequest<AgaveRoyaltyResponse>
    {
        public AgaveRoyaltyRefundRequest AgaveRoyaltyRefund { get; set; }
    }
}