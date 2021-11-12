using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Roles
{
    public interface IRoleMapperService
    {
        Expression<Func<Role, RoleResponse>> MapResponse { get; }
    }
}