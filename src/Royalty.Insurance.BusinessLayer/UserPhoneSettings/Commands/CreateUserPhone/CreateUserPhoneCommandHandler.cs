using LinqKit;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public class CreateUserPhoneCommandHandler : IRequestHandler<CreateUserPhoneCommand, UserPhoneResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserPhoneMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateUserPhoneCommandHandler(IUserPhoneMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<UserPhoneResponse> Handle(CreateUserPhoneCommand request, CancellationToken cancellationToken)
        {
            UserPhone entity = new UserPhone { CreatedBy = _currentUserService.UserId, UpdatedBy = _currentUserService.UserId };
            _mapper.UpdateEntity(entity, request.Request);
            await _context.UserPhones.AddAsync(entity);
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}