using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public class CoverageMapperService : ICoverageMapperService
    {
        public void UpdateEntity(Coverage entity, CoverageRequest request)
        {
            entity.CoverageLimit = request.Limit;
            entity.CoverageType = request.CoverageType;
        }

        public Expression<Func<Coverage, CoverageResponse>> MapResponse
        {
            get
            {
                return entity => new CoverageResponse
                {
                    Limit = entity.CoverageLimit,
                    CoverageType = entity.CoverageType
                };
            }
        }
    }
}