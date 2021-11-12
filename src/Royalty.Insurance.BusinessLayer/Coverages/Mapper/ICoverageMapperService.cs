using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public interface ICoverageMapperService
    {
        void UpdateEntity(Coverage entity, CoverageRequest request);
        Expression<Func<Coverage, CoverageResponse>> MapResponse { get; }
    }
}