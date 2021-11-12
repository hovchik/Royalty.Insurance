using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("LossInformation")]
    public partial class LossInformation
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EffectiveDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpireDate { get; set; }
        [Required]
        [StringLength(50)]
        public string InsuranceName { get; set; }
        [StringLength(50)]
        public string LesseeName { get; set; }
        [Required]
        [StringLength(50)]
        public string PoliceNumber { get; set; }
        [Column("LesseeMCNumber")]
        [StringLength(50)]
        public string LesseeMcnumber { get; set; }
        [Required]
        [StringLength(50)]
        public string NumberOfClaims { get; set; }
        [Column(TypeName = "ntext")]
        public string Comments { get; set; }
        public int InsuredId { get; set; }

        [ForeignKey(nameof(InsuredId))]
        [InverseProperty("LossInformations")]
        public virtual Insured Insured { get; set; }
    }
}
