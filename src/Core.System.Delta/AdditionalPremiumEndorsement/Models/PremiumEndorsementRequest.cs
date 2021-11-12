using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.System.Delta.Common;

namespace Core.System.Delta
{
    public class PremiumEndorsementRequest
    {
        [JsonPropertyName("insuredId")]
        public string InsuredId { get; set; }

        [JsonPropertyName("insuredName1")]
        public string InsuredName1 { get; set; }

        [JsonPropertyName("aisAccountNumber")]
        public string AisAccountNumber { get; set; }

        [JsonPropertyName("note1")]
        public string Note1 { get; set; }

        [JsonPropertyName("note2")]
        public string Note2 { get; set; }

        [JsonPropertyName("policies")]
        public List<DeltaPolicy> Policies { get; set; }
    }

}
