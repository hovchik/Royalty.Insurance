using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    class GetCoverageByIdQueryHandler : IRequestHandler<GetCoverageByIdQuery, CoverageResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICoverageMapperService _mapper;

        public GetCoverageByIdQueryHandler(ICoverageMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CoverageResponse> Handle(GetCoverageByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Coverages.Where(item => item.Id == request.Id)
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
