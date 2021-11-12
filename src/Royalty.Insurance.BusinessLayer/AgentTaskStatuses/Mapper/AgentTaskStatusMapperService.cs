using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class AgentTaskStatusMapperService : IAgentTaskStatusMapperService
    {
        public void UpdateEntity(AgentTaskStatus entity, CreateAgentTaskStatusCommand request)
        {
            entity.Name = request.Name;
        }

        public Expression<Func<AgentTaskStatus, AgentTaskStatusResponse>> MapResponse
        {
            get
            {
                return entity => new AgentTaskStatusResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                };
            }
        }
    }
}
