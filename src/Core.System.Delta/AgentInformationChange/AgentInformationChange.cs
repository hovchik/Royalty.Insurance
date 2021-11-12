using System.Common.Authentication.Models;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Core.System.Delta
{
    public class AgentInformationChange : IAgentInformationChange
    {
        private readonly DeltaConfig _config;
        private readonly IBaseDeltaRequestHandler _deltaRequestHandler;

        public AgentInformationChange(IOptions<AppSetting> options, IBaseDeltaRequestHandler deltaRequestHandler)
        {
            _deltaRequestHandler = deltaRequestHandler;
            _config = options.Value.DeltaConfig;
        }

        public async Task<AgentInformationChangeViewModel> SetUpAsync(AgentInformationChangeRequest request, CancellationToken cancellationToken)
        {
            return await _deltaRequestHandler.PostAsync<AgentInformationChangeViewModel, AgentInformationChangeRequest>(request, _config.AgentInformationChange, cancellationToken);
        }
    }
}
