using System;

namespace Domain
{
    public class UserPhone
    {
        
        public int Id { get; set; }
        
        
        public string PhoneNumber { get; set; }
        
        
        public string IpAddress { get; set; }
        public int PhoneOwnerId { get; set; }
        public int Extension { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }

        public User CreatedByNavigation { get; set; }
        public User PhoneOwner { get; set; }
        public User UpdatedByNavigation { get; set; }
    }
}
