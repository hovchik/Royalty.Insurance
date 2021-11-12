using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agencies
{
    public interface IAgencyMapperService
    {
        void UpdateEntity(Agency entity, UpdateAgencyCommand request);
        Expression<Func<Agency, AgencyResponse>> MapResponse { get; }
    }
}
