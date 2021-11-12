
using System;

namespace Royalty.Insurance.Proxy.Response
{
    public class AgentTaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AssigneeId { get; set; }
        public int AgentTaskStatusId { get; set; }
        public byte AgentTaskTypeId { get; set; }
        public string CanceledReason { get; set; }
        public DateTime? DueDatetimeUtc { get; set; }
        public DateTime? CompletedDatetimeUtc { get; set; }
        public int? InsuredId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
