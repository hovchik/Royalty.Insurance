using System.Common.Validator;
using System.ComponentModel.DataAnnotations;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.Proxy.Request
{
    public class UserRequest : UserBaseRequest
    {
        [PasswordValidation]
        public string Password { get; set; }

        [Required]
        public UserRoleType Role { get; set; }
    }
}
