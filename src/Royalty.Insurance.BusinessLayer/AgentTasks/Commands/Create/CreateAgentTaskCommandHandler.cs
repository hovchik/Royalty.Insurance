using System.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class CreateAgentTaskCommandHandler : IRequestHandler<CreateAgentTaskCommand, AgentTaskResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAgentTaskMapperService _mapper;
        private readonly IRequestHandler<GetAgentTaskByIdQuery, AgentTaskResponse> _requestHandler;

        public CreateAgentTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IAgentTaskMapperService mapper, IRequestHandler<GetAgentTaskByIdQuery, AgentTaskResponse> requestHandler)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _requestHandler = requestHandler;
        }

        public async Task<AgentTaskResponse> Handle(CreateAgentTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = new AgentTask();
            _mapper.UpdateEntity(entity, request);
            entity.CreatedBy = _currentUserService.UserId;
            entity.UpdatedBy = _currentUserService.UserId;
            await _context.AgentTasks.AddAsync(entity, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException(ResourceCommonMessage.SaveFailed);
            }

            return await _requestHandler.Handle(new GetAgentTaskByIdQuery {Id = entity.Id}, cancellationToken);
        }
    }
}
