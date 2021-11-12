using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class DeltaBillingAccountResponse
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

        [JsonPropertyName("totalPremiumAmount")]
        public string TotalPremiumAmount { get; set; }

        [JsonPropertyName("totalDownPayAmount")]
        public string TotalDownPayAmount { get; set; }

        [JsonPropertyName("amountFinanced")]
        public string AmountFinanced { get; set; }

        [JsonPropertyName("financeCharge")]
        public string FinanceCharge { get; set; }

        [JsonPropertyName("totalOfPayments")]
        public string TotalOfPayments { get; set; }

        [JsonPropertyName("numberOfInstallments")]
        public string NumberOfInstallments { get; set; }

        [JsonPropertyName("paymentAmount")]
        public string PaymentAmount { get; set; }

        [JsonPropertyName("billingAccountSetupDate")]
        public string BillingAccountSetupDate { get; set; }

        [JsonPropertyName("firstPaymentDueDate")]
        public string FirstPaymentDueDate { get; set; }

        [JsonPropertyName("nextPaymentDueDate")]
        public string NextPaymentDueDate { get; set; }

        [JsonPropertyName("nextPaymentDueAmount")]
        public string NextPaymentDueAmount { get; set; }

        [JsonPropertyName("installmentBillingServiceFee")]
        public string InstallmentBillingServiceFee { get; set; }

        [JsonPropertyName("originalAPR")]
        public string OriginalAPR { get; set; }

        [JsonPropertyName("currentAPR")]
        public string CurrentAPR { get; set; }

        [JsonPropertyName("stampTax")]
        public string StampTax { get; set; }

        [JsonPropertyName("processingReportLocation")]
        public string ProcessingReportLocation { get; set; }

        [JsonPropertyName("premiumFinanceAgreement")]
        public string PremiumFinanceAgreement { get; set; }

        [JsonPropertyName("cdAgreement")]
        public string CdAgreement { get; set; }

        [JsonPropertyName("producerQuote")]
        public string ProducerQuote { get; set; }
    }
}
