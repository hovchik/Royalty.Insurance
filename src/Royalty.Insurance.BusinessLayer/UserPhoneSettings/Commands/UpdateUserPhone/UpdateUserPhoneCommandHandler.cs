using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public class UpdateUserPhoneCommandHandler : IRequestHandler<UpdateUserPhoneCommand, UserPhoneResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly IUserPhoneMapperService _mapper;

        public UpdateUserPhoneCommandHandler(IUserPhoneMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<UserPhoneResponse> Handle(UpdateUserPhoneCommand request, CancellationToken cancellationToken)
        {
            UserPhone entity = await _context.UserPhones.Where(item => item.Id.Equals(request.Request.Id)).FirstOrDefaultAsync(cancellationToken);
            entity.UpdatedBy = _currentUserService.UserId;
            _mapper.UpdateEntity(entity, request.Request);
            _context.UserPhones.Update(entity);
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}