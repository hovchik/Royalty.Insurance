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
    public class GetGvwrQueryHandler : IRequestHandler<GetGvwrQuery, List<GvwrResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetGvwrQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GvwrResponse>> Handle(GetGvwrQuery request, CancellationToken cancellationToken)
        {
            return await _context.Gvwrs.Select(gvwr => new GvwrResponse { GvwrDescription = gvwr.ClassDescription, Id = gvwr.Id }).ToListAsync(cancellationToken);
        }
    }
}
