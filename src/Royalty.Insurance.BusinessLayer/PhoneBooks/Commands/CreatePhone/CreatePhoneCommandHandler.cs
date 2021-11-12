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

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public class CreatePhoneCommandHandler : IRequestHandler<CreatePhoneCommand, PhoneBookResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPhoneBookMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreatePhoneCommandHandler(IApplicationDbContext context, IPhoneBookMapperService mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<PhoneBookResponse> Handle(CreatePhoneCommand request, CancellationToken cancellationToken)
        {
            PhoneBook entity = new PhoneBook { UserId = _currentUser.UserId };
            _mapper.UpdateEntity(entity, request);
            await _context.PhoneBooks.AddAsync(entity);
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
