using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Royalty.Insurance.Proxy.Request
{
    public class Verification
    {
        [JsonPropertyName("merchantId")]
        public int MerchantId { get; set; }

        [JsonPropertyName("merchantKey")]
        public string MerchantKey { get; set; }
    }

    public class Shipping
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("address2")]
        public string Address2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("postalcode")]
        public int Postalcode { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }

    public class Billing
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("address2")]
        public string Address2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("postalcode")]
        public int Postalcode { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }

    public class Payment
    {
        [JsonPropertyName("chargeTotal")]
        public double ChargeTotal { get; set; }

        [JsonPropertyName("shippingTotal")]
        public double ShippingTotal { get; set; }

        [JsonPropertyName("salesTaxTotal")]
        public double SalesTaxTotal { get; set; }

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }
    }

    public class CreditCard
    {
        [JsonPropertyName("number")]
        public long Number { get; set; }

        [JsonPropertyName("expMonth")]
        public int ExpMonth { get; set; }

        [JsonPropertyName("expYear")]
        public int ExpYear { get; set; }

        [JsonPropertyName("cvvNumber")]
        public int CvvNumber { get; set; }

        [JsonPropertyName("cvvInd")]
        public string CvvInd { get; set; }

        [JsonPropertyName("track1Data")]
        public string Track1Data { get; set; }

        [JsonPropertyName("track2Data")]
        public string Track2Data { get; set; }

        [JsonPropertyName("signatureImage")]
        public string SignatureImage { get; set; }

        [JsonPropertyName("eCommInd")]
        public string ECommInd { get; set; }
    }

    public class PayType
    {
        [JsonPropertyName("creditCard")]
        public CreditCard CreditCard { get; set; }
    }

    public class TransactionDetail
    {
        [JsonPropertyName("payType")]
        public PayType PayType { get; set; }
    }

    public class Sale
    {
        //[JsonPropertyName("shipping")]
        //public Shipping Shipping { get; set; }

        [JsonPropertyName("billing")]
        public Billing Billing { get; set; }

        [JsonPropertyName("payment")]
        public Payment Payment { get; set; }

        [JsonPropertyName("transactionDetail")]
        public TransactionDetail TransactionDetail { get; set; }

        [JsonPropertyName("referenceNum")]
        public int ReferenceNum { get; set; }

        [JsonPropertyName("invoiceNumber")]
        public int InvoiceNumber { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }
    }

    public class Order
    {
        [JsonPropertyName("sale")]
        public Sale Sale { get; set; }
    }

    public class TransactionRequest: RoyaltyTransactionRequest
    {
        [JsonPropertyName("verification")]
        public Verification Verification { get; set; }

    }


    public class AgaveSaleRequest
    {
        [JsonPropertyName("transaction-request")]
        public TransactionRequest TransactionRequest { get; set; }
    }

    public class AgaveRoyaltySaleRequest
    {
        public RoyaltyTransactionRequest TransactionRequest { get; set; }
    }


    public class RoyaltyTransactionRequest
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
        [JsonPropertyName("order")]
        public Order Order { get; set; }
    }
}