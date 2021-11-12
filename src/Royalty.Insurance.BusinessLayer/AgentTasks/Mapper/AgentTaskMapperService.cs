using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class AgentTaskMapperService : IAgentTaskMapperService
    {
        public void UpdateEntity(AgentTask entity, CreateAgentTaskCommand request)
        {
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.AgentTaskStatusId = request.AgentTaskStatusId;
            entity.AssigneeId = request.AssigneeId;
            entity.AgentTaskTypeId = request.AgentTaskTypeId;
            entity.DueDatetimeUtc = request.DueDatetimeUtc;
            entity.CanceledReason = request.CanceledReason;
            entity.CompletedDatetimeUtc = entity.CompletedDatetimeUtc;
            entity.InsuredId = request.InsuredId;
        }

        public Expression<Func<AgentTask, AgentTaskResponse>> MapResponse
        {
            get
            {
                return entity => new AgentTaskResponse
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    AgentTaskStatusId = entity.AgentTaskStatusId,
                    AssigneeId = entity.AssigneeId,
                    AgentTaskTypeId = entity.AgentTaskTypeId,
                    DueDatetimeUtc = entity.DueDatetimeUtc,
                    CanceledReason = entity.CanceledReason,
                    CompletedDatetimeUtc = entity.CompletedDatetimeUtc,
                    InsuredId = entity.InsuredId,
                    UpdatedBy = entity.UpdatedBy,
                    CreatedBy = entity.CreatedBy
                };
            }
        }
    }
}
