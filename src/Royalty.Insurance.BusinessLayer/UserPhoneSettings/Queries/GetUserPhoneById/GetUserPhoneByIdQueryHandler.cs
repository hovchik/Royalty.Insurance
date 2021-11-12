using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public class GetUserPhoneByIdQueryHandler : IRequestHandler<GetUserPhoneByIdQuery, UserPhoneResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserPhoneMapperService _mapper;

        public GetUserPhoneByIdQueryHandler(IApplicationDbContext context, IUserPhoneMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserPhoneResponse> Handle(GetUserPhoneByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserPhones
                .Where(item => item.Id.Equals(request.Id))
                .Select(_mapper.MapResponse)
                .FirstOrDefaultAsync(cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}