using System.Common.Authentication.Models;
using System.Common.Exceptions;
using AspNetCore.Totp.Interface;
using Core.System.Security.Cryptography;
using Royalty.Insurance.Settings;

namespace System.Common.Authentication.TwoFactor
{
    public class TotpHelper : ITotpHelper
    {
        private readonly ITotpSetupGenerator _totpSetUpGenerator;
        private readonly ITotpValidator _totpValidator;

        public TotpHelper(ITotpSetupGenerator totpSetUpGenerator, ITotpValidator totpValidator)
        {
            _totpSetUpGenerator = totpSetUpGenerator;
            _totpValidator = totpValidator;
        }

        public string GenerateSecret(string key, long salting)
        {
            return PasswordHasher.Generate(key, BitConverter.GetBytes(salting));
        }

        public TotpResult GenerateQrImage(string appName, string userName, string secret)
        {
            var totpSetUp = _totpSetUpGenerator.Generate(appName, userName, secret);
            
            return new TotpResult(totpSetUp.QrCodeImage, totpSetUp.ManualSetupKey);
        }

        public bool Validate(string secret, string clientTotp)
        {
            if (int.TryParse(clientTotp, out int clientCode))
            {
                return _totpValidator.Validate(secret, clientCode);
            }

            throw  new RestApiResponseException(ResourceCommonMessage.VerificationCodeIsInvalid);
        }
    }
}
