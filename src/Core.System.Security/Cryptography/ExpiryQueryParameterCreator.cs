using System;
using System.Security.Cryptography;
using System.Text;
namespace Core.System.Security.Cryptography
{
    public class ExpiryQueryParameterCreator : IExpiryQueryParameterCreator
    {
        private readonly string _secret;

        public ExpiryQueryParameterCreator(string secret)
        {
            _secret = secret;
        }

        public bool IsValidRequest(long expiryTicks, string hash)
        {
            var expired = new DateTime(expiryTicks);
            if (expired < DateTime.Now)
                return false;

            if (hash.ToLower() == GetHash(GetHashingString(expiryTicks)))
                return true;

            return false;
        }

        public string GetHashForExpiryTicks(long expiryTicks)
        {
            return GetHash(GetHashingString(expiryTicks));
        }

        public string GetAvatarQueryParam(int id, long queryParamExpiry)
        {
            long expiry = DateTime.UtcNow.AddHours(queryParamExpiry).Ticks;
            var hash = GetHashForExpiryTicks(expiry);

            return $"?expiry={expiry}&&hash={hash}&&id={id}";

        }

        private string GetHash(string strToHash)
        {
            string strResult = "";
            using SHA256CryptoServiceProvider provider = new SHA256CryptoServiceProvider();
            byte[] bytesToHash = Encoding.Default.GetBytes(strToHash);
            bytesToHash = provider.ComputeHash(bytesToHash);
                
            foreach (byte b in bytesToHash)
            {
                strResult += b.ToString("x2");
            }

            return strResult.ToLower();
        }

        private string GetHashingString(long expiryTicks)
        {
            return  $"{expiryTicks}{_secret}";
        }
    }
}
