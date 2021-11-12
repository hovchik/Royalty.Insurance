using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetUserMessagesQueryHandler : IRequestHandler<GetUserMessagesQuery, PaginationResponse<FileMessageResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMessageMapperService _mapper;

        public GetUserMessagesQueryHandler(IApplicationDbContext context, IMessageMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<FileMessageResponse>> Handle(GetUserMessagesQuery request, CancellationToken cancellationToken)
        {
            var response = await _context.Messages
                                         .Where(item =>  item.SenderId.Equals(request.UserId)
                                                                || item.RecipientGroup.GroupMembers.Any(member => member.MemberId.Equals(request.UserId)))
                                         .OrderByDescending(item => item.CreateDatetimeUtc)
                                         
                                         .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize); // sort by so front can show latest first
            response.Response = response.Response.OrderBy(item => item.SentDate).ToList();

            return response;

        }
    }
}
