using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class TrustedDeviceCommandHandler : IRequestHandler<TrustedDeviceCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public TrustedDeviceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(TrustedDeviceCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.DeviceId))
            {
                return await Task.FromResult(false);
            }

            return await _context.UserTrustedDevices.AnyAsync(item => item.UserId.Equals(request.User.Id) && item.DeviceId.Equals(request.DeviceId));
        }
    }
}
