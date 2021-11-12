using System;

namespace Domain
{
    public class UserPhoneCallHistory
    {
        
        public long Id { get; set; }
        public int UserPhoneId { get; set; }
        public int InitialCallTypeId { get; set; }
        public int CurrentCallTypeId { get; set; }
        
        
        public string CallerNumber { get; set; }
        
        
        public string CallId { get; set; }
        public int Extension { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime? EndDatetimeUtc { get; set; }
        
        public string CallerName { get; set; }

        public CallType CurrentCallType { get; set; }
        public CallType InitialCallType { get; set; }
        public User UserPhone { get; set; }
    }
}
