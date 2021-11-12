using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class Attachment
    {
        public Attachment()
        {
            MessageAttachments = new HashSet<MessageAttachment>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int? UserGarageId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(UserGarageId))]
        [InverseProperty("Attachments")]
        public virtual UserGarage UserGarage { get; set; }
        [InverseProperty(nameof(MessageAttachment.Attachment))]
        public virtual ICollection<MessageAttachment> MessageAttachments { get; set; }
    }
}
