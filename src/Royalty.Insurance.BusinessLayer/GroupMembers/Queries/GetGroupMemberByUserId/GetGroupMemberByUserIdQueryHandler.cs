using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class GetGroupMemberByUserIdQueryHandler : IRequestHandler<GetGroupMemberByUserIdQuery, List<GroupMemberResponse>>
    {
        private readonly IGroupMemberMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetGroupMemberByUserIdQueryHandler(IApplicationDbContext context, IGroupMemberMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GroupMemberResponse>> Handle(GetGroupMemberByUserIdQuery request, CancellationToken cancellationToken)
        {
            //TODO: if there is performance issue refactor the select
            var entities = await _context.Groups
                .Include(item => item.GroupMembers)
                .Where(item => item.Active && item.GroupMembers.Any(member => member.MemberId.Equals(request.UserRequestedId)))
                .Select(_mapper.MapResponse)
                .ToListAsync(cancellationToken);
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }
            return entities.OrderByDescending(item => item.Members.Max(member => member.LastMessageDate)).ToList();
        }
    }
}
