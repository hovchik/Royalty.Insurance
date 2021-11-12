using System.Common.Authentication.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Core.System.Delta
{
    public class PolicyCancellation : IPolicyCancellation
    {
        private readonly DeltaConfig _config;
        private readonly IBaseDeltaRequestHandler _deltaRequestHandler;

        public PolicyCancellation(IOptions<AppSetting> options, IBaseDeltaRequestHandler deltaRequestHandler)
        {
            _deltaRequestHandler = deltaRequestHandler;
            _config = options.Value.DeltaConfig;
        }

        public async Task<PolicyCancellationViewModel> SetUpAsync(PolicyCancellationRequest request, CancellationToken cancellationToken)
        {
            return await _deltaRequestHandler.PostAsync<PolicyCancellationViewModel, PolicyCancellationRequest>(request, _config.PolicyCancellationUrl, cancellationToken);
        }
    }
}
