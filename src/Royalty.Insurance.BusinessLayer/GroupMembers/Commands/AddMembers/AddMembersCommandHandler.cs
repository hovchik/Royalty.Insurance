using System.Collections.Generic;
using System.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Extensions;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.GroupMembers
{
    public class AddMembersCommandHandler : IRequestHandler<AddMembersCommand, List<GroupMemberResponse>>
    {
        private readonly IGroupMemberMapperService _mapper;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRequestHandler<GetUserGroupMemberByGroupIdQuery, List<GroupMemberResponse>> _handler;

        public AddMembersCommandHandler(IApplicationDbContext context, IGroupMemberMapperService mapper, IRequestHandler<GetUserGroupMemberByGroupIdQuery, List<GroupMemberResponse>> handler, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _handler = handler;
            _currentUserService = currentUserService;
        }

        public async Task<List<GroupMemberResponse>> Handle(AddMembersCommand request, CancellationToken cancellationToken)
        {
            request.UserRequestedId = _currentUserService.UserId;
            if (!await _context.IsGroupMember(request.GroupId , request.UserRequestedId))
            {
                throw new RestApiResponseException(ResourceCommonMessage.CreatorCanAddMember);
            }
            foreach (var memberId in request.MemberIds)
            {
                var entity = new GroupMember
                {
                    GroupId = request.GroupId

                };
                _mapper.UpdateEntity(entity, memberId);
                await _context.GroupMembers.AddAsync(entity, cancellationToken);
            }
            if (await _context.SaveChangesAsync(cancellationToken) != request.MemberIds.Count)
            {
                throw new RestApiResponseException(ResourceCommonMessage.SaveFailed);
            }

            return await _handler.Handle(new GetUserGroupMemberByGroupIdQuery { GroupId = request.GroupId, UserRequestedId = request .UserRequestedId}, cancellationToken);
        }
    }
}
