using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("Gvwr")]
    public partial class Gvwr
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        public string ClassDescription { get; set; }
    }
}
