using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account.DeactivateUser
{
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public DeactivateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public  async Task<bool> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id.Equals(request.UserId), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.UserNotFound);
            }

            if (!user.IsActive)
            {
                throw new RestApiResponseException(ResourceCommonMessage.UserAlreadyDeactivated);
            }
            user.IsActive = false;

            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int) HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return true;
        }
    }
}
