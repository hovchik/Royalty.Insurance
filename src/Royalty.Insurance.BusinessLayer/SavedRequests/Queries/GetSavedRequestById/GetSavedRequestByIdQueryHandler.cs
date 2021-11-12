using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetSavedRequestByIdQueryHandler : IRequestHandler<GetSavedRequestByIdQuery, SavedRequestResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISavedRequestMapperService _mapper;

        public GetSavedRequestByIdQueryHandler(ISavedRequestMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<SavedRequestResponse> Handle(GetSavedRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.SavedMarketingRequests
                 .Where(item => item.Id.Equals(request.Id))
                 .Select(_mapper.MapResponse)
                 .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}