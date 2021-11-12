using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Agencies
{
    public class UpdateAgencyCommandValidator : AbstractValidator<UpdateAgencyCommand>
    {
        public UpdateAgencyCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(128).WithMessage("Name should be less or equal 128");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(255).WithMessage("Address  should be less or equal 255");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required")
                .MaximumLength(255).WithMessage("State  should be less or equal 255");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(255).WithMessage("City  should be less or equal 255");


            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Zip is required")
                .MaximumLength(7).WithMessage("Zip should be less or equal 255");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .MaximumLength(15).WithMessage("Phone number should be less or equal 15");

            RuleFor(x => x.FaxNumber)
                .NotEmpty().WithMessage("Fax Number is required")
                .MaximumLength(255).WithMessage("Fax Number  should be less or equal 255");

        }
    }
}
