using System.Common.Validator;

namespace Royalty.Insurance.Proxy.Request
{
    public class UserBaseRequest : UserProfileBaseRequest
    {
        [EmailValidation]
        public string Email { get; set; }
    }
}
