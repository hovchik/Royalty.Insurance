using System.Collections.Immutable;
using System.Common.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace System.Common.Authentication.Services
{
    public interface IJwtAuthManager
    {
        JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);
        ClaimsPrincipal DecodeJwtToken(string token);
        string GenerateAccessToken(Claim[] claims, DateTime now, double minuteExpiry);
    }
}