using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class MessageAttachment
    {
        [Key]
        public long MessageId { get; set; }
        [Key]
        public int AttachmentId { get; set; }

        [ForeignKey(nameof(AttachmentId))]
        [InverseProperty("MessageAttachments")]
        public virtual Attachment Attachment { get; set; }
        [ForeignKey(nameof(MessageId))]
        [InverseProperty("MessageAttachments")]
        public virtual Message Message { get; set; }
    }
}
