using System.Common.Authentication.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Core.System.Delta
{
    public class PolicyReinstatement : IPolicyReinstatement
    {
        private readonly DeltaConfig _config;
        private readonly IBaseDeltaRequestHandler _deltaRequestHandler;

        public PolicyReinstatement(IOptions<AppSetting> options, IBaseDeltaRequestHandler deltaRequestHandler)
        {
            _deltaRequestHandler = deltaRequestHandler;
            _config = options.Value.DeltaConfig;
        }

        public async Task<PolicyReinstatementViewModel> SetUpAsync(PolicyReinstatementRequest request, CancellationToken cancellationToken)
        {
            return await _deltaRequestHandler.PostAsync<PolicyReinstatementViewModel, PolicyReinstatementRequest>(request, _config.PolicyReinstatementUrl, cancellationToken);
        }
    }
}
