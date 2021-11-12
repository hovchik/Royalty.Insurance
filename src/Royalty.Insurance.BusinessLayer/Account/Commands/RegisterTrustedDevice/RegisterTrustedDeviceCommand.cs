using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account.Commands.RegisterTrustedDevice
{
    public class RegisterTrustedDeviceCommand : IRequest<Unit>
    {
        public string DeviceId { get; set; }
    }
}
