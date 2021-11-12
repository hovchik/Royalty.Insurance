using System.Collections.Generic;

namespace Domain
{
    public class Coverage
    {
        public Coverage()
        {
            InsuredCoverages = new HashSet<InsuredCoverage>();
        }

        
        public int Id { get; set; }
        
        
        public string CoverageType { get; set; }
        public int CoverageLimit { get; set; }

        public ICollection<InsuredCoverage> InsuredCoverages { get; set; }
    }
}
