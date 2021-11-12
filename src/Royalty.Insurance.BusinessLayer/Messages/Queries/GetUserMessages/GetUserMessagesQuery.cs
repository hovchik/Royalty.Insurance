using MediatR;
using Royalty.Insurance.BusinessLayer.Validator;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetUserMessagesQuery : PaginationCommand, IRequest<PaginationResponse<FileMessageResponse>>
    {
        public int UserId { get; set; }
    }
}
