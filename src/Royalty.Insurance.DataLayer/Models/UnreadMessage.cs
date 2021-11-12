using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Index(nameof(ReadUserId), nameof(MessageId), Name = "IX_UnReadMessagesUniqieUserIdMessageId", IsUnique = true)]
    public partial class UnreadMessage
    {
        [Key]
        public long Id { get; set; }
        public long MessageId { get; set; }
        public int ReadUserId { get; set; }
        public int SendUserId { get; set; }
        public int GroupId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReadDatetimeUtc { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("UnreadMessages")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(MessageId))]
        [InverseProperty("UnreadMessages")]
        public virtual Message Message { get; set; }
        [ForeignKey(nameof(ReadUserId))]
        [InverseProperty(nameof(User.UnreadMessageReadUsers))]
        public virtual User ReadUser { get; set; }
        [ForeignKey(nameof(SendUserId))]
        [InverseProperty(nameof(User.UnreadMessageSendUsers))]
        public virtual User SendUser { get; set; }
    }
}
