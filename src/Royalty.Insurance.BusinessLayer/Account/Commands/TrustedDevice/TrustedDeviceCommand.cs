using Domain;
using MediatR;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public class TrustedDeviceCommand : IRequest<bool>
    {
        public User User { get; set; }

        public string DeviceId { get; set; }
    }
}