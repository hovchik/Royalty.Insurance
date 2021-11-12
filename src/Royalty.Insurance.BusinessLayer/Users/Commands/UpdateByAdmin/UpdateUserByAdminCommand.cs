using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class UpdateUserByAdminCommand : UserBaseRequest, IRequest<UserResponse>
    {
        public int UserId { get; set; }
        public UserRoleType Role { get; set; }
    }
}
