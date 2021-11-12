using System.Collections.Generic;


namespace Domain
{
    public class Attachment
    {
        public Attachment()
        {
            MessageAttachments = new HashSet<MessageAttachment>();
        }

        
        public int Id { get; set; }
        
        
        public string Name { get; set; }
        public int? UserGarageId { get; set; }
        public bool IsDeleted { get; set; }

        public UserGarage UserGarage { get; set; }
        public ICollection<MessageAttachment> MessageAttachments { get; set; }
    }
}
