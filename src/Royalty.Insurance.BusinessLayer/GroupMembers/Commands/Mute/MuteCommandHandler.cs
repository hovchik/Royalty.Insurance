using System.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Extensions;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class MuteCommandHandler : IRequestHandler<MuteCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public MuteCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(MuteCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.IsGroupMember(request.GroupId, request.UserId))
            {
                throw new RestApiResponseException(ResourceCommonMessage.UserIsNotMember);
            }

            var groupMember = await _context.GroupMembers
                .FirstOrDefaultAsync(item => item.GroupId.Equals(request.GroupId) && item.MemberId.Equals(request.UserId), cancellationToken);

            groupMember.Muted = request.Mute;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
