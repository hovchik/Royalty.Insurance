using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class UserPhone
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(15)]
        public string IpAddress { get; set; }
        public int PhoneOwnerId { get; set; }
        public int Extension { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.UserPhoneCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(PhoneOwnerId))]
        [InverseProperty(nameof(User.UserPhonePhoneOwners))]
        public virtual User PhoneOwner { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.UserPhoneUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}
