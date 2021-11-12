using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("_DatabaseVersion")]
    public partial class DatabaseVersion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(16)]
        public string DbVersion { get; set; }
    }
}
