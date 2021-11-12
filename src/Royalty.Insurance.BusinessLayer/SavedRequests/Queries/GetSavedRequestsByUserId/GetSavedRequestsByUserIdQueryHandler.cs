using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class GetSavedRequestsByUserIdQueryHandler : IRequestHandler<GetSavedRequestsByUserIdQuery, PaginationResponse<SavedRequestResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISavedRequestMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetSavedRequestsByUserIdQueryHandler(ISavedRequestMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<PaginationResponse<SavedRequestResponse>> Handle(GetSavedRequestsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.SavedMarketingRequests
                 .Where(u => u.UserId == _currentUser.UserId)
                 .OrderByDescending(x => x.CreatedDateUtc).ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);

            if (entities.RowCount == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}