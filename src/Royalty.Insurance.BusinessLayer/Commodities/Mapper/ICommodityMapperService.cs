using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public interface ICommodityMapperService
    {
        void UpdateEntity(Commodity entity, CommodityRequest request);
        Expression<Func<Commodity, CommodityResponse>> MapResponse { get; }
    }
}