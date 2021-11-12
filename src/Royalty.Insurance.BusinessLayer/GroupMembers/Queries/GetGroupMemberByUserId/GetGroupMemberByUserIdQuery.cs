using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class GetGroupMemberByUserIdQuery : IRequest<List<GroupMemberResponse>>
    {
        public int UserRequestedId { get; set; }
    }
}
