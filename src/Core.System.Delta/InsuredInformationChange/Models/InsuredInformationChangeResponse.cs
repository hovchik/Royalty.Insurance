using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class InsuredInformationChangeResponse
    {
        [JsonPropertyName("responseReference")]
        public string ResponseReference { get; set; }

        [JsonPropertyName("processingStatus")]
        public string ProcessingStatus { get; set; }

        [JsonPropertyName("returnCode")]
        public string ReturnCode { get; set; }

        [JsonPropertyName("returnMessage")]
        public string ReturnMessage { get; set; }

        [JsonPropertyName("processedDateTime")]
        public string ProcessedDateTime { get; set; }

        [JsonPropertyName("processedDate")]
        public string ProcessedDate { get; set; }

        [JsonPropertyName("processedTime")]
        public string ProcessedTime { get; set; }

        [JsonPropertyName("premiumFinanceCompanyCode")]
        public string PremiumFinanceCompanyCode { get; set; }

        [JsonPropertyName("aisAccountNumber")]
        public string AisAccountNumber { get; set; }

        [JsonPropertyName("agentCodeCrossReference")]
        public string AgentCodeCrossReference { get; set; }

        [JsonPropertyName("agentCode")]
        public string AgentCode { get; set; }

        [JsonPropertyName("insuredId")]
        public string InsuredId { get; set; }
    }
}
