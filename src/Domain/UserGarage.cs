using System;
using System.Collections.Generic;


namespace Domain
{
    public class UserGarage
    {
        public UserGarage()
        {
            Attachments = new HashSet<Attachment>();
        }

        
        public int Id { get; set; }
        public int UserId { get; set; }
        
        public string FileName { get; set; }
        public int? AssignedInsuredId { get; set; }
        public byte FileFormatId { get; set; }
        
        
        public string Path { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime? ModifyDatetimeUtc { get; set; }

        public Insured AssignedInsured { get; set; }
        public FileFormat FileFormat { get; set; }
        public User User { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
