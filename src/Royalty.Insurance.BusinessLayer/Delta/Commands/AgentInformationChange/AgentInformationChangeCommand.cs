using Core.System.Delta;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Delta
{
    public class AgentInformationChangeCommand : AgentInformationChangeRequest, IRequest<AgentInformationChangeViewModel>
    {
    }
}
