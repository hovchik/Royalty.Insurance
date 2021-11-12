using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Agencies.Queries;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Agencies.Commands
{
    public class UpdateAgencyCommandHandler : IRequestHandler<UpdateAgencyCommand, AgencyResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgencyMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRequestHandler<GetAgencyByIdQuery, AgencyResponse> _query;

        public UpdateAgencyCommandHandler(IApplicationDbContext context, IAgencyMapperService mapper, ICurrentUserService currentUserService, IRequestHandler<GetAgencyByIdQuery, AgencyResponse> query)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _query = query;
        }

        public async Task<AgencyResponse> Handle(UpdateAgencyCommand request, CancellationToken cancellationToken)
        {
            Agency entity = await _context.Agencies.FirstOrDefaultAsync(item => item.Id.Equals(request.Id) , cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            if (!_currentUserService.IsSupperAdmin)
            {
                throw new RestApiResponseException((int)HttpStatusCode.Forbidden, ResourceCommonMessage.UserIsNotAdmin);
            } 
            _mapper.UpdateEntity(entity, request);
            entity.UpdatedBy = _currentUserService.UserId;
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return await _query.Handle(new GetAgencyByIdQuery {Id = request.Id}, cancellationToken);
        }
    }
}
