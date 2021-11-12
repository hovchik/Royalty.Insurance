using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class BillingAccountPolicy
    {
        [JsonPropertyName("policySequenceNumber")]
        public string PolicySequenceNumber { get; set; }

        [JsonPropertyName("policyNumber")]
        public string PolicyNumber { get; set; }

        [JsonPropertyName("policyInceptionDate")]
        public string PolicyInceptionDate { get; set; }

        [JsonPropertyName("policyTerm")]
        public string PolicyTerm { get; set; }

        [JsonPropertyName("policyInsuranceCompanyNumber")]
        public string PolicyInsuranceCompanyNumber { get; set; }

        [JsonPropertyName("policyInsuranceCompanyName")]
        public string PolicyInsuranceCompanyName { get; set; }

        [JsonPropertyName("policyCoverageTypeCode")]
        public string PolicyCoverageTypeCode { get; set; }

        [JsonPropertyName("policyCoverageDescription")]
        public string PolicyCoverageDescription { get; set; }

        [JsonPropertyName("policyPremiumDescription")]
        public string PolicyPremiumDescription { get; set; }

        [JsonPropertyName("policyPremiumAmount")]
        public string PolicyPremiumAmount { get; set; }

        [JsonPropertyName("policyPremiumDownpay")]
        public string PolicyPremiumDownpay { get; set; }

        [JsonPropertyName("policyUnpaidPremiumAmount")]
        public string PolicyUnpaidPremiumAmount { get; set; }

        [JsonPropertyName("policyFee1Type")]
        public string PolicyFee1Type { get; set; }

        [JsonPropertyName("policyFee1Description")]
        public string PolicyFee1Description { get; set; }

        [JsonPropertyName("policyFee1Amount")]
        public string PolicyFee1Amount { get; set; }

        [JsonPropertyName("policyFee1AmountDownpay")]
        public string PolicyFee1AmountDownpay { get; set; }

        [JsonPropertyName("policyFee1UnpaidAmount")]
        public string PolicyFee1UnpaidAmount { get; set; }

        [JsonPropertyName("policyFee2Type")]
        public string PolicyFee2Type { get; set; }

        [JsonPropertyName("policyFee2Description")]
        public string PolicyFee2Description { get; set; }

        [JsonPropertyName("policyFee2Amount")]
        public string PolicyFee2Amount { get; set; }

        [JsonPropertyName("policyFee2AmountDownpay")]
        public string PolicyFee2AmountDownpay { get; set; }

        [JsonPropertyName("policyFee2UnpaidAmount")]
        public string PolicyFee2UnpaidAmount { get; set; }

        [JsonPropertyName("policyFee3Type")]
        public string PolicyFee3Type { get; set; }

        [JsonPropertyName("policyFee3Description")]
        public string PolicyFee3Description { get; set; }

        [JsonPropertyName("policyFee3Amount")]
        public string PolicyFee3Amount { get; set; }

        [JsonPropertyName("policyFee3AmountDownpay")]
        public string PolicyFee3AmountDownpay { get; set; }

        [JsonPropertyName("policyFee3UnpaidAmount")]
        public string PolicyFee3UnpaidAmount { get; set; }

        [JsonPropertyName("policyFee4Type")]
        public string PolicyFee4Type { get; set; }

        [JsonPropertyName("policyFee4Description")]
        public string PolicyFee4Description { get; set; }

        [JsonPropertyName("policyFee4Amount")]
        public string PolicyFee4Amount { get; set; }

        [JsonPropertyName("policyFee4AmountDownpay")]
        public string PolicyFee4AmountDownpay { get; set; }

        [JsonPropertyName("policyFee4UnpaidAmount")]
        public string PolicyFee4UnpaidAmount { get; set; }
    }
}
