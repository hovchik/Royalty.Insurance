using System;
using System.Collections.Generic;


namespace Domain
{
    public class AgentTaskStatus
    {
        public AgentTaskStatus()
        {
            AgentTasks = new HashSet<AgentTask>();
        }

        
        public int Id { get; set; }
        
        
        public string Name { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }

        public ICollection<AgentTask> AgentTasks { get; set; }
    }
}
