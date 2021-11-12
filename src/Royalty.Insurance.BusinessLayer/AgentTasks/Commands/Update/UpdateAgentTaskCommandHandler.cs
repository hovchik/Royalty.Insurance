using System.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class UpdateAgentTaskCommandHandler : IRequestHandler<UpdateAgentTaskCommand, AgentTaskResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAgentTaskMapperService _mapper;
        private readonly IRequestHandler<GetAgentTaskByIdQuery, AgentTaskResponse> _requestHandler;

        public UpdateAgentTaskCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IAgentTaskMapperService mapper, IRequestHandler<GetAgentTaskByIdQuery, AgentTaskResponse> requestHandler)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _requestHandler = requestHandler;
        }

        public async Task<AgentTaskResponse> Handle(UpdateAgentTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.AgentTasks
                .FirstOrDefaultAsync(item => item.Id.Equals(request.Id), cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException(StatusCodes.Status404NotFound, ResourceCommonMessage.RecordNotFound);
            }
            _mapper.UpdateEntity(entity, request);
            entity.UpdatedBy = _currentUserService.UserId;
            await _context.AgentTasks.AddAsync(entity, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException(ResourceCommonMessage.SaveFailed);
            }

            return await _requestHandler.Handle(new GetAgentTaskByIdQuery { Id = entity.Id }, cancellationToken);
        }
    }
}
