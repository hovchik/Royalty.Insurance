using System.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class UpdateAgentTaskStatusCommandHandler : IRequestHandler<UpdateAgentTaskStatusCommand, AgentTaskStatusResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgentTaskStatusMapperService _mapper;
        private readonly IRequestHandler<GetAgentTaskStatusByIdQuery, AgentTaskStatusResponse> _handler;

        public UpdateAgentTaskStatusCommandHandler(IApplicationDbContext context, IAgentTaskStatusMapperService mapper, IRequestHandler<GetAgentTaskStatusByIdQuery, AgentTaskStatusResponse> handler)
        {
            _context = context;
            _mapper = mapper;
            _handler = handler;
        }

        public async Task<AgentTaskStatusResponse> Handle(UpdateAgentTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.AgentTaskStatuses.Where(item => item.Id.Equals(request. Id))
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EntityNotFound);
            }
            _mapper.UpdateEntity(entity, request);
            _context.AgentTaskStatuses.Update(entity);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException(ResourceCommonMessage.SaveFailed);
            }
            return await _handler.Handle(new GetAgentTaskStatusByIdQuery { Id = entity.Id }, cancellationToken);
        }
    }
}
