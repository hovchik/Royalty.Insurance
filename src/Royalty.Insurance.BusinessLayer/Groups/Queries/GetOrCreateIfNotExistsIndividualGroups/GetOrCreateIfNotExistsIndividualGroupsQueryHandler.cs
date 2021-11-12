using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetOrCreateIfNotExistsIndividualGroupsQueryHandler : IRequestHandler<GetOrCreateIfNotExistsIndividualGroupsQuery, List<GroupResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGroupMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetOrCreateIfNotExistsIndividualGroupsQueryHandler(IGroupMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<List<GroupResponse>> Handle(GetOrCreateIfNotExistsIndividualGroupsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Groups.Where(item => item.Active && item.GroupTypeId.Equals((int)GroupTypeCode.Individual)
                                                                           && item.GroupMembers.Any(m => m.MemberId.Equals(_currentUser.UserId)))
                .Select(_mapper.MapResponse).ToListAsync(cancellationToken);
            if (entities.Count == 0)
            {
                var users = await _context.Users.Where(item => item.IsActive && !item.Id.Equals(_currentUser.UserId)).Select(item => item.Id)
                    .ToListAsync(cancellationToken);
                foreach (var user in users)
                {
                    Group entity = new Group
                    {
                        CreatedBy = _currentUser.UserId,
                        UpdatedBy = _currentUser.UserId,
                        Name = $"{_currentUser.UserId}:{user}",
                        GroupTypeId = (int)GroupTypeCode.Individual
                    };

                    GroupMember member = new GroupMember { Group = entity, MemberId = _currentUser.UserId };
                    entity.GroupMembers.Add(member);
                    member = new GroupMember { Group = entity, MemberId = user };
                    entity.GroupMembers.Add(member);
                    await _context.Groups.AddAsync(entity, cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);

                return await _context.Groups.Where(item => item.GroupMembers.Any(m => m.MemberId.Equals(_currentUser.UserId)))
                    .Select(_mapper.MapResponse).ToListAsync(cancellationToken);
            }

            return entities;
        }
    }
}
