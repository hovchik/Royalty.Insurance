using System.ComponentModel.DataAnnotations;

namespace Royalty.Insurance.Proxy.Request
{
    public class UserProfileBaseRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        [Required]
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
    }
}