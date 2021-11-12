using System;

namespace Domain
{
    public class UnreadMessage
    {
        
        public long Id { get; set; }
        public long MessageId { get; set; }
        public int ReadUserId { get; set; }
        public int SendUserId { get; set; }
        public int GroupId { get; set; }
        
        public DateTime ReadDatetimeUtc { get; set; }

        public Group Group { get; set; }
        public Message Message { get; set; }
        public User ReadUser { get; set; }
        public User SendUser { get; set; }
    }
}
