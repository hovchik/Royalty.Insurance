
using System;

namespace Royalty.Insurance.Proxy.Response
{
    public class UserActivityLogResponse
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public DateTime Login { get; set; }

        public int Duration { get; set; }
    }
}
