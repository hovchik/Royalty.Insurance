using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class GetCommoditiesQuery : IRequest<List<CommodityResponse>>
    {
    }
}
