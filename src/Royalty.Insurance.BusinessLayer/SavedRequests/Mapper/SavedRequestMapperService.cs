using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class SavedRequestMapperService : ISavedRequestMapperService
    {
        public void UpdateEntity(SavedMarketingRequest entity, CreateSavedRequestCommand request)
        {
            entity.SavedRequest = request.Request;
            entity.ShortDescription = request.ShortDescription;
            entity.CreatedDateUtc = DateTime.UtcNow;
            entity.Hash = request.Request.GetHashCode();
        }

        public void UpdateEntity(SavedMarketingRequest entity, UpdateSavedRequestCommand request)
        {
            entity.SavedRequest = request.Request;
            entity.ShortDescription = request.ShortDescription;
            entity.CreatedDateUtc = DateTime.UtcNow;
            entity.Hash = request.Request.GetHashCode();
        }

        public Expression<Func<SavedMarketingRequest, SavedRequestResponse>> MapResponse
        {
            get
            {
                return entity => new SavedRequestResponse
                {
                    Request = entity.SavedRequest,
                    ShortDescription = entity.ShortDescription,
                    CreatedDate = entity.CreatedDateUtc,
                    Id = entity.Id
                };
            }
        }
    }
}