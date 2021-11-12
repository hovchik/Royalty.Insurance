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
    public class GetAgentTaskStatusByIdQueryHandler : IRequestHandler<GetAgentTaskStatusByIdQuery, AgentTaskStatusResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgentTaskStatusMapperService _mapper;

        public GetAgentTaskStatusByIdQueryHandler(IApplicationDbContext context, IAgentTaskStatusMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AgentTaskStatusResponse> Handle(GetAgentTaskStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.AgentTaskStatuses.Where(item => item.Id.Equals(request.Id)).Select(_mapper.MapResponse)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}
