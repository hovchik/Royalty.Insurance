using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class Message
    {
        public Message()
        {
            InverseParent = new HashSet<Message>();
            MessageAttachments = new HashSet<MessageAttachment>();
            UnreadMessages = new HashSet<UnreadMessage>();
        }

        [Key]
        public long Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientGroupId { get; set; }
        [StringLength(1024)]
        public string Body { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        public long? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(Message.InverseParent))]
        public virtual Message Parent { get; set; }
        [ForeignKey(nameof(RecipientGroupId))]
        [InverseProperty(nameof(Group.Messages))]
        public virtual Group RecipientGroup { get; set; }
        [ForeignKey(nameof(SenderId))]
        [InverseProperty(nameof(User.Messages))]
        public virtual User Sender { get; set; }
        [InverseProperty(nameof(Message.Parent))]
        public virtual ICollection<Message> InverseParent { get; set; }
        [InverseProperty(nameof(MessageAttachment.Message))]
        public virtual ICollection<MessageAttachment> MessageAttachments { get; set; }
        [InverseProperty(nameof(UnreadMessage.Message))]
        public virtual ICollection<UnreadMessage> UnreadMessages { get; set; }
    }
}
