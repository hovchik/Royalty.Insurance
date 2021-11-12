using System.Text.Json.Serialization;

namespace Core.System.Delta
{
    public class DeltaAccount
    {
        [JsonPropertyName("accountType")]
        public string AccountType { get; set; }

        [JsonPropertyName("calculateQuoteFlag")]
        public string CalculateQuoteFlag { get; set; }

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

        [JsonPropertyName("quarterlyPaymentFlag")]
        public string QuarterlyPaymentFlag { get; set; }

        [JsonPropertyName("paymentTerm")]
        public string PaymentTerm { get; set; }

        [JsonPropertyName("paymentAmount")]
        public string PaymentAmount { get; set; }

        [JsonPropertyName("firstPaymentDate")]
        public string FirstPaymentDate { get; set; }

        [JsonPropertyName("originalAPR")]
        public string OriginalAPR { get; set; }

        [JsonPropertyName("currentAPR")]
        public string CurrentAPR { get; set; }

        [JsonPropertyName("brokerFeeAddonFlag")]
        public string BrokerFeeAddonFlag { get; set; }

        [JsonPropertyName("brokerFeeAddonAmount")]
        public string BrokerFeeAddonAmount { get; set; }

        [JsonPropertyName("policyBrokerFees")]
        public string PolicyBrokerFees { get; set; }

        [JsonPropertyName("paymentReceived")]
        public string PaymentReceived { get; set; }

        [JsonPropertyName("spanishFlag")]
        public string SpanishFlag { get; set; }

        [JsonPropertyName("floridaAccountFlag")]
        public string FloridaAccountFlag { get; set; }

        [JsonPropertyName("stampTax")]
        public string StampTax { get; set; }

        [JsonPropertyName("quoteNumber")]
        public string QuoteNumber { get; set; }

        [JsonPropertyName("signatureReceived")]
        public string SignatureReceived { get; set; }

        [JsonPropertyName("billingFee")]
        public string BillingFee { get; set; }

        [JsonPropertyName("billingType")]
        public string BillingType { get; set; }
    }
}
