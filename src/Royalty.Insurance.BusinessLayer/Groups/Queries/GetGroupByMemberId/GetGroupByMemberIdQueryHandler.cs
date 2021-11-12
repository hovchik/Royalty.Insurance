using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Groups
{

    public class GetGroupByMemberIdQueryHandler : IRequestHandler<GetGroupByMemberIdQuery, List<GroupResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGroupMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetGroupByMemberIdQueryHandler(IApplicationDbContext context, IGroupMapperService mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<List<GroupResponse>> Handle(GetGroupByMemberIdQuery request, CancellationToken cancellationToken)
        {
            List<GroupResponse> entities = await _context.Groups
                .Where(item => item.GroupMembers.Any(m => m.MemberId.Equals(_currentUser.UserId)))
                .Select(_mapper.MapResponse).ToListAsync(cancellationToken);

            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }
            return entities;
        }
    }
}
