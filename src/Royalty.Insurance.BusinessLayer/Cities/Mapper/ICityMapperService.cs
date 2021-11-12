using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Cities
{
    public interface ICityMapperService
    {
        void UpdateEntity(City entity, InsertCityCommand request);
        Expression<Func<City, CityResponse>> MapResponse { get; }
    }
}
