using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetLocationTypeQueryHandler : IRequestHandler<GetLocationTypeQuery, List<LocationTypeResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetLocationTypeQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LocationTypeResponse>> Handle(GetLocationTypeQuery request, CancellationToken cancellationToken)
        {
            return await _context.LocationTypes.Select(basic => new LocationTypeResponse { LocationType = basic.LocationType1, Id = basic.Id }).ToListAsync(cancellationToken);
        }
    }
}
