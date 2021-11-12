
namespace Domain
{
    public class CargoCommodity
    {
        
        public int Id { get; set; }
        public int CargoId { get; set; }
        public int CommodityId { get; set; }
        public Commodity Commodity { get; set; }
        public Cargo Cargo { get; set; }
    }
}
