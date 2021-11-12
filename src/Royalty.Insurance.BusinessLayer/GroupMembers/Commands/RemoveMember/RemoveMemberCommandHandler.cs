using System.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Extensions;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, GroupMemberResponse>
    {
        private readonly IGroupMemberMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public RemoveMemberCommandHandler(IApplicationDbContext context, IGroupMemberMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GroupMemberResponse> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
        {


            if (!await _context.IsGroupMember(request.GroupId, request.UserRequestedId, request.MemberIds))
            {
                throw new RestApiResponseException(ResourceCommonMessage.UserIsNotMember);
            }

            var entities = await _context.GroupMembers
                .Where(item => item.GroupId.Equals(request.GroupId) && request.MemberIds.Contains(item.MemberId))
                .ToListAsync(cancellationToken);
            _context.GroupMembers.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);

            return  await _context.Groups.Where(item => item.Id.Equals(request.GroupId))
                .Include(item => item.GroupMembers)
                .Where(item => item.Active)
                .Select(_mapper.MapResponse).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
