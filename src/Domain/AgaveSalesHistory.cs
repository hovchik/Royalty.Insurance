using System;

namespace Domain
{
    public class AgaveSalesHistory
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public int? AuthCode { get; set; }
        public int ResponseCode { get; set; }
        public string OrderId { get; set; }
        public string CvvResponseCode { get; set; }
        public string CreditCardScheme { get; set; }
        public int TransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        public string ProcessorMessage { get; set; }
        public string MerchantTransactionTime { get; set; }
        public string MerchantTransactionDate { get; set; }
        public int ReferenceNum { get; set; }
        public string ResponseMessage { get; set; }
        public string ProcessorCode { get; set; }
        public string AvsResponseCode { get; set; }
        public int? TransactionTimestamp { get; set; }
        public int AccountNumber { get; set; }
        public int ChargeTotal { get; set; }
        public int UserId { get; set; }
        public string CardHolderName { get; set; }
        public string CardHolderPhone { get; set; }
        public string CardHolderAddress { get; set; }
        public string CardHolderCity { get; set; }
        public string CardHolderState { get; set; }
        public string CardHolderEmail { get; set; }
        public int? CardHolderZip { get; set; }
        public DateTime CreateDateTimeUtc { get; set; }

        public AgaveTransactionType TransactionType { get; set; }
        public User User { get; set; }
    }
}
