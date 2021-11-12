
using System;
using System.Common.Authentication.Models;
using Core.System.Security.Cryptography;

namespace Royalty.Insurance.Proxy.Response
{
    public class MemberResponse : BaseUserProfileResponse
    {
        public MemberResponse(IExpiryQueryParameterCreator expiryQueryParameterCreator, AppSetting appSetting) : base(expiryQueryParameterCreator, appSetting)
        {
        }

        public string MemberFullName { get; set; }

        public int Status { get; set; }


        public string LastMessage { get; set; }

        public DateTime? LastMessageDate { get; set; }
        
        public bool Muted { get; set; }

        public int UnreadMessageCount { get; set; }
    }
}
