using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("CargoCommodity")]
    public partial class CargoCommodity
    {
        [Key]
        public int Id { get; set; }
        public int CargoId { get; set; }
        public int CommodityId { get; set; }

        [ForeignKey(nameof(CargoId))]
        [InverseProperty("CargoCommodities")]
        public virtual Cargo Cargo { get; set; }
        [ForeignKey(nameof(CommodityId))]
        [InverseProperty("CargoCommodities")]
        public virtual Commodity Commodity { get; set; }
    }
}
