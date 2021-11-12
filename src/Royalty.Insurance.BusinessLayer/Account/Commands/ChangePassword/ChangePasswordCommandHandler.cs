using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id.Equals(_currentUserService.UserId), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EmailNotFound);
            }

            if (!PasswordHasher.IsValid(user.Password, request.Password, user.Salting))
            {
                throw new RestApiResponseException(ResourceCommonMessage.UserOrPassword);
            }

            var result = PasswordHasher.Generate(request.NewPassword);
            user.Password = result.PasswordHash;
            user.Salting = result.Salting;

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return Unit.Value;
        }
    }
}
