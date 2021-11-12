using System;

namespace Domain
{
    public class UserTrustedDevice
    {
        public int Id { get; set; }
        
        public string DeviceId { get; set; }
        public int UserId { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }

        public User User { get; set; }
    }
}
