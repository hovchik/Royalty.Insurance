using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.SavedRequests
{
    public class CreateSavedRequestCommandValidator : AbstractValidator<CreateSavedRequestCommand>
    {
        public CreateSavedRequestCommandValidator()
        {
            RuleFor(x => x.Request).NotEmpty().NotNull();
            RuleFor(x => x.ShortDescription).NotEmpty().MaximumLength(200);
        }
    }
}