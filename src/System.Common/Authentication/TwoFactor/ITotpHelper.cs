using System.Common.Authentication.Models;

namespace System.Common.Authentication.TwoFactor
{
    public interface ITotpHelper
    {
        string GenerateSecret(string key, long salting);
        TotpResult GenerateQrImage(string appName, string userName, string secret);
        bool Validate(string secret, string clientTotp);
    }
}
