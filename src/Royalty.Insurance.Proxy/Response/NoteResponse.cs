using System;

namespace Royalty.Insurance.Proxy.Response
{
    public class NoteResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int? InsuredId { get; set; }
        public string Note { get; set; }
    }
}
