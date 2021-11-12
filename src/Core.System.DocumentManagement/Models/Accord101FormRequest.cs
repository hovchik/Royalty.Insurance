
namespace Core.System.DocumentManagement
{
    public class Accord101FormRequest
    {
        public string ProducerFullName { get; set; }
        
        public string AgencyName { get; set; }
        public string InsuranceNameCarrier { get; set; } = "TODO:";
        public string InsuredCompanyName { get; set; }
    
        public string InsuredAddress { get; set; }

        public string InsuredCity { get; set; }
        public string InsuredState {get;set; }
        public string InsuredZip { get; set; }
    }
}
