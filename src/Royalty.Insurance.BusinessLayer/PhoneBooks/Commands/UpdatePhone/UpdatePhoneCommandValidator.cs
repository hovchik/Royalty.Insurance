using FluentValidation;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.PhoneBooks
{
    public class UpdatePhoneCommandValidator : AbstractValidator<PhoneBookResponse>
    {
        public UpdatePhoneCommandValidator()
        {
            RuleFor(x => x.Number).NotEmpty().MaximumLength(50);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
        }
    }
}
