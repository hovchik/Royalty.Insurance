using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class ReturnPremiumEndorsementViewModel
    {
        [JsonPropertyName("results")]
        public ReturnPremiumEndorsementResponse Results { get; set; }
    }
}
