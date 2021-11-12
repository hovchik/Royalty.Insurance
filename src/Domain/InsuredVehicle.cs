namespace Domain
{
    public class InsuredVehicle
    {
        
        public int Id { get; set; }
        public int InsuredId { get; set; }
        public int VehicleId { get; set; }

        public Insured Insured { get; set; }
        public VehicleInfo Vehicle { get; set; }
    }
}
