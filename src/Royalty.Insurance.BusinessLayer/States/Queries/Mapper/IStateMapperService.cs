using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.States.Queries.Mapper
{
    public interface IStateMapperService
    {
        //TODO: change it
        //void UpdateEntity(State entity, StateRequest request);
        Expression<Func<State, StateResponse>> MapResponse { get; }
    }
}
