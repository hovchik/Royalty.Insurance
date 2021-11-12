using System;

namespace Domain
{
    public class InsuredCoverage
    {
        
        public int Id { get; set; }
        public int InsuredId { get; set; }
        public int CoverageId { get; set; }
        public int Limit { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }

        public Coverage Coverage { get; set; }
        public  Insured Insured { get; set; }
    }
}
