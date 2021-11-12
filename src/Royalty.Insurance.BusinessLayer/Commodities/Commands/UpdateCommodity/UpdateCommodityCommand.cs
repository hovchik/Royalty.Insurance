using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class UpdateCommodityCommand : IRequest<CommodityResponse>
    {
        public int Id { get; set; }
        public CommodityRequest Request { get; set; }
    }
}
