using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class GetAchTypeQueryHandler : IRequestHandler<GetAchTypeQuery, List<AchTypeResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAchTypeQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AchTypeResponse>> Handle(GetAchTypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.AchTypes
                .Select(x => new AchTypeResponse { Type = x.Type, Description = x.Description, Id = x.Id }).ToListAsync(cancellationToken);

            return entities;
        }
    }
}