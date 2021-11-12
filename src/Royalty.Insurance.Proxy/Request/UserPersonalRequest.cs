using System;
using System.ComponentModel.DataAnnotations;

namespace Royalty.Insurance.Proxy.Request
{
    public class UserPersonalRequest : UserProfileBaseRequest
    {
        public string PersonalAvatar { get; set; }
    }
}