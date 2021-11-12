
using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class AgentInfo
    {
        [JsonPropertyName("agentCodeCrossReference")]
        public string AgentCodeCrossReference { get; set; }

        [JsonPropertyName("agentCode")]
        public string AgentCode { get; set; }

        [JsonPropertyName("agentRequestType")]
        public string AgentRequestType { get; set; }

        [JsonPropertyName("agentNameChangeFlag")]
        public string AgentNameChangeFlag { get; set; }

        [JsonPropertyName("agentName")]
        public string AgentName { get; set; }

        [JsonPropertyName("agentAddressChangeFlag")]
        public string AgentAddressChangeFlag { get; set; }

        [JsonPropertyName("agentAddress")]
        public string AgentAddress { get; set; }

        [JsonPropertyName("agentCity")]
        public string AgentCity { get; set; }

        [JsonPropertyName("agentState")]
        public string AgentState { get; set; }

        [JsonPropertyName("agentZipCode")]
        public string AgentZipCode { get; set; }

        [JsonPropertyName("agentPhoneChangeFlag")]
        public string AgentPhoneChangeFlag { get; set; }

        [JsonPropertyName("agentPhoneNumber")]
        public string AgentPhoneNumber { get; set; }

        [JsonPropertyName("agentFaxChangeFlag")]
        public string AgentFaxChangeFlag { get; set; }

        [JsonPropertyName("agentFaxNumber")]
        public string AgentFaxNumber { get; set; }

        [JsonPropertyName("agentEmailChangeFlag")]
        public string AgentEmailChangeFlag { get; set; }

        [JsonPropertyName("agentEmailAddress")]
        public string AgentEmailAddress { get; set; }

        [JsonPropertyName("agentMailOptionChangeFlag")]
        public string AgentMailOptionChangeFlag { get; set; }

        [JsonPropertyName("agentMailOptionFlag")]
        public string AgentMailOptionFlag { get; set; }

        [JsonPropertyName("agentSSNFederalIdChangeFlag")]
        public string AgentSSNFederalIdChangeFlag { get; set; }

        [JsonPropertyName("agentSSNFederalIdTypeFlag")]
        public string AgentSSNFederalIdTypeFlag { get; set; }

        [JsonPropertyName("agentSSNFederalId")]
        public string AgentSSNFederalId { get; set; }
    }
}
