using System;
using System.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetTwoFactorEnabledCommandHandler : IRequestHandler<SetTwoFactorEnabledCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IRequestHandler<VerifyAuthenticatorCommand, bool> _verifyAuthenticator;

        public SetTwoFactorEnabledCommandHandler(IApplicationDbContext context, IRequestHandler<VerifyAuthenticatorCommand, bool> verifyAuthenticator)
        {
            _context = context;
            _verifyAuthenticator = verifyAuthenticator;
        }

        public async Task<bool> Handle(SetTwoFactorEnabledCommand request, CancellationToken cancellationToken)
        {
            var token = new JwtSecurityToken(request.Token);
            if (token.ValidTo < DateTime.UtcNow)
            {
                throw new RestApiResponseException(ResourceCommonMessage.TokenExpired);
            }

            string email = token.Claims.First(item => item.Type.Equals(ClaimTypes.Email)).Value;
            if (!await _verifyAuthenticator.Handle(
                new VerifyAuthenticatorCommand {Code = request.Token, Email = email }, cancellationToken))
            {
                throw new RestApiResponseException(ResourceCommonMessage.VerificationCodeIsInvalid);
            }
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Email.Equals(email.ToLower()), cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.UserNotFound);
            }

            if (user.TwoFactorEnabled)
            {
                throw new RestApiResponseException((int)HttpStatusCode.BadRequest, ResourceCommonMessage.AuthenticatorAlreadyAdded);
            }

            user.TwoFactorEnabled = true;

            return await _context.SaveChangesAsync(new CancellationToken()) == 1;
        }
    }
}
