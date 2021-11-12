using System.Collections.Immutable;
using System.Common.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace System.Common.Authentication.TwoFactor
{
    public class TokenManager : ITokenManager
    {
        private readonly AppSetting _appSetting;

        public TokenManager(IOptions<AppSetting> options)
        {
            _appSetting = options.Value;
        }

        public string GenerateToken(string username)
        {
            var claims = new Claim[]{new  Claim(ClaimTypes.Email, username)};
            var jwtToken = new JwtSecurityToken(
                _appSetting.TotpSetting.Issuer,
                _appSetting.TotpSetting.Audience,
                claims,
                expires: DateTime.UtcNow.AddSeconds(_appSetting.TotpSetting.TokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.Unicode.GetBytes(_appSetting.TotpSetting.Secret)),
                    SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return accessToken;
        }
    }
}
