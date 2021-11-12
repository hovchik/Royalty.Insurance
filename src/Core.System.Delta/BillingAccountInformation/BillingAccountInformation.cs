using System.Common.Authentication.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Core.System.Delta
{
    public class BillingAccountInformation : IBillingAccountInformation
    {
        private readonly DeltaConfig _config;
        private readonly IBaseDeltaRequestHandler _deltaRequestHandler;

        public BillingAccountInformation(IOptions<AppSetting> options, IBaseDeltaRequestHandler deltaRequestHandler)
        {
            _deltaRequestHandler = deltaRequestHandler;
            _config = options.Value.DeltaConfig;
        }

        public async Task<BillingAccountInformationViewModel> SetUpAsync(BillingAccountInformationRequest request, CancellationToken cancellationToken)
        {
            return await _deltaRequestHandler.PostAsync<BillingAccountInformationViewModel, BillingAccountInformationRequest>(request, _config.BillingAccountInformationUrl, cancellationToken);
        }
    }
}
