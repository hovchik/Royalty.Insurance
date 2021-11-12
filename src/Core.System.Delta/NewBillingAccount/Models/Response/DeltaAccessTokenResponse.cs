using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class DeltaAccessTokenResponse
    {
        [JsonPropertyName("results")]
        public string Results { get; set; }
    }
}
