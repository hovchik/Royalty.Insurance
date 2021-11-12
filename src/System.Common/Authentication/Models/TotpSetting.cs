namespace System.Common.Authentication.Models
{
    public class TotpSetting
    {
        public string Secret { get; set; }

        public string AppName { get; set; }

        public int TimeToleranceInSeconds { get; set; }
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int TokenExpiration { get; set; }
    }
}
