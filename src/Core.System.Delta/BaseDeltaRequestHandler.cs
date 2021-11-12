using System.Common.Authentication.Models;
using System.Common.Network;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Settings;

namespace Core.System.Delta
{
    public class BaseDeltaRequestHandler: IBaseDeltaRequestHandler
    {
        private readonly DeltaConfig _config;
        private readonly IHttpHelper _httpHelper;

        public BaseDeltaRequestHandler(IOptions<AppSetting> options, IHttpHelper httpHelper)
        {
            _config = options.Value.DeltaConfig;
            _httpHelper = httpHelper;
        }

        public async Task<TResponse> PostAsync<TResponse, TRequest>(TRequest request, string url, CancellationToken cancellationToken)
        {
            var deltaAuthorization = await DeltaTokenManager.GetAccessTokenAsync(_config, _httpHelper, cancellationToken);
            _httpHelper.AddAuthorization(SystemConstants.AuthenticationType, deltaAuthorization.AccessToken);
            _httpHelper.AddHeader("SessionToken", deltaAuthorization.SessionToken);
            TResponse response = await _httpHelper.Post<TResponse, TRequest>(
                $"{_config.TransactionUrl}{url}", request, cancellationToken);

            return response;
        }
    }
}
