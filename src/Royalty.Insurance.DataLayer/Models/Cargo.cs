using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("Cargo")]
    public partial class Cargo
    {
        public Cargo()
        {
            CargoCommodities = new HashSet<CargoCommodity>();
        }

        [Key]
        public int Id { get; set; }
        public int InsuredId { get; set; }
        public int CreateBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(User.CargoCreateByNavigations))]
        public virtual User CreateByNavigation { get; set; }
        [ForeignKey(nameof(InsuredId))]
        [InverseProperty("Cargos")]
        public virtual Insured Insured { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.CargoUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(CargoCommodity.Cargo))]
        public virtual ICollection<CargoCommodity> CargoCommodities { get; set; }
    }
}
