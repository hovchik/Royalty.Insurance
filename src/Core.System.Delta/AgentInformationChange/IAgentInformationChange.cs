using System.Threading;
using System.Threading.Tasks;

namespace Core.System.Delta
{
    public interface IAgentInformationChange
    {
        Task<AgentInformationChangeViewModel> SetUpAsync(AgentInformationChangeRequest request, CancellationToken cancellationToken);
    }
}
