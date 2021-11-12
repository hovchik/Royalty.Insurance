using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class PolicyReinstatementViewModel
    {
        [JsonPropertyName("results")]
        public PolicyReinstatementResponse Results { get; set; }
    }
}
