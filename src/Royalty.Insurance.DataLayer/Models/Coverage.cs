using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class Coverage
    {
        public Coverage()
        {
            InsuredCoverages = new HashSet<InsuredCoverage>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string CoverageType { get; set; }
        public int CoverageLimit { get; set; }

        [InverseProperty(nameof(InsuredCoverage.Coverage))]
        public virtual ICollection<InsuredCoverage> InsuredCoverages { get; set; }
    }
}
