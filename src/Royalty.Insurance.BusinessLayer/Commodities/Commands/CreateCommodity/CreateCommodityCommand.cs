using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class CreateCommodityCommand : IRequest<CommodityResponse>
    {
        public CommodityRequest Request { get; set; }
    }
}
