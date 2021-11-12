using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    class GetAgentTasksByStatusQueryHandler
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgentTaskMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAgentTasksByStatusQueryHandler(IApplicationDbContext context, IAgentTaskMapperService mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PaginationResponse<AgentTaskResponse>> Handle(GetAgentTasksByStatusQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.AgentTasks
                .Where(item => item.AgentTaskStatusId.Equals(request.AgentTaskStatusId) && item.AssigneeId.Equals(_currentUserService.UserId) || _currentUserService.IsSupperAdmin)
                .OrderByDescending(item => item.CreateDatetimeUtc)
                .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);

            return response;

        }
    }
}
