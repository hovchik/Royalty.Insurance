using System.Collections.Generic;

namespace Domain
{
    public class DocumentType
    {
        public DocumentType()
        {
            Documents = new HashSet<Document>();
        }

        
        public byte Id { get; set; }
        
        
        public string Name { get; set; }

        public ICollection<Document> Documents { get; set; }
    }
}
