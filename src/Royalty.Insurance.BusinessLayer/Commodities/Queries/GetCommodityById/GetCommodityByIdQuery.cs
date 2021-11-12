using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class GetCommodityByIdQuery : IRequest<CommodityResponse>
    {
        public int Id { get; set; }
    }
}
