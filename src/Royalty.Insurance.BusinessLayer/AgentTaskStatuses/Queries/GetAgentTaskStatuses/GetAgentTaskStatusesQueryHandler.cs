using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Extensions;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.AgentTaskStatuses
{
    public class GetAgentTaskStatusesQueryHandler : IRequestHandler<GetAgentTaskStatusesQuery, PaginationResponse<AgentTaskStatusResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgentTaskStatusMapperService _mapper;

        public GetAgentTaskStatusesQueryHandler(IApplicationDbContext context, IAgentTaskStatusMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<AgentTaskStatusResponse>> Handle(GetAgentTaskStatusesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.AgentTaskStatuses
                .OrderByDescending(item => item.CreateDatetimeUtc)
                .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize );

            return entities;
        }
    }
}
