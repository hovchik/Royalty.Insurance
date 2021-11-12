using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Index(nameof(GroupId), nameof(MemberId), Name = "IX_GroupMembers", IsUnique = true)]
    public partial class GroupMember
    {
        [Key]
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int MemberId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Required]
        public bool? Active { get; set; }
        public bool Muted { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupMembers")]
        public virtual Group Group { get; set; }
        [ForeignKey(nameof(MemberId))]
        [InverseProperty(nameof(User.GroupMembers))]
        public virtual User Member { get; set; }
    }
}
