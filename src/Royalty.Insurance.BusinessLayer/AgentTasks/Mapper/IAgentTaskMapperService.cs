using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public interface IAgentTaskMapperService
    {
        void UpdateEntity(AgentTask entity, CreateAgentTaskCommand request);
        Expression<Func<AgentTask, AgentTaskResponse>> MapResponse { get; }
    }
}
