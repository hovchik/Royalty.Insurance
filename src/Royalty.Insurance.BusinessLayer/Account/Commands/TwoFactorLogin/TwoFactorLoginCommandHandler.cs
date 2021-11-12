using System;
using System.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class TwoFactorLoginCommandHandler : IRequestHandler<TwoFactorLoginCommand, LoginResponse>
    {
        private readonly IRequestHandler<TokenProviderCommand, LoginResponse> _tokenProviderHandler;
        private readonly IRequestHandler<FindByEmailCommand, User> _findByEmailHandler;
        private readonly IRequestHandler<VerifyAuthenticatorCodeCommand, bool> _verifyAuthenticatorCode;

        public TwoFactorLoginCommandHandler(IRequestHandler<TokenProviderCommand, LoginResponse> tokenProviderHandler, IRequestHandler<FindByEmailCommand, User> findByemailHandler, IRequestHandler<VerifyAuthenticatorCodeCommand, bool> verifyAuthenticatorCode)
        {
            _tokenProviderHandler = tokenProviderHandler;
            _findByEmailHandler = findByemailHandler;
            _verifyAuthenticatorCode = verifyAuthenticatorCode;
        }

        public async Task<LoginResponse> Handle(TwoFactorLoginCommand request, CancellationToken cancellationToken)
        {
            var token = new JwtSecurityToken(request.Token);
            if (token.ValidTo < DateTime.UtcNow)
            {
                throw new RestApiResponseException(ResourceCommonMessage.TokenExpired);
            }

            string email = token.Claims.First(item => item.Type.Equals(ClaimTypes.Email)).Value;
            if (!await _verifyAuthenticatorCode.Handle(new VerifyAuthenticatorCodeCommand {Code =  request.Code, Email =  email}, cancellationToken))
            {
                throw new RestApiResponseException(ResourceCommonMessage.AuthenticatorFailedMessage);
            }
            var user = await _findByEmailHandler.Handle(new FindByEmailCommand {Email = email}, cancellationToken);
            var sessionId = Guid.NewGuid();

            return  await _tokenProviderHandler.Handle(new TokenProviderCommand() { SessionId = sessionId, User = user, UserIpAddress = request.UserIpAddress}, cancellationToken);
        }
    }
}
