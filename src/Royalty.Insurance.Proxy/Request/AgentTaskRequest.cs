
using System;
using System.ComponentModel.DataAnnotations;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Proxy.Request
{
    public class AgentTaskRequest
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        public int? AssigneeId { get; set; }

        [Required]
        public int AgentTaskStatusId { get; set; }

        [Range((int)AgentTaskTypeCode.Endorsement, (int)AgentTaskTypeCode.Renew)]
        public byte AgentTaskTypeId { get; set; }

        [StringLength(255)]
        public string CanceledReason { get; set; }
        
        public DateTime? DueDatetimeUtc { get; set; }

        public DateTime? CompletedDatetimeUtc { get; set; }

        public int? InsuredId { get; set; }
    }
}
