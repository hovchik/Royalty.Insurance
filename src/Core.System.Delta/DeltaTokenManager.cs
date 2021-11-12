using System;
using System.Common.Authentication.Models;
using System.Common.Network;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.System.Delta.Models;
using Royalty.Insurance.Settings;

namespace Core.System.Delta
{
    internal static class DeltaTokenManager
    {
        public static  async Task<DeltaAuthorizationResponse> GetAccessTokenAsync(DeltaConfig config, IHttpHelper httpHelper, CancellationToken cancellationToken)
        {
            string basicAuth =
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.ApiKey}:{config.ApiKeyValue}"));
            httpHelper.AddAuthorization("Basic", basicAuth);
            httpHelper.AddContentType("application/x-www-form-urlencoded");
            var response =
                await httpHelper.Post<DeltaTokenResponse>($"{config.BaseUrl}{config.AuthorizationTokenUrl}", cancellationToken);
            httpHelper.AddAuthorization(SystemConstants.AuthenticationType, response.AccessToken);
            httpHelper.AddContentType("application/json");
            var request = new DeltaLoginRequest
            {
                Userid = config.UserId,
                Password = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{config.SecretKey}:{config.UserPassword}"))
            };
            var accessToken =
                await httpHelper.Post<DeltaAccessTokenResponse, DeltaLoginRequest>($"{config.BaseUrl}{config.AuthorizationLoginUrl}",
                    request,
                    cancellationToken);

            return new DeltaAuthorizationResponse
                {SessionToken = accessToken.Results, AccessToken = response.AccessToken, };
        }

    }
}
