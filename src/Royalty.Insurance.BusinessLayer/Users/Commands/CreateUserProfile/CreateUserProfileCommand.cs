using MediatR;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Settings.Enums;
using System.Common.Validator;
using System.ComponentModel.DataAnnotations;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class CreateUserProfileCommand : UserBaseRequest, IRequest<bool>
    {
        [PasswordValidation]
        public string Password { get; set; }

        [Required]
        public UserRoleType Role { get; set; }
    }
}
