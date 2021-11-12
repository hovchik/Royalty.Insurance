using System.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Groups.Commands
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, GroupResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGroupMapperService _mapper;
        private readonly IRequestHandler<GetGroupByIdQuery, GroupResponse> _handler;
        private readonly ICurrentUserService _currentUser;
        public CreateGroupCommandHandler(IApplicationDbContext context, IGroupMapperService mapper, IRequestHandler<GetGroupByIdQuery, GroupResponse> handler, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _handler = handler;
            _currentUser = currentUser;
        }

        public async Task<GroupResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            Group entity = new Group
            {
                UpdatedBy = _currentUser.UserId,
                CreatedBy = _currentUser.UserId,
            };
            _mapper.UpdateEntity(entity, request);

            GroupMember member = new GroupMember { Group = entity, MemberId = _currentUser.UserId };
            entity.GroupMembers.Add(member);
            await _context.Groups.AddAsync(entity, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) != 2)
            {
                throw new RestApiResponseException(ResourceCommonMessage.SaveFailed);
            }

            return await _handler.Handle(new GetGroupByIdQuery {Id =  entity.Id}, cancellationToken);
        }
    }
}
