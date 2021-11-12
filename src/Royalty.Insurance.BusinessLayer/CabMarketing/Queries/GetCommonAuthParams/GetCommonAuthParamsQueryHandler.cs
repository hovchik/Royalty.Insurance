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
    public class GetCommonAuthParamsQueryHandler : IRequestHandler<GetCommonAuthParamsQuery, List<CommonAuthTypeResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetCommonAuthParamsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CommonAuthTypeResponse>> Handle(GetCommonAuthParamsQuery request, CancellationToken cancellationToken)
        {
            return await _context.CommonAuthTypes.Select(res => new CommonAuthTypeResponse { Value = res.Value, Name = res.Name }).ToListAsync(cancellationToken);
        }
    }
}
