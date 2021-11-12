using System.Common.Exceptions;
using System.Net;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetCustomStatusCommandHandler : IRequestHandler<SetCustomStatusCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public SetCustomStatusCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(SetCustomStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(item => item.UsersProfile).FirstOrDefaultAsync(item => item.Id.Equals(_currentUserService.UserId), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.UserNotFound);
            }

            if (user.UsersProfile == null)
            {

                user.UsersProfile = new UsersProfile
                {
                    Id = _currentUserService.UserId,
                    UserStatusId = (int)UserStatusCode.Custom,
                    UserLastStatusId = (int)UserStatusCode.Custom,
                    Status = request.CustomStatus
                };
                await _context.UsersProfiles.AddAsync(user.UsersProfile, cancellationToken);
            }
            else
            {
                user.UsersProfile.UserStatusId = (int)UserStatusCode.Custom;
                user.UsersProfile.UserLastStatusId = (int)UserStatusCode.Custom;
                user.UsersProfile.Status = request.CustomStatus;
            }

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw  new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return Unit.Value;
        }
    }
}
