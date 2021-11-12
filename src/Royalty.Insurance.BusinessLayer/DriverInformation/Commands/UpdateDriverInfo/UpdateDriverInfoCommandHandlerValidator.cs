using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.DriverInfo
{
    public class UpdateDriverInfoCommandHandlerValidator : AbstractValidator<UpdateDriverInfoCommand>
    {
        public UpdateDriverInfoCommandHandlerValidator()
        {
            RuleFor(x => x.DateHired).NotEmpty();
            RuleFor(x => x.LicenseNumber).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.DriverName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.YearOfExperiance).NotEmpty();
        }
    }
}