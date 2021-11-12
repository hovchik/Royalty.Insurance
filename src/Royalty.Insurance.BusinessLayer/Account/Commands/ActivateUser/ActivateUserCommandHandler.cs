using System;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.System.Security.Cryptography;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public ActivateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _context.Users.FirstOrDefaultAsync(item => item.Email.Equals(request.Email), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EmailNotFound);
            }

            if (!PasswordHasher.IsValid(user.Password, request.Password, user.Salting))
            {
                throw new RestApiResponseException(ResourceCommonMessage.UserOrPassword);
            }

            if (!user.ActivationExpiryDatetimeUtc.HasValue ||
                DateTime.Compare(user.ActivationExpiryDatetimeUtc.Value, DateTime.UtcNow) < 0)
            {
                throw new RestApiResponseException(ResourceCommonMessage.ActivationPeriod);
            }

            var result = PasswordHasher.Generate(request.NewPassword);
            user.Password = result.PasswordHash;
            user.Salting = result.Salting;
            user.IsActive = true;
            user.TemporaryPassword = false;
            user.ActivationExpiryDatetimeUtc = null;

            return await _context.SaveChangesAsync(cancellationToken) == 1;
        }
    }
}
