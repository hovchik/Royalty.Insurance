using MediatR;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class ReadMessageCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        
        public long MessageId { get; set; }
    }
}
