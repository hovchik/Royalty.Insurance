using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public interface IInsuredMapperService
    {
        void UpdateEntity(Insured entity, InsuredRequest request);
        Expression<Func<Insured, InsuredResponse>> MapResponse { get; }
    }
}
