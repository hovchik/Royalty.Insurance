using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("InsuredVehicle")]
    public partial class InsuredVehicle
    {
        [Key]
        public int Id { get; set; }
        public int InsuredId { get; set; }
        public int VehicleId { get; set; }

        [ForeignKey(nameof(InsuredId))]
        [InverseProperty("InsuredVehicles")]
        public virtual Insured Insured { get; set; }
        [ForeignKey(nameof(VehicleId))]
        [InverseProperty(nameof(VehicleInfo.InsuredVehicles))]
        public virtual VehicleInfo Vehicle { get; set; }
    }
}
