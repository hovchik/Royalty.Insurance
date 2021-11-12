using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.DriverInfo
{
    public class UpdateDriverInfoCommandHandler : IRequestHandler<UpdateDriverInfoCommand, DriverInfoResponse>
    {
        private readonly IDriverInfoMapperService _mapper;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateDriverInfoCommandHandler(IDriverInfoMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<DriverInfoResponse> Handle(UpdateDriverInfoCommand request, CancellationToken cancellationToken)
        {
            int userId = _currentUserService.UserId;

            var entity = await _context.DriverInformations.Where(item => item.Id.Equals(request.Id)).FirstOrDefaultAsync(cancellationToken);
            entity.UpdatedBy = userId;
            _mapper.UpdateEntity(entity, request);
            _context.DriverInformations.Update(entity);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}