using System;

namespace Royalty.Insurance.SessionScheduler.DataManager.Domain
{
    public class UserActivityLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid SessionId { get; set; }
        public string DeviceIp { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireAt { get; set; }
        public DateTime LogInDatetimeUtc { get; set; }
        public DateTime? LogOutDatetimeUtc { get; set; }


        public User User { get; set; }
    }
}
