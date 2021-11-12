using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GroupMapperService : IGroupMapperService
    {
        public void UpdateEntity(Group entity, CreateGroupCommand request)
        {
            entity.Name = request.Name;
            entity.GroupTypeId = (int)GroupTypeCode.Group;
        }

        public Expression<Func<Group, GroupResponse>> MapResponse
        {
            get
            {
                return entity => new GroupResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    GroupTypeId = entity.GroupTypeId,

                };
            }
        }
    }
}
