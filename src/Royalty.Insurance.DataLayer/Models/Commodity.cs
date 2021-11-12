using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("Commodity")]
    public partial class Commodity
    {
        public Commodity()
        {
            CargoCommodities = new HashSet<CargoCommodity>();
        }

        [Key]
        public int Id { get; set; }
        public int CommodityValue { get; set; }
        public int CommodityPercent { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public int CreateBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(User.CommodityCreateByNavigations))]
        public virtual User CreateByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.CommodityUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(CargoCommodity.Commodity))]
        public virtual ICollection<CargoCommodity> CargoCommodities { get; set; }
    }
}
