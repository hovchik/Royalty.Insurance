using System.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class CreateIndividualGroupCommandHandler : IRequestHandler<CreateIndividualGroupCommand, GroupResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly IRequestHandler<GetGroupByIdQuery, GroupResponse> _handler;

        public CreateIndividualGroupCommandHandler(IApplicationDbContext context, IRequestHandler<GetGroupByIdQuery, GroupResponse> handler, ICurrentUserService currentUser)
        {
            _context = context;
            _handler = handler;
            _currentUser = currentUser;
        }

        public async Task<GroupResponse> Handle(CreateIndividualGroupCommand request, CancellationToken cancellationToken)
        {
            var existingGroup = await _context.Groups
                .Include(item => item.GroupMembers)
                .Where(item => item.GroupTypeId.Equals((int)GroupTypeCode.Individual)
                               && (item.GroupMembers.All(members =>
                                   members.MemberId.Equals(_currentUser.UserId) ||
                                   members.MemberId.Equals(request.UserId)))
                )
                .FirstOrDefaultAsync(cancellationToken);
            if (existingGroup != null)
            {
                return await _handler.Handle(new GetGroupByIdQuery { Id = existingGroup.Id }, cancellationToken);
            }

            Group entity = new Group
            {
                CreatedBy = _currentUser.UserId,
                UpdatedBy = _currentUser.UserId,
                Name = $"{request.UserId}.{_currentUser.UserId}",
                GroupTypeId = (int)GroupTypeCode.Individual
            };

            GroupMember member = new GroupMember { Group = entity, MemberId = request.UserId };
            entity.GroupMembers.Add(member);
            member = new GroupMember { Group = entity, MemberId = _currentUser.UserId };
            entity.GroupMembers.Add(member);
            await _context.Groups.AddAsync(entity, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) != 3)
            {
                throw new RestApiResponseException(ResourceCommonMessage.SaveFailed);
            }

            return await _handler.Handle(new GetGroupByIdQuery {Id = entity.Id }, cancellationToken);
        }
    }
}
