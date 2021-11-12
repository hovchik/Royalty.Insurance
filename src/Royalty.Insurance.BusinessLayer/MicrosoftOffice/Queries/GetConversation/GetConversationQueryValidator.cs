using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice.Queries.GetConversation
{
    public class GetConversationQueryValidator : AbstractValidator<GetConversationQuery>
    {
        public GetConversationQueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.ConversationId).NotEmpty();
        }
    }
}
