using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetUserMessagesQueryValidator : AbstractValidator<GetUserMessagesQuery>
    {
        public GetUserMessagesQueryValidator()
        {
            RuleFor(v => v.UserId)
                .GreaterThan(0);
            RuleFor(v => v.PageIndex)
                .GreaterThan(0);
            RuleFor(v => v.PageSize)
                .GreaterThan(0);
        }
    }
}
