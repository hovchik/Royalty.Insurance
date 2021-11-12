using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class DeltaBillingAccountViewModel
    {
        [JsonPropertyName("results")]
        public DeltaBillingAccountResponse Results { get; set; }
    }
}
