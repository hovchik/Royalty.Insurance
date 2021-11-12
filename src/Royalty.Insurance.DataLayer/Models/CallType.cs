using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class CallType
    {
        public CallType()
        {
            UserPhoneCallHistoryCurrentCallTypes = new HashSet<UserPhoneCallHistory>();
            UserPhoneCallHistoryInitialCallTypes = new HashSet<UserPhoneCallHistory>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(UserPhoneCallHistory.CurrentCallType))]
        public virtual ICollection<UserPhoneCallHistory> UserPhoneCallHistoryCurrentCallTypes { get; set; }
        [InverseProperty(nameof(UserPhoneCallHistory.InitialCallType))]
        public virtual ICollection<UserPhoneCallHistory> UserPhoneCallHistoryInitialCallTypes { get; set; }
    }
}
