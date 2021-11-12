using System;
using System.Common.Authentication.Models;
using System.Common.Authentication.TwoFactor;
using System.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class SetupAuthenticatorCommandHandler : IRequestHandler<SetupAuthenticatorCommand, TotpResult>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITotpHelper _totpHelper;
        private readonly AppSetting _appSetting;

        public SetupAuthenticatorCommandHandler(IApplicationDbContext context, ITotpHelper totpHelper, IOptions<AppSetting> options)
        {
            _context = context;
            _totpHelper = totpHelper;
            _appSetting = options.Value;
        }

        public async Task<TotpResult> Handle(SetupAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            var token = new JwtSecurityToken(request.Token);
            if (token.ValidTo < DateTime.UtcNow)
            {
                throw new RestApiResponseException(ResourceCommonMessage.TokenExpired);
            }

            string email = token.Claims.First(item => item.Type.Equals(ClaimTypes.Email)).Value;
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Email.ToLower().Equals(email.ToLower()), cancellationToken);

            return _totpHelper.GenerateQrImage(_appSetting.TotpSetting.AppName, email, _totpHelper.GenerateSecret(user.Email, user.CreateDatetimeUtc.Ticks));
        }
    }
}
