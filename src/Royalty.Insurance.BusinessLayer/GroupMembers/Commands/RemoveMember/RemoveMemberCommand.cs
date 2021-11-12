using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class RemoveMemberCommand : IRequest<GroupMemberResponse>
    {
        public List<int> MemberIds { get; set; }
        public int GroupId { get; set; }
        public int UserRequestedId { get; set; }
    }
}
