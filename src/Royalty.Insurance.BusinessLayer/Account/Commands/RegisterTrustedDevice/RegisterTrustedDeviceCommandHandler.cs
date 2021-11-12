using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Account.Commands.RegisterTrustedDevice
{
    public class RegisterTrustedDeviceCommandHandler : IRequestHandler<RegisterTrustedDeviceCommand, Unit>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public RegisterTrustedDeviceCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(RegisterTrustedDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = new UserTrustedDevice
                {
                    DeviceId = request.DeviceId,
                    UserId = _currentUserService.UserId
            }
                ;
            await _context.UserTrustedDevices.AddAsync(device, cancellationToken);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return Unit.Value;
        }
    }
}
