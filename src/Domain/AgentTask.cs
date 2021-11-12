using System;

namespace Domain
{
    public class AgentTask
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
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }
        public AgentTaskType AgentTaskType { get; set; }
        public AgentTaskStatus AgentTaskStatus { get; set; }
        public User Assignee { get; set; }
        public User CreatedByNavigation { get; set; }
        public Insured Insured { get; set; }
        public User UpdatedByNavigation { get; set; }
    }
}