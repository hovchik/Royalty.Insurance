using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.Roles
{
    public class GetRoleQuery : IRequest<List<RoleResponse>>
    {
    }
}
