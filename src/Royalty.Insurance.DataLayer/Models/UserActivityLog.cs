using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class UserActivityLog
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid SessionId { get; set; }
        [Required]
        [StringLength(50)]
        public string DeviceIp { get; set; }
        [Required]
        [StringLength(64)]
        public string RefreshToken { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RefreshTokenExpireAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LogInDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LogOutDatetimeUtc { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserActivityLogs")]
        public virtual User User { get; set; }
    }
}
