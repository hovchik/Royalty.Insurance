using System;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetGroupConversationQuery: IRequest<PaginationResponse<FileMessageResponse>>
    {
        public int GroupId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }
}
