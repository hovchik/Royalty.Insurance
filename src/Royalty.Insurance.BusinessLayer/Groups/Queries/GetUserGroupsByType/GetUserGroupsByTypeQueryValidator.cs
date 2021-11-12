using FluentValidation;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Groups
{
    public class GetUserGroupsByTypeQueryValidator : AbstractValidator<GetUserGroupsByTypeQuery>
    {
        public GetUserGroupsByTypeQueryValidator()
        {
            RuleFor(x => (int)x.GroupTypeCode).GreaterThanOrEqualTo((int)GroupTypeCode.Individual).LessThanOrEqualTo((int)GroupTypeCode.Group);
        }
    }
}
