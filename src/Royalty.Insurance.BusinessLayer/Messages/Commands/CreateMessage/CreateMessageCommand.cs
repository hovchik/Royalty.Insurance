using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class CreateMessageCommand : IRequest<FileMessageResponse>
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
