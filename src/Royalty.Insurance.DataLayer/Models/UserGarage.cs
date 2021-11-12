using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class UserGarage
    {
        public UserGarage()
        {
            Attachments = new HashSet<Attachment>();
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(50)]
        public string FileName { get; set; }
        public int? AssignedInsuredId { get; set; }
        public byte FileFormatId { get; set; }
        [Required]
        [StringLength(100)]
        public string Path { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDatetimeUtc { get; set; }

        [ForeignKey(nameof(AssignedInsuredId))]
        [InverseProperty(nameof(Insured.UserGarages))]
        public virtual Insured AssignedInsured { get; set; }
        [ForeignKey(nameof(FileFormatId))]
        [InverseProperty("UserGarages")]
        public virtual FileFormat FileFormat { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserGarages")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(Attachment.UserGarage))]
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}
