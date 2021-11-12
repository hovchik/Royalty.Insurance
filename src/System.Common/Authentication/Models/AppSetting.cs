
namespace System.Common.Authentication.Models
{
    public class AppSetting
    {
        public JwtTokenConfig JwtTokenConfig { get; set; }

        public TotpSetting TotpSetting { get; set; }

        public DeltaConfig DeltaConfig { get; set; }

        public string AdminEmail { get; set; }

        public int FailedMaxCount { get; set; }

        public bool RequiredTwoFactor { get; set; }

        public EmailSetting EmailSetting { get; set; }

        public MicrosoftOfficeSetting MicrosoftOfficeSetting { get; set; }

        public SmtpSetting Smtp { get; set; }

        public DocUSignJwt DocUSignJwt { get; set; }

        public string BlobStorageConnectionString { get; set; }

        public string QueryParamSecret { get; set; }

        public int QueryParamExpiry { get; set; }

        public string CabKey { get; set; }

        public int GarageSize { get; set; }

        public AgaveSetting AgaveSetting { get; set; }
        public double ProRatePercent { get; set; }
    }
}
