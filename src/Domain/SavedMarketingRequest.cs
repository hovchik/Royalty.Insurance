using System;


namespace Domain
{
    public class SavedMarketingRequest
    {
        
        public int Id { get; set; }
        public int UserId { get; set; }
        
        public string SavedRequest { get; set; }
        public string ShortDescription { get; set; }
        
        public DateTime CreatedDateUtc { get; set; }
        public int? Hash { get; set; }
        public User User { get; set; }
    }
}
