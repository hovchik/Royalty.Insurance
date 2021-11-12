
namespace System.Common.Authentication.Models
{
    public class TotpResult
    {
        public string QrCodeImageUrl { get; }
        public string ManualSetUpKey { get; }

        public TotpResult(string qrCodeImageUrl, string manualSetUpKey)
        {
            QrCodeImageUrl = qrCodeImageUrl;
            ManualSetUpKey = manualSetUpKey;
        }
    }
}
