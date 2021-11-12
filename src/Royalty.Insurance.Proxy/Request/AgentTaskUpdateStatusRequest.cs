
using System.ComponentModel.DataAnnotations;

namespace Royalty.Insurance.Proxy.Request
{
    public class AgentTaskUpdateStatusRequest
    {
        [Required]
        public int AgentTaskStatusId { get; set; }

    }
}
