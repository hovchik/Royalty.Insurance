using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Index(nameof(Name), Name = "Unique_name", IsUnique = true)]
    public partial class Group
    {
        public Group()
        {
            GroupMembers = new HashSet<GroupMember>();
            Messages = new HashSet<Message>();
            UnreadMessages = new HashSet<UnreadMessage>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public bool Active { get; set; }
        public byte GroupTypeId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.GroupCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.GroupUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(GroupMember.Group))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        [InverseProperty(nameof(Message.RecipientGroup))]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty(nameof(UnreadMessage.Group))]
        public virtual ICollection<UnreadMessage> UnreadMessages { get; set; }
    }
}
