using System;

namespace Domain
{
    public class DriverInformation
    {
        public int Id { get; set; }
        
        
        public string DriverName { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        
        public string LicenseNumber { get; set; }
        public int StateId { get; set; }
        
        public DateTime DateHired { get; set; }
        public int YearOfExperiance { get; set; }
        public string Accidents { get; set; }
        public int? InsuredId { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }

        public User CreatedByNavigation { get; set; }
        public Insured Insured { get; set; }
        public State State { get; set; }
        public User UpdatedByNavigation { get; set; }
    }
}
