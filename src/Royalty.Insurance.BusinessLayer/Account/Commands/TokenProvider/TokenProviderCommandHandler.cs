using System;
using System.Common.Authentication.Models;
using System.Common.Authentication.Services;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.System.Security.Cryptography;
using MediatR;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Roles;
using Royalty.Insurance.BusinessLayer.UserActivityLogs;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class TokenProviderCommandHandler : IRequestHandler<TokenProviderCommand, LoginResponse>
    {
        private readonly IRequestHandler<CreateLogInActivityQuery, bool> _activityLogInHandler;
        private readonly IAccountMapperService _mapper;
        private readonly AppSetting _appSetting;
        private readonly IRequestHandler<GetRoleByTypeQuery, string> _getRoleHandler;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IRequestHandler<SetUserStatusCommand, Unit> _userStatusHandler;

        public TokenProviderCommandHandler(IRequestHandler<CreateLogInActivityQuery, bool> activityLogInHandler, IExpiryQueryParameterCreator expiryQueryParameterCreator, IAccountMapperService mapper, IRequestHandler<GetRoleByTypeQuery, string> getRoleHandler, IOptions<AppSetting> options, IJwtAuthManager jwtAuthManager, IRequestHandler<SetUserStatusCommand, Unit> userStatusHandler)
        {
            _activityLogInHandler = activityLogInHandler;
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _mapper = mapper;
            _getRoleHandler = getRoleHandler;
            _jwtAuthManager = jwtAuthManager;
            _userStatusHandler = userStatusHandler;
            _appSetting = options.Value;
        }

        public async Task<LoginResponse> Handle(TokenProviderCommand request, CancellationToken cancellationToken)
        {
            var returnUser = _mapper.MapResponse.Compile().Invoke(request.User, _expiryQueryParameterCreator, _appSetting);
            var type = await _getRoleHandler.Handle(new GetRoleByTypeQuery { RoleType = (UserRoleType)request.User.UserRoleId }, cancellationToken);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,request.User.Email),
                new Claim(ClaimTypes.Name,request.User.Email),
                new Claim(ClaimTypes.GivenName,$"{request.User.FirstName} {request.User.LastName}"),
                new Claim(JwtClaimTypes.Subject, request.User.Id.ToString()),
                new Claim(SystemConstants.SessionId,request.SessionId.ToString()),
                new Claim(ClaimTypes.Role,type),
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(request.User.Email, claims, DateTime.UtcNow);
            

            returnUser.AccessToken = jwtResult.AccessToken;
            returnUser.RefreshToken = jwtResult.RefreshToken;
            await _activityLogInHandler.Handle(new CreateLogInActivityQuery
            {
                Request = new UserActivityLogRequest
                {
                    SessionId = request.SessionId,
                    UserId = request.User.Id,
                    RefreshTokenExpireAt = returnUser.RefreshToken.ExpireAt,
                    RefreshToken = returnUser.RefreshToken.TokenString,
                    DeviceIp = request.UserIpAddress
                }
            }, cancellationToken);

            await _userStatusHandler.Handle(new SetUserStatusCommand {UserId =  request.User.Id , UserStatus = UserStatusCode.Online }, cancellationToken);

            return returnUser;
        }
    }
}
