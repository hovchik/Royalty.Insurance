using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetGroupByIdQueryValidator : AbstractValidator<GetGroupByIdQuery>
    {
        public GetGroupByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
