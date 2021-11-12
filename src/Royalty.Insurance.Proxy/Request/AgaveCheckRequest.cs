using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Royalty.Insurance.Proxy.Request
{
    public class ClientData
    {
        [JsonPropertyName("comments")]
        public string Comments { get; set; }
    }

    public class Ach
    {
        [JsonPropertyName("achAccountType")]
        public string AchAccountType { get; set; }

        [JsonPropertyName("bankRoutingNumber")]
        public int BankRoutingNumber { get; set; }

        [JsonPropertyName("achAccountNumber")]
        public int AchAccountNumber { get; set; }

        [JsonPropertyName("achPaymentType")]
        public string AchPaymentType { get; set; }

        [JsonPropertyName("bankName")]
        public string BankName { get; set; }

        [JsonPropertyName("bankState")]
        public string BankState { get; set; }

        [JsonPropertyName("checkNumber")]
        public int CheckNumber { get; set; }

        [JsonPropertyName("MICR")]
        public string MICR { get; set; }

        [JsonPropertyName("achBusinessName")]
        public string AchBusinessName { get; set; }
    }

    public class eCheckPayType
    {
        [JsonPropertyName("ach")]
        public Ach Ach { get; set; }
    }

    public class eCheckTransactionDetail
    {
        [JsonPropertyName("payType")]
        public eCheckPayType PayType { get; set; }
    }

    public class eCheckSale
    {
        [JsonPropertyName("billing")]
        public Billing Billing { get; set; }

        [JsonPropertyName("payment")]
        public Payment Payment { get; set; }

        [JsonPropertyName("transactionDetail")]
        public eCheckTransactionDetail TransactionDetail { get; set; }

        [JsonPropertyName("referenceNum")]
        public int ReferenceNum { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }
    }

    public class eCheckOrder
    {
        [JsonPropertyName("clientData")]
        public ClientData ClientData { get; set; }

        [JsonPropertyName("sale")]
        public eCheckSale Sale { get; set; }
    }

    public class eCheckTransactionRequest : RoyaltyCheckTransactionRequest
    {
        [JsonPropertyName("verification")]
        public Verification Verification { get; set; }
    }

    public class AgaveCheckRequest
    {
        [JsonPropertyName("transaction-request")]
        public eCheckTransactionRequest TransactionRequest { get; set; }
    }

    public class AgaveRoyaltyCheckRequest
    {
        public RoyaltyCheckTransactionRequest TransactionRequest { get; set; }
    }

    public class RoyaltyCheckTransactionRequest
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("order")]
        public eCheckOrder Order { get; set; }
    }
}