using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("BasicAlert")]
    public partial class BasicAlert
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("BasicAlert")]
        [StringLength(30)]
        public string BasicAlert1 { get; set; }
    }
}
