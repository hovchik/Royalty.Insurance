using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetGroupConversationQueryHandler : IRequestHandler<GetGroupConversationQuery, PaginationResponse<FileMessageResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMessageMapperService _mapper;

        public GetGroupConversationQueryHandler(IApplicationDbContext context, IMessageMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<FileMessageResponse>> Handle(GetGroupConversationQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.Messages.Where(item => item.RecipientGroupId.Equals(request.GroupId)
                                                    && item.CreateDatetimeUtc >= request.From
                                                    && item.CreateDatetimeUtc <= request.To)
                    .OrderByDescending(item => item.CreateDatetimeUtc)
                    .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);

            return response;
        }
    }
}
