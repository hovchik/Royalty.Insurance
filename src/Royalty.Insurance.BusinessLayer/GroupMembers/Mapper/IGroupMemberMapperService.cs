using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public interface IGroupMemberMapperService
    {
        void UpdateEntity(GroupMember entity, int memberId);
        Expression<Func<Group, GroupMemberResponse>> MapResponse { get; }
    }
}
