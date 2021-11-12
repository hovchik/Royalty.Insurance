using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class Document
    {
        [Key]
        public int Id { get; set; }
        public int? InsuredId { get; set; }
        [Required]
        [StringLength(100)]
        public string DocumentName { get; set; }
        [Required]
        [StringLength(1024)]
        public string Path { get; set; }
        [StringLength(100)]
        public string GroupId { get; set; }
        [StringLength(100)]
        public string DriveItemId { get; set; }
        public byte DocumentTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeleteDatetimeUtc { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.DocumentCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(DocumentTypeId))]
        [InverseProperty("Documents")]
        public virtual DocumentType DocumentType { get; set; }
        [ForeignKey(nameof(InsuredId))]
        [InverseProperty("Documents")]
        public virtual Insured Insured { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.DocumentUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}
