
using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class AddMemberCommand : IRequest<List<GroupMemberResponse>>
    {
        public int MemberId { get; set; }
        public int GroupId { get; set; }
        public int UserRequestedId { get; set; }
    }
}
