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
    public class CreateSavedRequestCommandHandler : IRequestHandler<CreateSavedRequestCommand, SavedRequestResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISavedRequestMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateSavedRequestCommandHandler(IApplicationDbContext context, ISavedRequestMapperService mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<SavedRequestResponse> Handle(CreateSavedRequestCommand request, CancellationToken cancellationToken)
        {
            var isRecordExists = await _context.SavedMarketingRequests.FirstOrDefaultAsync(x => x.Hash == request.Request.GetHashCode());
            if (isRecordExists != null)
            {
                return _mapper.MapResponse.Invoke(isRecordExists);
            }

            SavedMarketingRequest entity = new SavedMarketingRequest { UserId = _currentUser.UserId };
            _mapper.UpdateEntity(entity, request);
            await _context.SavedMarketingRequests.AddAsync(entity);

            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}