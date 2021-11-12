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
    public class GetBasicAlertQueryHandler : IRequestHandler<GetBasicAlertQuery, List<BasicAlertResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetBasicAlertQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BasicAlertResponse>> Handle(GetBasicAlertQuery request, CancellationToken cancellationToken)
        {
            return await _context.BasicAlerts.Select(basic => new BasicAlertResponse { BasicAlert = basic.BasicAlert1, Id = basic.Id }).ToListAsync(cancellationToken);
        }
    }
}
