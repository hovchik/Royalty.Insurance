using System;
using System.Collections.Generic;


namespace Domain
{
    public class Cargo
    {
        public Cargo()
        {
            CargoCommodities = new HashSet<CargoCommodity>();
        }

        
        public int Id { get; set; }
        public int InsuredId { get; set; }
        public int CreateBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreateDatetimeUtc { get; set; }
        public DateTime LastModifiedUtc { get; set; }

        public User CreateByNavigation { get; set; }
        public Insured Insured { get; set; }
        public User UpdatedByNavigation { get; set; }
        public ICollection<CargoCommodity> CargoCommodities { get; set; }
    }
}
