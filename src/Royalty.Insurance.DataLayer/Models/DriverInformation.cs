using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("DriverInformation")]
    public partial class DriverInformation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string DriverName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; }
        public int StateId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateHired { get; set; }
        public int YearOfExperiance { get; set; }
        [Column(TypeName = "ntext")]
        public string Accidents { get; set; }
        public int? InsuredId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.DriverInformationCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(InsuredId))]
        [InverseProperty("DriverInformations")]
        public virtual Insured Insured { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("DriverInformations")]
        public virtual State State { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.DriverInformationUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}
