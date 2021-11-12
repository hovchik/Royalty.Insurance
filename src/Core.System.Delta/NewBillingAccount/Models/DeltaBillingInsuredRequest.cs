using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class DeltaBillingInsuredRequest
    {
        [JsonPropertyName("insuredId")]
        public string InsuredId { get; set; }

        [JsonPropertyName("insuredCANumber")]
        public string InsuredCANumber { get; set; }

        [JsonPropertyName("insuredMCNumber")]
        public string InsuredMCNumber { get; set; }

        [JsonPropertyName("aisAccountNumber")]
        public string AisAccountNumber { get; set; }

        [JsonPropertyName("insuredName1")]
        public string InsuredName1 { get; set; }

        [JsonPropertyName("insuredName2")]
        public string InsuredName2 { get; set; }

        [JsonPropertyName("insuredAddress1")]
        public string InsuredAddress1 { get; set; }

        [JsonPropertyName("insuredAddress2")]
        public string InsuredAddress2 { get; set; }

        [JsonPropertyName("insuredCity")]
        public string InsuredCity { get; set; }

        [JsonPropertyName("insuredState")]
        public string InsuredState { get; set; }

        [JsonPropertyName("insuredZip")]
        public string InsuredZip { get; set; }

        [JsonPropertyName("insuredPhoneNumber")]
        public string InsuredPhoneNumber { get; set; }

        [JsonPropertyName("insuredFaxNumber")]
        public string InsuredFaxNumber { get; set; }

        [JsonPropertyName("insuredEmail")]
        public string InsuredEmail { get; set; }

        [JsonPropertyName("insuredCellPhoneServiceProviderCode")]
        public string InsuredCellPhoneServiceProviderCode { get; set; }

        [JsonPropertyName("insuredCellPhoneNumber")]
        public string InsuredCellPhoneNumber { get; set; }

        [JsonPropertyName("insuredBillMailOption")]
        public string InsuredBillMailOption { get; set; }

        [JsonPropertyName("insuredBillReminderOption")]
        public string InsuredBillReminderOption { get; set; }

        [JsonPropertyName("bankAccountType")]
        public string BankAccountType { get; set; }

        [JsonPropertyName("bankABANumber")]
        public string BankABANumber { get; set; }

        [JsonPropertyName("bankAccountNumber")]
        public string BankAccountNumber { get; set; }

        [JsonPropertyName("insuredSSNFederalId")]
        public string InsuredSSNFederalId { get; set; }
    }
}
