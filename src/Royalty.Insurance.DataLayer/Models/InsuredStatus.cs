using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class InsuredStatus
    {
        public InsuredStatus()
        {
            Insureds = new HashSet<Insured>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(Insured.InsuredStatus))]
        public virtual ICollection<Insured> Insureds { get; set; }
    }
}
