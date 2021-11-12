using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class AgentInformationChangeViewModel
    {
        [JsonPropertyName("results")]
        public AgentInformationChangeRequest Results { get; set; }
    }
}
