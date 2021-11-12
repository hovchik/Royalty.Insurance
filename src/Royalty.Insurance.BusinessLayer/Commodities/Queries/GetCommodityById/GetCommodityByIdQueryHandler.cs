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

namespace Royalty.Insurance.BusinessLayer.Commodities
{
    public class GetCommodityByIdQueryHandler : IRequestHandler<GetCommodityByIdQuery, CommodityResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICommodityMapperService _mapper;

        public GetCommodityByIdQueryHandler(ICommodityMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<CommodityResponse> Handle(GetCommodityByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Commodities
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
