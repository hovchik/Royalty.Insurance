using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class InsuredCoverage
    {
        [Key]
        public int Id { get; set; }
        public int InsuredId { get; set; }
        public int CoverageId { get; set; }
        public int Limit { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }

        [ForeignKey(nameof(CoverageId))]
        [InverseProperty("InsuredCoverages")]
        public virtual Coverage Coverage { get; set; }
        [ForeignKey(nameof(InsuredId))]
        [InverseProperty("InsuredCoverages")]
        public virtual Insured Insured { get; set; }
    }
}
