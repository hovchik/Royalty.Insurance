using System.Collections.Generic;

namespace Domain
{
    public class LegalStatus
    {
        public LegalStatus()
        {
            Insureds = new HashSet<Insured>();
        }

        
        public int Id { get; set; }
        
        
        public string Name { get; set; }

        public ICollection<Insured> Insureds { get; set; }
    }
}
