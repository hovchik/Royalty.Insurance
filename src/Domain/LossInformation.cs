using System;

namespace Domain
{
    public class LossInformation
    {
        
        public int Id { get; set; }
        
        public DateTime EffectiveDate { get; set; }
        
        public DateTime ExpireDate { get; set; }
        
        
        public string InsuranceName { get; set; }
        
        public string LesseeName { get; set; }
        
        
        public string PoliceNumber { get; set; }
        
        public string LesseeMcnumber { get; set; }
        
        
        public string NumberOfClaims { get; set; }
        public string Comments { get; set; }
        public int InsuredId { get; set; }

        public Insured Insured { get; set; }
    }
}
