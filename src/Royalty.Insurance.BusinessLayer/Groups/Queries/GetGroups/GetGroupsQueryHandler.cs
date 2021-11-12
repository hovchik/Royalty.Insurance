using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, List<GroupResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGroupMapperService _mapper;

        public GetGroupsQueryHandler(IApplicationDbContext context, IGroupMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GroupResponse>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            List<GroupResponse> entities = await _context.Groups
                .Select(_mapper.MapResponse)
                .ToListAsync(cancellationToken);
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }
            return entities;
        }
    }
}
