
namespace Domain
{
    public class MessageAttachment
    {
        
        public long MessageId { get; set; }
        
        public int AttachmentId { get; set; }

        public Attachment Attachment { get; set; }
        public Message Message { get; set; }
    }
}
