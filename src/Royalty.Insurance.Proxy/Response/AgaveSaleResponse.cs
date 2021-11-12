using System;
using System.Text.Json.Serialization;

namespace Royalty.Insurance.Proxy.Response
{
    public class TransactionResponse
    {
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("authCode")]
        public int? AuthCode { get; set; }

        [JsonPropertyName("responseCode")]
        public int ResponseCode { get; set; }

        [JsonPropertyName("orderID")]
        public string OrderID { get; set; }

        [JsonPropertyName("cvvResponseCode")]
        public string CvvResponseCode { get; set; }

        [JsonPropertyName("creditCardScheme")]
        public string CreditCardScheme { get; set; }

        [JsonPropertyName("merchantTransactionTime")]
        public string MerchantTransactionTime { get; set; }

        [JsonPropertyName("merchantTransactionDate")]
        public string MerchantTransactionDate { get; set; }

        [JsonPropertyName("transactionID")]
        public int TransactionID { get; set; }

        [JsonPropertyName("processorMessage")]
        public string ProcessorMessage { get; set; }

        [JsonPropertyName("avsResponseCode")]
        public string AvsResponseCode { get; set; }

        [JsonPropertyName("referenceNum")]
        public int ReferenceNum { get; set; }

        [JsonPropertyName("cardholderName")]
        public string CardholderName { get; set; }

        [JsonPropertyName("responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonPropertyName("accountNumber")]
        public int AccountNumber { get; set; }

        [JsonPropertyName("creditCardCountry")]
        public string CreditCardCountry { get; set; }

        [JsonPropertyName("chargeTotal")]
        public int ChargeTotal { get; set; }

        [JsonPropertyName("processorCode")]
        public string ProcessorCode { get; set; }

        [JsonPropertyName("transactionTimestamp")]
        public int TransactionTimestamp { get; set; }
    }

    public class AgaveSaleResponse
    {
        [JsonPropertyName("transaction-response")]
        public TransactionResponse TransactionResponse { get; set; }
    }

    public class AgaveRoyaltyResponse : TransactionResponse
    {
        public int UserId { get; set; }
        public string CardHolderPhone { get; set; }
        public string CardHolderAddress { get; set; }
        public string CardHolderCity { get; set; }
        public string CardHolderState { get; set; }
        public string CardHolderEmail { get; set; }
        public int CardHolderZip { get; set; }
        public DateTime CreateDateTimeUtc { get; set; }
    }

}