namespace Royalty.Insurance.Proxy.Response
{
    public class VehicleInfoResponse
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Type { get; set; }
        public int GVW { get; set; }
        public int ActualValue { get; set; }
        public string Radius { get; set; }
        public string VIN { get; set; }
        public string Comments { get; set; }
        public bool IsTruck { get; set; }

    }
}