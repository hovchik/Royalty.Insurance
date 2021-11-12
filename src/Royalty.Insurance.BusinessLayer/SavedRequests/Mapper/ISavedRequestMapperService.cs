using Domain;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public interface ISavedRequestMapperService
    {
        void UpdateEntity(SavedMarketingRequest entity, CreateSavedRequestCommand request);
        Expression<Func<SavedMarketingRequest, SavedRequestResponse>> MapResponse { get; }

        void UpdateEntity(SavedMarketingRequest entity, UpdateSavedRequestCommand request);
    }
}