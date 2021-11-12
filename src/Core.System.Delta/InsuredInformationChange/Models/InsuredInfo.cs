using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class InsuredInfo
    {
        [JsonPropertyName("insuredId")]
        public string InsuredId { get; set; }

        [JsonPropertyName("aisAccountNumber")]
        public string AisAccountNumber { get; set; }

        [JsonPropertyName("insuredNameChangeFlag")]
        public string InsuredNameChangeFlag { get; set; }

        [JsonPropertyName("insuredName1")]
        public string InsuredName1 { get; set; }

        [JsonPropertyName("insuredName2")]
        public string InsuredName2 { get; set; }

        [JsonPropertyName("insuredAddressChangeFlag")]
        public string InsuredAddressChangeFlag { get; set; }

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

        [JsonPropertyName("insuredPhoneChangeFlag")]
        public string InsuredPhoneChangeFlag { get; set; }

        [JsonPropertyName("insuredPhoneNumber")]
        public string InsuredPhoneNumber { get; set; }

        [JsonPropertyName("insuredFaxChangeFlag")]
        public string InsuredFaxChangeFlag { get; set; }

        [JsonPropertyName("insuredFaxNumber")]
        public string InsuredFaxNumber { get; set; }

        [JsonPropertyName("insuredEmailChangeFlag")]
        public string InsuredEmailChangeFlag { get; set; }

        [JsonPropertyName("insuredEmail")]
        public string InsuredEmail { get; set; }

        [JsonPropertyName("insuredCellPhoneChangeFlag")]
        public string InsuredCellPhoneChangeFlag { get; set; }

        [JsonPropertyName("insuredCellPhoneServiceProviderCode")]
        public string InsuredCellPhoneServiceProviderCode { get; set; }

        [JsonPropertyName("insuredCellPhoneNumber")]
        public string InsuredCellPhoneNumber { get; set; }

        [JsonPropertyName("insuredBillMailOptionChangeFlag")]
        public string InsuredBillMailOptionChangeFlag { get; set; }

        [JsonPropertyName("insuredBillMailOption")]
        public string InsuredBillMailOption { get; set; }

        [JsonPropertyName("insuredBillReminderOptionChangeFlag")]
        public string InsuredBillReminderOptionChangeFlag { get; set; }

        [JsonPropertyName("insuredBillReminderOption")]
        public string InsuredBillReminderOption { get; set; }
    }
}
