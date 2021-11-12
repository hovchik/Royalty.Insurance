using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetUserStatusCommandHandler : IRequestHandler<SetUserStatusCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public SetUserStatusCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SetUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(item => item.UsersProfile)
                .FirstOrDefaultAsync(item => item.Id.Equals(request.UserId), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.UserNotFound);
            }
            if (user.UsersProfile == null)
            {
                user.UsersProfile = new UsersProfile
                {
                    Id = request.UserId,
                    UserStatusId = (int)request.UserStatus,
                    UserLastStatusId = (int)request.UserStatus,
                };
                await _context.UsersProfiles.AddAsync(user.UsersProfile, cancellationToken);
            }
            else
            {
                user.UsersProfile.UserStatusId = (int) request.UserStatus;
                user.UsersProfile.UserLastStatusId = (int)request.UserStatus;
                user.UsersProfile.Status = null;
            }


            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
