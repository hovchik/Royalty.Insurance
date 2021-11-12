using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class CheckUserFileExistsQueryHandler : IRequestHandler<CheckUserFileExistsQuery, BaseResponse<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CheckUserFileExistsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponse<bool>> Handle(CheckUserFileExistsQuery request, CancellationToken cancellationToken)
        {
            var isRecordExists =
                await _context.UserGarages.AnyAsync(x => x.Path == request.FileName && x.UserId == _currentUserService.UserId,
                    cancellationToken); // check if file is the same and owner is current user

            return new BaseResponse<bool>(isRecordExists);
        }
    }
}
