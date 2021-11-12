using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.GetUsersActivityLogsBySession
{
    public class GetUsersActivityLogsBySessionQueryHandler : IRequestHandler<GetUsersActivityLogsBySessionQuery, UserActivityLog>
    {
        private readonly IApplicationDbContext _context;

        public GetUsersActivityLogsBySessionQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserActivityLog> Handle(GetUsersActivityLogsBySessionQuery request, CancellationToken cancellationToken)
        {
            return await _context.UserActivityLogs.Where(item => item.SessionId.Equals(request.SessionId)).FirstOrDefaultAsync();
        }
    }
}
