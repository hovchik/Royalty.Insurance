using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class AgentTask
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(1024)]
        public string Description { get; set; }
        public int? AssigneeId { get; set; }
        public int AgentTaskStatusId { get; set; }
        public byte AgentTaskTypeId { get; set; }
        [StringLength(255)]
        public string CanceledReason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDatetimeUtc { get; set; }
        public int? InsuredId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }

        [ForeignKey(nameof(AgentTaskStatusId))]
        [InverseProperty("AgentTasks")]
        public virtual AgentTaskStatus AgentTaskStatus { get; set; }
        [ForeignKey(nameof(AgentTaskTypeId))]
        [InverseProperty("AgentTasks")]
        public virtual AgentTaskType AgentTaskType { get; set; }
        [ForeignKey(nameof(AssigneeId))]
        [InverseProperty(nameof(User.AgentTaskAssignees))]
        public virtual User Assignee { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        [InverseProperty(nameof(User.AgentTaskCreatedByNavigations))]
        public virtual User CreatedByNavigation { get; set; }
        [ForeignKey(nameof(InsuredId))]
        [InverseProperty("AgentTasks")]
        public virtual Insured Insured { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.AgentTaskUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
    }
}
