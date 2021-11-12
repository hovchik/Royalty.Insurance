using FluentValidation;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public class CreatePhoneCommandValidator : AbstractValidator<PhoneBookResponse>
    {
        public CreatePhoneCommandValidator()
        {
            RuleFor(x => x.Number).NotEmpty().MaximumLength(50);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        }
    }
}
