using FluentValidation;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class UpdateSavedRequestCommandValidator : AbstractValidator<SavedRequestResponse>
    {
        public UpdateSavedRequestCommandValidator()
        {
            RuleFor(x => x.Request).NotEmpty().NotNull();
            RuleFor(x => x.ShortDescription).NotNull().NotEmpty().MaximumLength(200);
        }
    }
}