using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace System.Common.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(JwtClaimTypes.Subject) ?? throw new UnauthorizedAccessException("Invalid User Id"));
        public string UserFullName  => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.GivenName);
        public Guid SessionId =>  Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(SystemConstants.SessionId) ?? throw new UnauthorizedAccessException("Invalid Session"));

        public bool IsSupperAdmin => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c =>
                                         c.Type == ClaimTypes.Role && c.Value == UserRoleType.SuperAdmin.ToString()) 
                                     !=  null;

        public string UserEmail => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

    }
}
