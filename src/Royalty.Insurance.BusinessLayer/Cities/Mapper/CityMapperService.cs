using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Cities
{
    public class CityMapperService : ICityMapperService
    {
        public void UpdateEntity(City entity, InsertCityCommand request)
        {
            entity.Name = request.Name;
        }

        public Expression<Func<City, CityResponse>> MapResponse
        {
            get
            {
                return entity => new CityResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    StateId = entity.StateId,
                };
            }
        }
    }
}
