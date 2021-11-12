using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class AgentTaskStatus
    {
        public AgentTaskStatus()
        {
            AgentTasks = new HashSet<AgentTask>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }

        [InverseProperty(nameof(AgentTask.AgentTaskStatus))]
        public virtual ICollection<AgentTask> AgentTasks { get; set; }
    }
}
