using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public interface IAgentTaskStatusMapperService
    {
        void UpdateEntity(AgentTaskStatus entity, CreateAgentTaskStatusCommand request);
        Expression<Func<AgentTaskStatus, AgentTaskStatusResponse>> MapResponse { get; }
    }
}
