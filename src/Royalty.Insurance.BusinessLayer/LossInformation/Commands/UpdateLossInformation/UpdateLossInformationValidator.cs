using System;
using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.LossInfo
{
    public class UpdateLossInformationValidator : AbstractValidator<UpdateLossInformationCommand>
    {
        public UpdateLossInformationValidator()
        {
            RuleFor(x=>x.LesseeName).MaximumLength(50);
            RuleFor(x=>x.LesseeMCNumber).MaximumLength(50);
            RuleFor(x=>x.ExpireDate).NotNull().NotEmpty().LessThan(p => DateTime.UtcNow).GreaterThan(p=>DateTime.Parse("01-01-1850"));
            RuleFor(x=>x.EffectiveDate).NotNull().NotEmpty();
            RuleFor(x=>x.InsuranceName).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x=>x.NumberOfClaims).NotNull().NotEmpty().MaximumLength(50);
        }
    }
}