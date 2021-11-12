using MediatR;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Roles
{
    public class GetRoleByTypeQuery : IRequest<string>
    {
        public UserRoleType RoleType { get; set; }
    }
}
