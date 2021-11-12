using System.Collections.Generic;


namespace Domain
{
    public class State
    {
        public State()
        {
            Cities = new HashSet<City>();
            DriverInformations = new HashSet<DriverInformation>();
            InsuredGaragingStates = new HashSet<Insured>();
            InsuredMailingStates = new HashSet<Insured>();
        }

        
        public int Id { get; set; }
        
        
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<DriverInformation> DriverInformations { get; set; }
        public ICollection<Insured> InsuredGaragingStates { get; set; }
        public ICollection<Insured> InsuredMailingStates { get; set; }
    }
}
