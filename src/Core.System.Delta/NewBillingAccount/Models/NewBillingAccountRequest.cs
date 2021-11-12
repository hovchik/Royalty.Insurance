using System.Collections.Generic;
using System.Text.Json.Serialization;
using Core.System.Delta.Common;

namespace Core.System.Delta
{
    public class NewBillingAccountRequest
    {
        [JsonPropertyName("insured")]
        public DeltaBillingInsuredRequest Insured { get; set; }

        [JsonPropertyName("agent")]
        public DeltaAgent Agent { get; set; }

        [JsonPropertyName("account")]
        public DeltaAccount Account { get; set; }

        [JsonPropertyName("policies")]
        public List<DeltaPolicy> Policies { get; set; }
    }
}
