using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.UserActivityLogs
{
    public class CreateLogOutActivityQueryHandler : IRequestHandler<CreateLogOutActivityQuery, bool>
    {
        private readonly IApplicationDbContext _context;

        public CreateLogOutActivityQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateLogOutActivityQuery request, CancellationToken cancellationToken)
        {
            var userActivityLogs = await _context.UserActivityLogs.Where(item => (item.RefreshTokenExpireAt < DateTime.UtcNow
                                                                && !item.LogOutDatetimeUtc.HasValue)
                                                               || (item.UserId.Equals(request.Request.UserId) &&
                                                                  item.SessionId.Equals(request.Request.SessionId) &&
                                                                  !item.LogOutDatetimeUtc.HasValue)).ToListAsync();
            foreach (var expiredUserActivityLog in userActivityLogs)
            {
                expiredUserActivityLog.LogOutDatetimeUtc = DateTime.UtcNow;
                expiredUserActivityLog.RefreshToken = SystemConstants.ExpiredRefreshToken;
                _context.UserActivityLogs.Update(expiredUserActivityLog);
            }

            return await _context.SaveChangesAsync(new CancellationToken()) >= 1;
        }
    }
}
