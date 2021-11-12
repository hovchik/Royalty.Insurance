using System;
using System.Collections.Generic;

namespace Domain
{
    public class Message
    {
        public Message()
        {
            InverseParent = new HashSet<Message>();
            MessageAttachments = new HashSet<MessageAttachment>();
            UnreadMessages = new HashSet<UnreadMessage>();
        }

        
        public long Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientGroupId { get; set; }
        public string Body { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        public long? ParentId { get; set; }

        public Message Parent { get; set; }
        public Group RecipientGroup { get; set; }
        public User Sender { get; set; }
        public ICollection<Message> InverseParent { get; set; }
        public ICollection<MessageAttachment> MessageAttachments { get; set; }
        public ICollection<UnreadMessage> UnreadMessages { get; set; }
    }
}
