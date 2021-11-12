using System.Collections.Generic;

namespace Domain
{
    public class ZipCode
    {
        public ZipCode()
        {
            InsuredGaragingZipCodes = new HashSet<Insured>();
            InsuredMailingZipCodes = new HashSet<Insured>();
        }

        
        public int Id { get; set; }
        
        public string Code { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }
        public ICollection<Insured> InsuredGaragingZipCodes { get; set; }
        public ICollection<Insured> InsuredMailingZipCodes { get; set; }
    }
}
