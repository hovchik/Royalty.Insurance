using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Proxy.Request
{
    public class UserAdminRequest : UserBaseRequest
    {
        public UserRoleType Role { get; set; }
    }
}