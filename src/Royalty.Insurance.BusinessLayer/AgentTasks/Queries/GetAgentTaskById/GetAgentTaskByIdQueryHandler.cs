using System.Common.Exceptions;
using System.Linq;
using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class GetAgentTaskByIdQueryHandler : IRequestHandler<GetAgentTaskByIdQuery, AgentTaskResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgentTaskMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAgentTaskByIdQueryHandler(IApplicationDbContext context, IAgentTaskMapperService mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<AgentTaskResponse> Handle(GetAgentTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.AgentTasks.Where(item => item.Id.Equals(request.Id) &&
                                                                 (item.AssigneeId.Equals(_currentUserService.UserId) || _currentUserService.IsSupperAdmin))
                .Select(_mapper.MapResponse).FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}
