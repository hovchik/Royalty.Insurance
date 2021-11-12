using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("UserStatus")]
    public partial class UserStatus
    {
        public UserStatus()
        {
            UsersProfileUserLastStatuses = new HashSet<UsersProfile>();
            UsersProfileUserStatuses = new HashSet<UsersProfile>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(UsersProfile.UserLastStatus))]
        public virtual ICollection<UsersProfile> UsersProfileUserLastStatuses { get; set; }
        [InverseProperty(nameof(UsersProfile.UserStatus))]
        public virtual ICollection<UsersProfile> UsersProfileUserStatuses { get; set; }
    }
}
