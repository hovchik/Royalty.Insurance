using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Royalty.Insurance.Proxy.Request
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class PaymentRefund
    {
        [JsonPropertyName("chargeTotal")]
        public double ChargeTotal { get; set; }
    }

    public class Return
    {
        [JsonPropertyName("orderID")]
        public string OrderID { get; set; }

        [JsonPropertyName("referenceNum")]
        public string ReferenceNum { get; set; }

        [JsonPropertyName("payment")]
        public PaymentRefund Payment { get; set; }
    }

    public class OrderRefund
    {
        [JsonPropertyName("return")]
        public Return Return { get; set; }
    }

    public class TransactionRequestRefund : TransactionRoyaltyRequestRefund
    {
        [JsonPropertyName("verification")]
        public Verification Verification { get; set; }
    }

    public class AgaveRefundRequest
    {
        [JsonPropertyName("transaction-request")]
        public TransactionRequestRefund TransactionRequest { get; set; }
    }

    public class AgaveRoyaltyRefundRequest
    {
        public TransactionRoyaltyRequestRefund TransactionRequest { get; set; }
    }

    public class TransactionRoyaltyRequestRefund
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("order")]
        public OrderRefund Order { get; set; }
    }
}