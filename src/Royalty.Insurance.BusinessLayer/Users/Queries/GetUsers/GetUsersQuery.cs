using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class GetUsersQuery : IRequest<List<UserResponse>>
    {
    }
}
