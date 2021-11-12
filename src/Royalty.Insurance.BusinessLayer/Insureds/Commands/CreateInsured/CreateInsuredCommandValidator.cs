using FluentValidation;
using Royalty.Insurance.Proxy.APIResponseModels;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class CreateInsuredCommandValidator : AbstractValidator<QuoteSheetRequest>
    {
        public CreateInsuredCommandValidator()
        {
            //not defined any validation rule
        }
    }
}
