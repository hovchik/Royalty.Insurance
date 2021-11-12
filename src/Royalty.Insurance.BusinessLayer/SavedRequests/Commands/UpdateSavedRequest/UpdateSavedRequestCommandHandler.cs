using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class UpdateSavedRequestCommandHandler : IRequestHandler<UpdateSavedRequestCommand, SavedRequestResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISavedRequestMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public UpdateSavedRequestCommandHandler(ISavedRequestMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<SavedRequestResponse> Handle(UpdateSavedRequestCommand request, CancellationToken cancellationToken)
        {
            SavedMarketingRequest entity = await _context.SavedMarketingRequests.FirstOrDefaultAsync(item => item.Id.Equals(request.Id) && item.UserId == _currentUser.UserId, cancellationToken);
            _mapper.UpdateEntity(entity, request);
            _context.SavedMarketingRequests.Update(entity);
           
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}