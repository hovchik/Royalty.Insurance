
namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class ForwardMessageCommand : CreateMessageCommand
    {
        public long ParentId { get; set; }
    }
}
