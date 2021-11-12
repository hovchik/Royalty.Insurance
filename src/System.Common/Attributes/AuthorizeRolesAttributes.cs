using Microsoft.AspNetCore.Authorization;
using Royalty.Insurance.Settings.Enums;

namespace System.Common.Attributes
{
    public class AuthorizeByRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeByRolesAttribute(params string[] roles)
        {
            Roles = nameof(UserRoleType.SuperAdmin) + "," + string.Join(",", roles);
        }

        public AuthorizeByRolesAttribute(bool isAllowAllRoles)
        {
            if (isAllowAllRoles)
            {
                Roles =
                    $"{nameof(UserRoleType.SuperAdmin)},{nameof(UserRoleType.Accounting)},{nameof(UserRoleType.Agent)},{nameof(UserRoleType.Assistant)}," +
                    $"{nameof(UserRoleType.IT)},{nameof(UserRoleType.Marketing)},{nameof(UserRoleType.Underwriter)}";
            }
        }
    }

    //public class Authorze : AuthenticationAtt

}