using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class DeltaReturnPolicy
    {
        [JsonPropertyName("policyNumber")]
        public string PolicyNumber { get; set; }

        [JsonPropertyName("policyReturnPremiumAmount")]
        public string PolicyReturnPremiumAmount { get; set; }

        [JsonPropertyName("policyCommissionDueAmount")]
        public string PolicyCommissionDueAmount { get; set; }
    }
}
