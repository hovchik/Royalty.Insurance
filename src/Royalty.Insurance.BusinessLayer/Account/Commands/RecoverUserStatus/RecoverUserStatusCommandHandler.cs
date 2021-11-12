
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class RecoverUserStatusCommandHandler : IRequestHandler<RecoverUserStatusCommand, UserStatusResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public RecoverUserStatusCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<UserStatusResponse> Handle(RecoverUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(item => item.UsersProfile).FirstOrDefaultAsync(item => item.Id.Equals(_currentUserService.UserId), cancellationToken);
            UserStatusResponse response = new UserStatusResponse();
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.UserNotFound);
            }

            if (user.UsersProfile == null)
            {
                UsersProfile usersProfile = new UsersProfile { Id = _currentUserService.UserId };
                response.UserStatusId = (int)UserStatusCode.Online;
                usersProfile.UserStatusId = response.UserStatusId;
                usersProfile.UserLastStatusId = response.UserStatusId;
                await _context.UsersProfiles.AddAsync(usersProfile,cancellationToken);
            }
            else
            {
                user.UsersProfile.UserStatusId = user.UsersProfile.UserLastStatusId;
                response.UserStatusId = user.UsersProfile.UserLastStatusId;
                response.CustomStatus = user.UsersProfile.Status;
            }

            await _context.SaveChangesAsync(cancellationToken);
            

            return response;
        }
    }
}
