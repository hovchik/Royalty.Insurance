
using System.Common.Authentication.Models;
using System.Security;
using Core.System.Security.Cryptography;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Proxy.Response
{
    public class UserResponse : BaseUserProfileResponse
    {
        public UserResponse(IExpiryQueryParameterCreator expiryQueryParameterCreator, AppSetting appSetting) : base(expiryQueryParameterCreator, appSetting)
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string WorkPhone { get; set; }
        public UserRoleType Role { get; set; }
        public string IpAddress { get; set; }
        public int? Extension { get; set; }
        public int Status { get; set; }
        public string CustomStatus { get; set; }

    }
}
