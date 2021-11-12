using System.Common.Authentication.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Core.System.Delta
{
    public class NewBillingAccount : INewBillingAccount
    {
        private readonly IBaseDeltaRequestHandler _deltaRequestHandler;
        private readonly DeltaConfig _config;

        public NewBillingAccount(IBaseDeltaRequestHandler deltaRequestHandler, IOptions<AppSetting> options)
        {
            _deltaRequestHandler = deltaRequestHandler;
            _config = options.Value.DeltaConfig;
        }

        public async Task<DeltaBillingAccountViewModel> SetUpAsync(NewBillingAccountRequest request, CancellationToken cancellationToken)
        {
            return await _deltaRequestHandler.PostAsync<DeltaBillingAccountViewModel, NewBillingAccountRequest>(request, _config.NewBillingAccountUrl, cancellationToken);
        }
    }
}
