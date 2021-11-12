using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetMessageByIdQuery : IRequest<FileMessageResponse>
    {
        public long Id { get; set; }
    }
}
