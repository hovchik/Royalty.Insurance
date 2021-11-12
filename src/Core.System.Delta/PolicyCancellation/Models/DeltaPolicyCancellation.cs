using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class DeltaPolicyCancellation
    {
        [JsonPropertyName("policyNumber")]
        public string PolicyNumber { get; set; }
    }
}
