using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("UserPhoneCallHistory")]
    public partial class UserPhoneCallHistory
    {
        [Key]
        public long Id { get; set; }
        public int UserPhoneId { get; set; }
        public int InitialCallTypeId { get; set; }
        public int CurrentCallTypeId { get; set; }
        [Required]
        [StringLength(15)]
        public string CallerNumber { get; set; }
        [Required]
        [StringLength(15)]
        public string CallId { get; set; }
        public int Extension { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDatetimeUtc { get; set; }
        [StringLength(50)]
        public string CallerName { get; set; }

        [ForeignKey(nameof(CurrentCallTypeId))]
        [InverseProperty(nameof(CallType.UserPhoneCallHistoryCurrentCallTypes))]
        public virtual CallType CurrentCallType { get; set; }
        [ForeignKey(nameof(InitialCallTypeId))]
        [InverseProperty(nameof(CallType.UserPhoneCallHistoryInitialCallTypes))]
        public virtual CallType InitialCallType { get; set; }
        [ForeignKey(nameof(UserPhoneId))]
        [InverseProperty(nameof(User.UserPhoneCallHistories))]
        public virtual User UserPhone { get; set; }
    }
}
