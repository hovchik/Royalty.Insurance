using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class SaleAgaveCommand : IRequest<AgaveRoyaltyResponse>
    {
        public AgaveRoyaltySaleRequest AgaveSaleRequest { get; set; }
    }
}