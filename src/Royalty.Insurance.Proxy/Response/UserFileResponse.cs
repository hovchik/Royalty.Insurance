
using System;
using System.Common.Authentication.Models;
using Core.System.Security.Cryptography;

namespace Royalty.Insurance.Proxy.Response
{
    public class UserFileResponse
    {
        private IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private AppSetting _appSetting;
        private string _path;

        public UserFileResponse(IExpiryQueryParameterCreator expiryQueryParameterCreator, AppSetting appSetting)
        {
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _appSetting = appSetting;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Path
        {
            get => GetFileQueryParams(_path,
                _expiryQueryParameterCreator.GetAvatarQueryParam(UserId, _appSetting.QueryParamExpiry));
            set =>
                _path = value;
        }

        public string AssignToFullName { get; set; }
        public int? AssignToId { get; set; }

        public byte FileFormatId { get; set; }

        public DateTime CreateDateTime { get; set; }

        public string FileName { get; set; }

        private static string GetFileQueryParams(string personalAvatar, string queryParams)
        {
            if (!string.IsNullOrWhiteSpace(personalAvatar))
            {
                return $"{personalAvatar}{queryParams}";
            }

            return personalAvatar;
        }
    }
}
