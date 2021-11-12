using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Roles
{
    public class RoleMapperService : IRoleMapperService
    {
        public Expression<Func<Role, RoleResponse>> MapResponse
        {
            get
            {
                return entity => new RoleResponse
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
            }
        }
    }
}