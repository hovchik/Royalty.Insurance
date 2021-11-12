
using System;

namespace Royalty.Insurance.Proxy.Request
{
    public class UserActivityLogRequest
    {
        public int UserId { get; set; }

        public Guid SessionId { get; set; }

        public string DeviceIp { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpireAt { get; set; }
    }
}
