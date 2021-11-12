using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class BillingAccountInformationViewModel
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

        [JsonPropertyName("totalPremiumAmount")]
        public string TotalPremiumAmount { get; set; }

        [JsonPropertyName("totalDownpayAmount")]
        public string TotalDownpayAmount { get; set; }

        [JsonPropertyName("totalAccountBalanceAmount")]
        public string TotalAccountBalanceAmount { get; set; }

        [JsonPropertyName("paymentTerm")]
        public string PaymentTerm { get; set; }

        [JsonPropertyName("paymentAmount")]
        public string PaymentAmount { get; set; }

        [JsonPropertyName("billingAccountSetupDate")]
        public string BillingAccountSetupDate { get; set; }

        [JsonPropertyName("nextPaymentDueDate")]
        public string NextPaymentDueDate { get; set; }

        [JsonPropertyName("nextPaymentDueAmount")]
        public string NextPaymentDueAmount { get; set; }

        [JsonPropertyName("installmentBillingServiceFee")]
        public string InstallmentBillingServiceFee { get; set; }

        [JsonPropertyName("policies")]
        public List<BillingAccountPolicy> Policies { get; set; }
    }
}
