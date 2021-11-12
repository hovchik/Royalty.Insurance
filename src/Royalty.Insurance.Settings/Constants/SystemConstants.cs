

namespace Royalty.Insurance.Settings
{
    public static class SystemConstants
    {    
        public const string DefaultConnection = "DefaultConnection";
        public const string SwaggerName = "Royalty Insurance Api v1";
        public const string SwaggerDescription = "Web Api for Royalty Insurance";
        public const string ApiVersion = "v1";
        public const string MediaType = "application/json";
        public const string MultimediaType = "multipart/form-data";
        public const string DbUpdateException = "Microsoft.EntityFrameworkCore.DbUpdateException";
        public const string RestApiResponseException = "Core.Api.Helper.RestApiResponseException";
        public static string SwaggerEndpoint = $"/swagger/{ApiVersion}/swagger.json";
        public const string AuthenticationType = "Bearer";
        public const string JwtTokenConfig = "jwtTokenConfig";
        public const string Authorization = "Authorization";
        public const string Unauthorized = "Unauthorized";
        public const string BearerFormat = "BearerFormat";
        internal const string ClaimType2005Namespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";
        public static string SessionId = $"{ClaimType2005Namespace}/sessionId";
        public const string AppSetting = "AppSetting";
        public const string AccessToken = "access_token";
        public const string ExpiredRefreshToken = "EXPIRED-REFRESH-TOKEN";
        public const string PasswordValidationRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";
        public const string ApplicationInsightsInstrumentationKey = "ApplicationInsights:InstrumentationKey";
    }
}
