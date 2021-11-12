using System.Collections.Generic;

namespace Domain
{
    public class VehicleInfo
    {
        public VehicleInfo()
        {
            InsuredVehicles = new HashSet<InsuredVehicle>();
        }

        
        public int Id { get; set; }
        public int Year { get; set; }
        
        
        public string Make { get; set; }
        
        
        public string Type { get; set; }
        public int Gvw { get; set; }
        public int ActualValue { get; set; }
        
        public string Radius { get; set; }
        
        
        public string Vin { get; set; }
        public string Comments { get; set; }
        public bool IsTruck { get; set; }

        public ICollection<InsuredVehicle> InsuredVehicles { get; set; }
    }
}
