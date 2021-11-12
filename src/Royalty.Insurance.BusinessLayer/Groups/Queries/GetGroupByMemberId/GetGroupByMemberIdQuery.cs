using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetGroupByMemberIdQuery : IRequest<List<GroupResponse>>
    {

    }
}
