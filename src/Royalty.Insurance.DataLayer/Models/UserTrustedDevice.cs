using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Index(nameof(DeviceId), Name = "IX_UserTrustedDevices", IsUnique = true)]
    public partial class UserTrustedDevice
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string DeviceId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserTrustedDevices")]
        public virtual User User { get; set; }
    }
}
