using MediatR;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class ReadGroupMessageCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        
        public int GroupId { get; set; }
    }
}
