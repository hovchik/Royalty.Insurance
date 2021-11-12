using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.UserActivityLogs;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Account.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRequestHandler<SetUserStatusCommand, Unit> _handler;
        private readonly IRequestHandler<CreateLogOutActivityQuery, bool> _logoutHandler;

        public LogoutCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IRequestHandler<SetUserStatusCommand, Unit> handler, IRequestHandler<CreateLogOutActivityQuery, bool> logoutHandler)
        {
            _context = context;
            _currentUserService = currentUserService;
            _handler = handler;
            _logoutHandler = logoutHandler;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id.Equals(_currentUserService.UserId), cancellationToken);

            await _handler.Handle(new SetUserStatusCommand {UserStatus = UserStatusCode.Offline, UserId = _currentUserService.UserId}, cancellationToken);
            return await _logoutHandler.Handle(new CreateLogOutActivityQuery
            {
                Request = new UserActivityLogRequest
                {
                    SessionId = _currentUserService.SessionId,
                    UserId = user.Id
                }
            }, cancellationToken);
        }
    }
}
