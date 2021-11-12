using System;
using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.Response
{
    public class DocumentResponse
    {
        public  int Id { get; set; }
        public string Path { get; set; }
        public byte TemplateId { get; set; }
        public string DocumentName { get; set; }
        public int UserId { get; set; }
        public int? InsuredsId { get; set; }
        public DateTime CreatedDatetime { get; set; }
    }
}