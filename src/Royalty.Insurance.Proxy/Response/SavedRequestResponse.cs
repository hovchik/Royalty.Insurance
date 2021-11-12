using System;

namespace Royalty.Insurance.Proxy.Response
{
    public class SavedRequestResponse
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public string  ShortDescription { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}