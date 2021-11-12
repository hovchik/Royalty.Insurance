using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("VehicleInfo")]
    public partial class VehicleInfo
    {
        public VehicleInfo()
        {
            InsuredVehicles = new HashSet<InsuredVehicle>();
        }

        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        [Required]
        [StringLength(50)]
        public string Make { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [Column("GVW")]
        public int Gvw { get; set; }
        public int ActualValue { get; set; }
        [Required]
        [StringLength(10)]
        public string Radius { get; set; }
        [Required]
        [Column("VIN")]
        [StringLength(50)]
        public string Vin { get; set; }
        [Column(TypeName = "ntext")]
        public string Comments { get; set; }
        public bool IsTruck { get; set; }

        [InverseProperty(nameof(InsuredVehicle.Vehicle))]
        public virtual ICollection<InsuredVehicle> InsuredVehicles { get; set; }
    }
}
