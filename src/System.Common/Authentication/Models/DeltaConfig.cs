
namespace System.Common.Authentication.Models
{
    public class DeltaConfig
    {
        public string ApiKey { get; set; }
        public string ApiKeyValue { get; set; }
        public string SecretKey { get; set; }
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public string BaseUrl { get; set; }
        public string TransactionUrl { get; set; }
        public string AuthorizationTokenUrl { get; set; }
        public string AuthorizationLoginUrl { get; set; }
        public string NewBillingAccountUrl { get; set; }
        public string AdditionalPremiumEndorsementUrl { get; set; }
        public string ReturnPremiumEndorsementUrl { get; set; }
        public string PolicyCancellationUrl { get; set; }
        public string PolicyReinstatementUrl { get; set; }
        public string BillingAccountInformationUrl { get; set; }
        public string InsuredInformationChangeUrl { get; set; }
        public string AgentInformationChange { get; set; }
    }
}
