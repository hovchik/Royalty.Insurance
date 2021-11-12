using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class AgentTaskType
    {
        public AgentTaskType()
        {
            AgentTasks = new HashSet<AgentTask>();
        }

        [Key]
        public byte Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(AgentTask.AgentTaskType))]
        public virtual ICollection<AgentTask> AgentTasks { get; set; }
    }
}
