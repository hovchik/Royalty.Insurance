using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, GroupResponse>
    {
        private readonly IRequestHandler<GetGroupByIdQuery, GroupResponse> _handler;
        private readonly IApplicationDbContext _context;
        private readonly IGroupMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public UpdateGroupCommandHandler(IApplicationDbContext context, IRequestHandler<GetGroupByIdQuery, GroupResponse> handler, IGroupMapperService mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _handler = handler;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<GroupResponse> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            Group entity = await _context.Groups.Where(item => item.Id.Equals(request.Id)).FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            entity.UpdatedBy = _currentUser.UserId;
            _mapper.UpdateEntity(entity, request);

            _context.Groups.Update(entity);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException(ResourceCommonMessage.SaveFailed);
            }

            return await _handler.Handle(new GetGroupByIdQuery {Id = entity.Id },
                cancellationToken);
        }
    }
}
