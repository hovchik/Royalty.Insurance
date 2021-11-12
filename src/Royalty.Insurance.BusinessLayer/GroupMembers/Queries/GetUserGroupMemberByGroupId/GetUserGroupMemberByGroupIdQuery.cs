using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.GroupMembers

{
    public class GetUserGroupMemberByGroupIdQuery : IRequest<List<GroupMemberResponse>>
    {
        public int GroupId { get; set; }
        public int UserRequestedId { get; set; }
    }
}
