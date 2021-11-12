using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agencies
{
    public class AgencyMapperService : IAgencyMapperService
    {
        public void UpdateEntity(Agency entity, UpdateAgencyCommand request)
        {
            entity.Name = request.Name;
            entity.FaxNumber = request.FaxNumber;
            entity.Address = request.Address;
            entity.Zip = request.Zip;
            entity.City = request.City;
            entity.State = request.State;
            entity.PhoneNumber = request.PhoneNumber;
        }

        public Expression<Func<Agency, AgencyResponse>> MapResponse
        {
            get
            {
                return entity => new AgencyResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    FaxNumber = entity.FaxNumber,
                    Address = entity.Address,
                    State = entity.State,
                    Zip = entity.State,
                    City = entity.City,
                    PhoneNumber = entity.PhoneNumber
                };
            }
        }
    }
}
