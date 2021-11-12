using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Cargoes
{
    public interface ICargoMapperService
    {
        Expression<Func<Cargo, CargoResponse>> MapResponse { get; }
    }
}