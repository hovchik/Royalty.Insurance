using System.ComponentModel.DataAnnotations;

namespace Royalty.Insurance.Proxy.Request
{
    public class InsuredRequest
    {
        [Required]
        [StringLength(50)]
        public string SocialSecurityNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string StateNumber { get; set; }
        [Required]
        [StringLength(120)]
        public string MotorCarrierNumber { get; set; }
        public bool IsFilings { get; set; }
        public int YearsInsured { get; set; }
        [Range(1, 50)]
        public byte FartherState { get; set; }
        public int MailingStateId { get; set; }
        public int MailingCityId { get; set; }
        public int MailingZipCodeId { get; set; }
        [Required]
        [StringLength(256)]
        public string MailingStreetAddress { get; set; }
        [Required]
        [StringLength(15)]
        public string MailingPhone { get; set; }
        [Required]
        [StringLength(256)]
        public string MailingEmail { get; set; }
        [Required]
        [StringLength(256)]
        public string MailingName { get; set; }
        public int GaragingStateId { get; set; }
        public int GaragingCityId { get; set; }
        public int GaragingZipCodeId { get; set; }
        [Required]
        [StringLength(256)]
        public string GaragingStreetAddress { get; set; }
        [Required]
        [StringLength(15)]
        public string GaragingPhone { get; set; }
        [Required]
        [StringLength(256)]
        public string GaragingEmail { get; set; }
        [Required]
        [StringLength(256)]
        public string GaragingName { get; set; }
        public int LegalStatusId { get; set; }
    }
}
