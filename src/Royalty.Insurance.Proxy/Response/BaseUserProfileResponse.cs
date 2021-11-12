
using System.Common.Authentication.Models;
using Core.System.Security.Cryptography;

namespace Royalty.Insurance.Proxy.Response
{
    public class BaseUserProfileResponse
    {
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private string _personalAvatar;
        private readonly AppSetting _appSetting;

        public BaseUserProfileResponse(IExpiryQueryParameterCreator expiryQueryParameterCreator, AppSetting appSetting)
        {
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _appSetting = appSetting;
        }

        public string PersonalAvatar
        {
            get => GetAvatarQueryParams(_personalAvatar, _expiryQueryParameterCreator.GetAvatarQueryParam(UserId, _appSetting.QueryParamExpiry));
            set =>
                _personalAvatar = value; 
        }

        public int UserId { get; set; }


        private  static string GetAvatarQueryParams(string personalAvatar,  string queryParams)
        {
            if (!string.IsNullOrWhiteSpace(personalAvatar))
            {
                return  $"{personalAvatar}{queryParams}";
            }

            return personalAvatar;
        }
    }
}
