using System;
using System.Collections.Generic;

namespace Domain
{
    public class Group
    {
        public Group()
        {
            GroupMembers = new HashSet<GroupMember>();
            Messages = new HashSet<Message>();
            UnreadMessages = new HashSet<UnreadMessage>();
        }

        
        public int Id { get; set; }
        
        
        public string Name { get; set; }
        
        public bool Active { get; set; }
        public byte GroupTypeId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }

        public User CreatedByNavigation { get; set; }
        public User UpdatedByNavigation { get; set; }
        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<UnreadMessage> UnreadMessages { get; set; }
    }
}
