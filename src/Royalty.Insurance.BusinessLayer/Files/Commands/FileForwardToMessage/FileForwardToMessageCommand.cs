using MediatR;
using Royalty.Insurance.Proxy.Response;


namespace Royalty.Insurance.BusinessLayer.Files
{
    public class FileForwardToMessageCommand : IRequest<FileMessageResponse>
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int GroupId { get; set; }
    }
}
