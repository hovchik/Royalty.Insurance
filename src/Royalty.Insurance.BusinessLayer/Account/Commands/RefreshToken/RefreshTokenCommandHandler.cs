using System;
using System.Common.Authentication.Services;
using System.Common.Exceptions;
using System.Common.Extensions;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IRequestHandler<TokenProviderCommand, LoginResponse> _tokenHandler;

        public RefreshTokenCommandHandler(IApplicationDbContext context, IJwtAuthManager jwtAuthManager, IRequestHandler<TokenProviderCommand, LoginResponse> tokenHandler)
        {
            _context = context;
            _jwtAuthManager = jwtAuthManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenParts = request.ExpiredAccessToken.Split(" ");
            if (tokenParts.Length != 2)
            {
                throw new RestApiResponseException(ResourceCommonMessage.InvalidToken);
            }
            if (!tokenParts[0].Equals(SystemConstants.AuthenticationType, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new RestApiResponseException(ResourceCommonMessage.InvalidToken);
            }
            var principal = _jwtAuthManager.DecodeJwtToken(tokenParts[1]);
            if (principal?.Identity == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.InvalidToken);
            }

            var session = await _context.UserActivityLogs
                .FirstOrDefaultAsync(
                    item =>
                        item.RefreshToken.Equals(request.Token)
                        && !item.LogOutDatetimeUtc.HasValue, cancellationToken);
            if (session == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.TokenExpired);
            }
            var userId = principal.UserId();

            return await _tokenHandler.Handle(new TokenProviderCommand {SessionId = principal.SessionId(), UserIpAddress = request.UserIpAddress, User =  await _context.Users.FirstOrDefaultAsync(item => item.Id.Equals(userId), cancellationToken) }, cancellationToken);
        }
    }
}
