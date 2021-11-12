using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class CreateAgentTaskStatusCommandHandler : IRequestHandler<CreateAgentTaskStatusCommand, AgentTaskStatusResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgentTaskStatusMapperService _mapper;
        private readonly IRequestHandler<GetAgentTaskStatusByIdQuery, AgentTaskStatusResponse> _handler;

        public CreateAgentTaskStatusCommandHandler(IApplicationDbContext context, IAgentTaskStatusMapperService mapper, IRequestHandler<GetAgentTaskStatusByIdQuery, AgentTaskStatusResponse> handler)
        {
            _context = context;
            _mapper = mapper;
            _handler = handler;
        }

        public async Task<AgentTaskStatusResponse> Handle(CreateAgentTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = new AgentTaskStatus();
            _mapper.UpdateEntity(entity, request);
            await _context.AgentTaskStatuses.AddAsync(entity, cancellationToken);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException(ResourceCommonMessage.SaveFailed);
            }

            return await _handler.Handle(new GetAgentTaskStatusByIdQuery { Id =  entity.Id }, cancellationToken);
        }
    }
}
