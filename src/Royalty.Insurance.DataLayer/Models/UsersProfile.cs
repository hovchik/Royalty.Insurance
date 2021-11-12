using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class UsersProfile
    {
        [Key]
        public int Id { get; set; }
        public int UserStatusId { get; set; }
        public int UserLastStatusId { get; set; }
        [StringLength(50)]
        public string Status { get; set; }

        [ForeignKey(nameof(Id))]
        [InverseProperty(nameof(User.UsersProfile))]
        public virtual User IdNavigation { get; set; }
        [ForeignKey(nameof(UserLastStatusId))]
        [InverseProperty("UsersProfileUserLastStatuses")]
        public virtual UserStatus UserLastStatus { get; set; }
        [ForeignKey(nameof(UserStatusId))]
        [InverseProperty("UsersProfileUserStatuses")]
        public virtual UserStatus UserStatus { get; set; }
    }
}
