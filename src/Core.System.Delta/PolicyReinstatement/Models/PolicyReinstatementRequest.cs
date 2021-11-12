using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class PolicyReinstatementRequest
    {
        [JsonPropertyName("insuredId")]
        public string InsuredId { get; set; }

        [JsonPropertyName("aisAccountNumber")]
        public string AisAccountNumber { get; set; }

        [JsonPropertyName("note1")]
        public string Note1 { get; set; }

        [JsonPropertyName("note2")]
        public string Note2 { get; set; }

        [JsonPropertyName("agentCodeCrossReference")]
        public string AgentCodeCrossReference { get; set; }

        [JsonPropertyName("agentCode")]
        public string AgentCode { get; set; }

        [JsonPropertyName("reinstateDate")]
        public string ReinstateDate { get; set; }

        [JsonPropertyName("reinstateType")]
        public string ReinstateType { get; set; }

        [JsonPropertyName("requestedBy")]
        public string RequestedBy { get; set; }

        [JsonPropertyName("policies")]
        public List<DeltaPolicyCancellation> Policies { get; set; }
    }
}
