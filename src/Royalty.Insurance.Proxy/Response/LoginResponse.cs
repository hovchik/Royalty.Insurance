using System.Common.Authentication.Models;
using Core.System.Security.Cryptography;

namespace Royalty.Insurance.Proxy.Response
{
    public class LoginResponse : BaseUserProfileResponse
    {
        public LoginResponse(IExpiryQueryParameterCreator expiryQueryParameterCreator, AppSetting appSetting) : base(expiryQueryParameterCreator, appSetting)
        {
        }

        //todo user Role
        public string FullName { get; set; }
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}