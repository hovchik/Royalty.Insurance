using System;

namespace Domain
{
    public class GroupMember
    {
        
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int MemberId { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public bool? Active { get; set; }
        public bool Muted { get; set; }

        public Group Group { get; set; }
        public User Member { get; set; }
    }
}
