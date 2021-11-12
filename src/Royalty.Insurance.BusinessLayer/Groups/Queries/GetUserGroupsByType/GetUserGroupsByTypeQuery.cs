using System.Collections.Generic;
using MediatR;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetUserGroupsByTypeQuery : IRequest<List<GroupResponse>>
    {
        public GroupTypeCode GroupTypeCode { get; set; }
    }
}
