using System;
using System.Collections.Generic;
using System.Text;

namespace Royalty.Insurance.Proxy.Response
{
    public class InsuredResponse
    {
        public int Id { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string StateNumber { get; set; }
        public string MotorCarrierNumber { get; set; }
        public bool IsFilings { get; set; }
        public int YearsInsured { get; set; }
        public byte FartherState { get; set; }
        public int MailingStateId { get; set; }
        public int MailingCityId { get; set; }
        public int MailingZipCodeId { get; set; }
        public string MailingStreetAddress { get; set; }
        public string MailingPhone { get; set; }
        public string MailingEmail { get; set; }
        public string MailingName { get; set; }
        public int GaragingStateId { get; set; }
        public int GaragingCityId { get; set; }
        public int GaragingZipCodeId { get; set; }
        public string GaragingStreetAddress { get; set; }
        public string GaragingPhone { get; set; }
        public string GaragingEmail { get; set; }
        public string GaragingName { get; set; }
        public int LegalStatusId { get; set; }
    }
}
