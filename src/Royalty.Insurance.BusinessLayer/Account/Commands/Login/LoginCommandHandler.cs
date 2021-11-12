using System;
using System.Common.Authentication.Models;
using System.Common.Authentication.TwoFactor;
using System.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IRequestHandler<CheckPasswordCommand, User> _handler;
        private readonly IRequestHandler<TrustedDeviceCommand, bool> _trustHandler;
        private readonly ITokenManager _tokenManager;
        private readonly AppSetting _appSetting;
        private readonly IRequestHandler<TokenProviderCommand, LoginResponse> _tokenProviderHandler;

        public LoginCommandHandler(IRequestHandler<CheckPasswordCommand, User> handler, IRequestHandler<TrustedDeviceCommand, bool> trustHandler, IOptions<AppSetting> options, ITokenManager tokenManager, IRequestHandler<TokenProviderCommand, LoginResponse> tokenProviderHandler)
        {
            _handler = handler;
            _trustHandler = trustHandler;
            _tokenManager = tokenManager;
            _tokenProviderHandler = tokenProviderHandler;
            _appSetting = options.Value;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _handler.Handle(
                new CheckPasswordCommand {Email = request.Email, Password = request.Password}, cancellationToken);
            var isTrusted = await _trustHandler.Handle(new TrustedDeviceCommand {DeviceId = request.DeviceId, User = user},
                cancellationToken);

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                if (!user.TwoFactorEnabled && _appSetting.RequiredTwoFactor && !isTrusted)
                {
                    throw new FoundException(_tokenManager.GenerateToken(request.Email));
                }

                if (user.TwoFactorEnabled && !isTrusted)
                {
                    throw new PreconditionRequiredException(_tokenManager.GenerateToken(request.Email));
                }
            }

            return await _tokenProviderHandler.Handle(
                new TokenProviderCommand
                    {SessionId = Guid.NewGuid(), UserIpAddress = request.UserIpAddress, User = user},
                cancellationToken);
        }
    }
}
