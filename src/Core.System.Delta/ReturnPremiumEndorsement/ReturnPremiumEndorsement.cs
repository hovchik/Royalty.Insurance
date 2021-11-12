using System.Common.Authentication.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Core.System.Delta
{
    public class ReturnPremiumEndorsement : IReturnPremiumEndorsement
    {
        private readonly DeltaConfig _config;
        private readonly IBaseDeltaRequestHandler _deltaRequestHandler;

        public ReturnPremiumEndorsement(IOptions<AppSetting> options, IBaseDeltaRequestHandler deltaRequestHandler)
        {
            _deltaRequestHandler = deltaRequestHandler;
            _config = options.Value.DeltaConfig;
        }

        public async Task<ReturnPremiumEndorsementViewModel> SetUpAsync(ReturnPremiumEndorsementRequest request, CancellationToken cancellationToken)
        {
            return await _deltaRequestHandler.PostAsync<ReturnPremiumEndorsementViewModel, ReturnPremiumEndorsementRequest>(request, _config.ReturnPremiumEndorsementUrl, cancellationToken);
        }
    }
}
