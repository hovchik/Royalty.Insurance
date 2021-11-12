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

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public class UpdatePhoneCommandHandler : IRequestHandler<UpdatePhoneCommand, PhoneBookResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPhoneBookMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public UpdatePhoneCommandHandler(IApplicationDbContext context, IPhoneBookMapperService mapper, ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<PhoneBookResponse> Handle(UpdatePhoneCommand request, CancellationToken cancellationToken)
        {
            PhoneBook entity = await _context.PhoneBooks.Where(item => item.Id.Equals(request.Id) && item.UserId == _currentUser.UserId).FirstOrDefaultAsync();
            _mapper.UpdateEntity(entity, request);
            _context.PhoneBooks.Update(entity);
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
