using System.Threading;
using System.Threading.Tasks;
using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta
{
    public class AgentInformationChangeCommandHandler : IRequestHandler<AgentInformationChangeCommand, AgentInformationChangeViewModel>
    {
        private readonly IAgentInformationChange _agentInformationChange;

        public AgentInformationChangeCommandHandler(IAgentInformationChange agentInformationChange)
        {
            _agentInformationChange = agentInformationChange;
        }

        public async Task<AgentInformationChangeViewModel> Handle(AgentInformationChangeCommand request, CancellationToken cancellationToken)
        {
            return await _agentInformationChange.SetUpAsync(request, cancellationToken);
        }
    }
}
