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
    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GroupResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IGroupMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetGroupByIdQueryHandler(IApplicationDbContext context, IGroupMapperService mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<GroupResponse> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Groups.Where(item => item.Id.Equals(request.Id) && item.GroupMembers.Any(m => m.MemberId.Equals(_currentUser.UserId)))
                .Select(_mapper.MapResponse)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }
            return entity;
        }
    }
}
