using System.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetMessageByIdQueryHandler : IRequestHandler<GetMessageByIdQuery, FileMessageResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMessageMapperService _mapper;

        public GetMessageByIdQueryHandler(IApplicationDbContext context, IMessageMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FileMessageResponse> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Messages.Where(item => item.Id.Equals(request.Id))
                                                .Select(_mapper.MapResponse)
                                                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}
