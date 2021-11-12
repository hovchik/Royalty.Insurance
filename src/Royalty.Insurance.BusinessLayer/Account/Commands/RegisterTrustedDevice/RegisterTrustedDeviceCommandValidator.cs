using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Account.Commands.RegisterTrustedDevice
{
    public class RegisterTrustedDeviceCommandValidator : AbstractValidator<RegisterTrustedDeviceCommand>
    {
        public RegisterTrustedDeviceCommandValidator()
        {
            RuleFor(x => x.DeviceId).NotEmpty().MaximumLength(128);
        }
    }
}
