
using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.Response
{
    public class GroupMemberResponse
    {
        public int GroupId { get; set; }

        public int GroupCreatedById { get; set; }

        public string GroupName { get; set; }

        public byte GroupTypeId { get; set; }

        public List<MemberResponse> Members { get; set; }
    }
}
