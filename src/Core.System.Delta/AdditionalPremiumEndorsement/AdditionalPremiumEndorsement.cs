using System.Common.Authentication.Models;
using System.Common.Network;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Settings;

namespace Core.System.Delta
{
    public class AdditionalPremiumEndorsement : IAdditionalPremiumEndorsement
    {
        private readonly DeltaConfig _config;
        private readonly IBaseDeltaRequestHandler _deltaRequestHandler;

        public AdditionalPremiumEndorsement(IOptions<AppSetting> options, IBaseDeltaRequestHandler deltaRequestHandler)
        {
            _deltaRequestHandler = deltaRequestHandler;
            _config = options.Value.DeltaConfig;
        }

        public async Task<PremiumEndorsementViewModel> SetUpAsync(PremiumEndorsementRequest request, CancellationToken cancellationToken)
        {
            return await _deltaRequestHandler.PostAsync<PremiumEndorsementViewModel, PremiumEndorsementRequest>(request, _config.AdditionalPremiumEndorsementUrl, cancellationToken);
        }
    }
}
