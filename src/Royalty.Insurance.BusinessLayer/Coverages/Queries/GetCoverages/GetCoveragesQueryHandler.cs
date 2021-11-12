using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Coverages
{
    public class GetCoveragesQueryHandler : IRequestHandler<GetCoveragesQuery, List<CoverageResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICoverageMapperService _mapper;

        public GetCoveragesQueryHandler(ICoverageMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CoverageResponse>> Handle(GetCoveragesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Coverages.Select(_mapper.MapResponse).ToListAsync(cancellationToken);

            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
