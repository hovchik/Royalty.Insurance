using System.Collections.Generic;

namespace Domain
{
    public class City
    {
        public City()
        {
            InsuredGaragingCities = new HashSet<Insured>();
            InsuredMailingCities = new HashSet<Insured>();
            ZipCodes = new HashSet<ZipCode>();
        }

        
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int StateId { get; set; }

        public State State { get; set; }
        public ICollection<Insured> InsuredGaragingCities { get; set; }
        public ICollection<Insured> InsuredMailingCities { get; set; }
        public ICollection<ZipCode> ZipCodes { get; set; }
    }
}
