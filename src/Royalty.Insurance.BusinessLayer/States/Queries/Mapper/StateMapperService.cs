using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.States.Queries.Mapper
{
    public class StateMapperService : IStateMapperService
    {
        //TODO: change it
        //public void UpdateEntity(State entity, StateRequest request)
        //{
        //    entity.Name = request.Name;
        //}

        public Expression<Func<State, StateResponse>> MapResponse
        {
            get
            {
                return entity => new StateResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                };
            }
        }
    }
}
