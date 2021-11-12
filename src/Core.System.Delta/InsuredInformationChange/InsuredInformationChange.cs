using System.Common.Authentication.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Core.System.Delta
{
    public class InsuredInformationChange : IInsuredInformationChange
    {
        private readonly DeltaConfig _config;
        private readonly IBaseDeltaRequestHandler _deltaRequestHandler;

        public InsuredInformationChange(IOptions<AppSetting> options, IBaseDeltaRequestHandler deltaRequestHandler)
        {
            _deltaRequestHandler = deltaRequestHandler;
            _config = options.Value.DeltaConfig;
        }
        public async Task<InsuredInformationChangeViewModel> SetUpAsync(InsuredInformationChangeRequest request, CancellationToken cancellationToken)
        {
            return await _deltaRequestHandler.PostAsync<InsuredInformationChangeViewModel, InsuredInformationChangeRequest>(request, _config.InsuredInformationChangeUrl, cancellationToken);
        }
    }
}
