using System;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class CreateAgentTaskCommand : IRequest<AgentTaskResponse>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int? AssigneeId { get; set; }

        public int AgentTaskStatusId { get; set; }

        public byte AgentTaskTypeId { get; set; }

        public string CanceledReason { get; set; }

        public DateTime? DueDatetimeUtc { get; set; }

        public DateTime? CompletedDatetimeUtc { get; set; }

        public int? InsuredId { get; set; }
    }
}
