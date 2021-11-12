using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class PremiumEndorsementViewModel
    {
        [JsonPropertyName("results")]
        public PremiumEndorsementResponse Results { get; set; }
    }
}
