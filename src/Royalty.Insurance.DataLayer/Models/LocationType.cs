using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("LocationType")]
    public partial class LocationType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("LocationType")]
        [StringLength(10)]
        public string LocationType1 { get; set; }
    }
}
