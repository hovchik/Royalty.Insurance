using System.Linq;
using System.Security.Claims;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace System.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasUserId(this ClaimsPrincipal user)
        {
            return user.Claims.Any(c => c.Type == JwtClaimTypes.Subject);
        }

        public static int UserId(this ClaimsPrincipal user)
        {
            if (int.TryParse(user.Claims.First(c => c.Type == JwtClaimTypes.Subject).Value, out int userId))
            {
                return userId;
            }
            throw new ArgumentNullException("User can not found");
        }

        public static Guid SessionId(this ClaimsPrincipal user)
        {
            if (Guid.TryParse(user.Claims.First(c => c.Type == SystemConstants.SessionId).Value, out Guid sessionId))
            {
                return sessionId;
            }

            throw new ArgumentNullException("Session can not found");
        }

        public static bool IsSupperAdmin(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c =>
                c.Type == ClaimTypes.Role && c.Value == UserRoleType.SuperAdmin.ToString()) != null;
        }

        public static bool HasUserEmail(this ClaimsPrincipal user)
        {
            return user.Claims.Any(c => c.Type == ClaimTypes.Email);
        }

        public static string UserEmail(this ClaimsPrincipal user)
        {
            return user.Claims.First(c => c.Type == ClaimTypes.Email).Value;
        }
    }
}
