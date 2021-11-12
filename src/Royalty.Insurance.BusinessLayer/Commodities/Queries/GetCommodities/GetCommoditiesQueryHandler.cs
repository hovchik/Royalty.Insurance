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

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class GetCommoditiesQueryHandler : IRequestHandler<GetCommoditiesQuery, List<CommodityResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICommodityMapperService _mapper;

        public GetCommoditiesQueryHandler(ICommodityMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CommodityResponse>> Handle(GetCommoditiesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.Commodities
               .Select(_mapper.MapResponse)
               .ToListAsync();

            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
