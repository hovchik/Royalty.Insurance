using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTasks
{
    public class GetAgentTasksQueryHandler : IRequestHandler<GetAgentTasksQuery, PaginationResponse<AgentTaskResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgentTaskMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAgentTasksQueryHandler(IApplicationDbContext context, IAgentTaskMapperService mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PaginationResponse<AgentTaskResponse>> Handle(GetAgentTasksQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.AgentTasks
                .Where(item => item.AssigneeId.Equals(_currentUserService.UserId) || _currentUserService.IsSupperAdmin)
                .OrderByDescending(item => item.CreateDatetimeUtc)
                .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);

            return response;

        }
    }
}
