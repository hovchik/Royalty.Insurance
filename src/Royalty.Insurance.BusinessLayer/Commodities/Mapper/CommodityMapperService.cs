using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class CommodityMapperService : ICommodityMapperService
    {
        public void UpdateEntity(Commodity entity, CommodityRequest request)
        {
            entity.CommodityValue = request.CommodityValue;
            entity.CommodityPercent = request.CommodityPercent;
            entity.Name = request.Name;
        }

        public Expression<Func<Commodity, CommodityResponse>> MapResponse
        {
            get
            {
                return entity => new CommodityResponse
                {
                    CommodityPercent = entity.CommodityPercent,
                    Name = entity.Name,
                    CommodityValue = entity.CommodityValue
                };
            }
        }
    }
}