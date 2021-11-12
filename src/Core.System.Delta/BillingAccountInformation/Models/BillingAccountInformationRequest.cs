using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class BillingAccountInformationRequest
    {
        [JsonPropertyName("aisAccountNumber")]
        public string AisAccountNumber { get; set; }

        [JsonPropertyName("insuredId")]
        public string InsuredId { get; set; }

        [JsonPropertyName("policyNumber")]
        public string PolicyNumber { get; set; }

        [JsonPropertyName("policyFlag")]
        public string PolicyFlag { get; set; }
    }
}
