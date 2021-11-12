using Royalty.Insurance.BusinessLayer.Extensions;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class GetUserGroupMemberByGroupIdQueryHandler : IRequestHandler<GetUserGroupMemberByGroupIdQuery, List<GroupMemberResponse>>
    {
        private readonly IGroupMemberMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetUserGroupMemberByGroupIdQueryHandler(IApplicationDbContext context, IGroupMemberMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GroupMemberResponse>> Handle(GetUserGroupMemberByGroupIdQuery request, CancellationToken cancellationToken)
        {
            if (!await _context.IsGroupMember(request.GroupId, request.UserRequestedId))
            {
                throw new RestApiResponseException(ResourceCommonMessage.UserIsNotMember);
            }

            var entities = await _context.Groups.Where(item => item.Id.Equals(request.GroupId))
                .Include(item => item.GroupMembers)
                .Where(item => item.Active && item.GroupMembers.Any(member => member.MemberId.Equals(request.UserRequestedId)))
                .Select(_mapper.MapResponse).ToListAsync(cancellationToken);

            return entities;
        }
    }
}
