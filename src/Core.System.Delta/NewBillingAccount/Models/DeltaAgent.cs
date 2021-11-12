using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class DeltaAgent
    {
        [JsonPropertyName("agentCodeCrossReference")]
        public string AgentCodeCrossReference { get; set; }

        [JsonPropertyName("agentCode")]
        public string AgentCode { get; set; }

        [JsonPropertyName("agentName")]
        public string AgentName { get; set; }

        [JsonPropertyName("generalAgentName")]
        public string GeneralAgentName { get; set; }
    }
}
