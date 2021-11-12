using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetUserGroupMessagesQueryHandler : IRequestHandler<GetUserGroupMessagesQuery, PaginationResponse<FileMessageResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMessageMapperService _mapper;

        public GetUserGroupMessagesQueryHandler(IApplicationDbContext context, IMessageMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<FileMessageResponse>> Handle(GetUserGroupMessagesQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.Messages
                                         .Where(item => item.RecipientGroupId.Equals(request.GroupId))
                                         .OrderByDescending(item => item.CreateDatetimeUtc)
                                         .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize); // sort by so front can show latest first
            response.Response = response.Response.OrderBy(item => item.SentDate).ToList();

            return response;

        }
    }
}
