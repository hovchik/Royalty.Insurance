using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetOperationTypesQueryHandler : IRequestHandler<GetOperationTypesQuery, List<OperationTypeResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetOperationTypesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OperationTypeResponse>> Handle(GetOperationTypesQuery request, CancellationToken cancellationToken)
        {
            return await _context.OperationTypes.Select(res => new OperationTypeResponse { Value = res.Value, Type = res.Type }).ToListAsync(cancellationToken);
        }
    }
}
