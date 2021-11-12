using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.UserActivityLogs
{
    public class CreateLogInActivityQueryHandler : IRequestHandler<CreateLogInActivityQuery, bool>
    {
        private readonly IApplicationDbContext _context;

        public CreateLogInActivityQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateLogInActivityQuery request, CancellationToken cancellationToken)
        {
            UserActivityLog userActivityLog = await _context.UserActivityLogs.Where(item => item.SessionId.Equals(request.Request.SessionId))
                .FirstOrDefaultAsync();
            if (userActivityLog == null)
            {
                userActivityLog = new UserActivityLog
                {
                    UserId = request.Request.UserId,
                    LogInDatetimeUtc = DateTime.UtcNow,
                    SessionId = request.Request.SessionId,
                    DeviceIp = request.Request.DeviceIp,
                    RefreshTokenExpireAt = request.Request.RefreshTokenExpireAt,
                    RefreshToken = request.Request.RefreshToken
                };
                await _context.UserActivityLogs.AddAsync(userActivityLog);
            }
            else
            {
                userActivityLog.RefreshToken = request.Request.RefreshToken;
                userActivityLog.RefreshTokenExpireAt = request.Request.RefreshTokenExpireAt;
                _context.UserActivityLogs.Update(userActivityLog);
            }

            return await _context.SaveChangesAsync(new CancellationToken()) == 1;
        }
    }
}
