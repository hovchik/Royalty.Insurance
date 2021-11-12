using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class SavedMarketingRequest
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string SavedRequest { get; set; }
        [StringLength(200)]
        public string ShortDescription { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDateUtc { get; set; }
        public int? Hash { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("SavedMarketingRequests")]
        public virtual User User { get; set; }
    }
}
