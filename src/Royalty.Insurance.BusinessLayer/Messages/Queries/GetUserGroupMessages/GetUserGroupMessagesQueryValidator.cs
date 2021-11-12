using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class GetUserGroupMessagesQueryValidator : AbstractValidator<GetUserGroupMessagesQuery>
    {
        public GetUserGroupMessagesQueryValidator()
        {
            RuleFor(v => v.UserId)
                .GreaterThan(0);
            RuleFor(v => v.GroupId)
                .GreaterThan(0);
            RuleFor(v => v.PageIndex)
                .GreaterThan(0);
            RuleFor(v => v.PageSize)
                .GreaterThan(0);
        }
    }
}
