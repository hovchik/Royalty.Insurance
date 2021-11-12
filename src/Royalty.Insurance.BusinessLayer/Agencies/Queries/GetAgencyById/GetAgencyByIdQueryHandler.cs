using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Agencies.Queries
{
    public class GetAgencyByIdQueryHandler : IRequestHandler<GetAgencyByIdQuery, AgencyResponse>
    {
        private readonly IAgencyMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetAgencyByIdQueryHandler(IAgencyMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AgencyResponse> Handle(GetAgencyByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Agencies.Select(_mapper.MapResponse).FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}
