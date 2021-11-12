using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Cargoes
{
    public class CargoMapperService : ICargoMapperService
    {

        public Expression<Func<Cargo, CargoResponse>> MapResponse
        {
            get
            {
                return entity => new CargoResponse
                {
                    InsuredId = entity.InsuredId,
                    Id = entity.Id
                };
            }
        }
    }
}