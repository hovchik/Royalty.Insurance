using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("OperationType")]
    public partial class OperationType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string Type { get; set; }
        [Required]
        [StringLength(15)]
        public string Value { get; set; }
    }
}
