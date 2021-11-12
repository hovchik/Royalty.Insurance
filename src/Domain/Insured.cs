using System;
using System.Collections.Generic;

namespace Domain
{
    public class Insured
    {
        public Insured()
        {
            AgentTasks = new HashSet<AgentTask>();
            Cargos = new HashSet<Cargo>();
            Documents = new HashSet<Document>();
            DriverInformations = new HashSet<DriverInformation>();
            InsuredCoverages = new HashSet<InsuredCoverage>();
            InsuredVehicles = new HashSet<InsuredVehicle>();
            LossInformations = new HashSet<LossInformation>();
            UserGarages = new HashSet<UserGarage>();
        }

        
        public int Id { get; set; }
        
        
        public string SocialSecurityNumber { get; set; }
        
        
        public string StateNumber { get; set; }
        
        public string MotorCarrierNumber { get; set; }
        public bool IsFilings { get; set; }
        public int? InsuredStatusId { get; set; }
        public int YearsInsured { get; set; }
        public byte FartherState { get; set; }
        public string Dba { get; set; }
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
        public int? LegalStatusId { get; set; }
        public int? DotNumber { get; set; }
        public int CreateBy { get; set; }
        public int UpdatedBy { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }

        public User CreateByNavigation { get; set; }
        public City GaragingCity { get; set; }
        public State GaragingState { get; set; }
        public ZipCode GaragingZipCode { get; set; }
        public InsuredStatus InsuredStatus { get; set; }
        public LegalStatus LegalStatus { get; set; }
        public City MailingCity { get; set; }
        public State MailingState { get; set; }
        public ZipCode MailingZipCode { get; set; }
        public User UpdatedByNavigation { get; set; }
        public ICollection<AgentTask> AgentTasks { get; set; }
        public ICollection<Cargo> Cargos { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<DriverInformation> DriverInformations { get; set; }
        public ICollection<InsuredCoverage> InsuredCoverages { get; set; }
        public ICollection<InsuredVehicle> InsuredVehicles { get; set; }
        public ICollection<LossInformation> LossInformations { get; set; }
        public ICollection<UserGarage> UserGarages { get; set; }
    }
}
