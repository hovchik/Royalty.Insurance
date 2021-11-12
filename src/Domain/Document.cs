using System;

namespace Domain
{
    public class Document
    {
        
        public int Id { get; set; }
        public int? InsuredId { get; set; }
        
        
        public string DocumentName { get; set; }
        
        
        public string Path { get; set; }
        
        public string GroupId { get; set; }
        
        public string DriveItemId { get; set; }
        public byte DocumentTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }
        
        public DateTime? DeleteDatetimeUtc { get; set; }

        public User CreatedByNavigation { get; set; }
        public DocumentType DocumentType { get; set; }
        public Insured Insured { get; set; }
        public User UpdatedByNavigation { get; set; }
    }
}
