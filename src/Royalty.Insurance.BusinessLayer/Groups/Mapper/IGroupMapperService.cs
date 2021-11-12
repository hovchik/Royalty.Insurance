using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public interface IGroupMapperService
    {
        void UpdateEntity(Group entity, CreateGroupCommand request);
        Expression<Func<Group, GroupResponse>> MapResponse { get; }
    }
}
