
using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class AgentInformationChangeRequest
    {
        [JsonPropertyName("agentInfo")]
        public AgentInfo AgentInfo { get; set; }
    }
}
