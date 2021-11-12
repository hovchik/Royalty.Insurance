using System.Collections.Generic;

namespace Domain
{
    public class AgentTaskType
    {
        public AgentTaskType()
        {
            AgentTasks = new HashSet<AgentTask>();
        }

        
        public byte Id { get; set; }
        
        
        public string Name { get; set; }

        public ICollection<AgentTask> AgentTasks { get; set; }
    }
}
