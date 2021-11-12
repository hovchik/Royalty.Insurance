using System.ComponentModel.DataAnnotations;

namespace Royalty.Insurance.Proxy.Request
{
    public class TwoFactorAuthenticationVerificationRequest
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
