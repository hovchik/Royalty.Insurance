using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetUserGroupsByTypeQueryHandler : IRequestHandler<GetUserGroupsByTypeQuery, List<GroupResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGroupMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetUserGroupsByTypeQueryHandler(IApplicationDbContext context, IGroupMapperService mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<List<GroupResponse>> Handle(GetUserGroupsByTypeQuery request, CancellationToken cancellationToken)
        {
            List<GroupResponse> entities = await _context.Groups
                .Where(item => item.GroupTypeId.Equals((byte)request.GroupTypeCode)
                               && item.GroupMembers.Any(m => m.MemberId.Equals(_currentUser.UserId)))
                .Select(_mapper.MapResponse).ToListAsync(cancellationToken);

            return entities;
        }
    }
}
