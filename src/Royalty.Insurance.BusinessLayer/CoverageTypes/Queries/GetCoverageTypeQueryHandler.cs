using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.CoverageTypes.Queries
{
    public class GetCoverageTypeQueryHandler : IRequestHandler<GetCoverageTypesQuery, List<CoverageTypeResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetCoverageTypeQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CoverageTypeResponse>> Handle(GetCoverageTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.CoverageTypes.Select(x => new CoverageTypeResponse { Id = x.Id, Name = x.Name }).ToListAsync();

            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}