using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetGroupsQuery : IRequest<List<GroupResponse>>
    {
    }
}
