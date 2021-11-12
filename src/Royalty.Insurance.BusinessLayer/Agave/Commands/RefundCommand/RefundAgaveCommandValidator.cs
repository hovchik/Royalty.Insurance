using FluentValidation;
using ServiceStack;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class RefundAgaveCommandValidator : AbstractValidator<RefundAgaveCommand>
    {
        public RefundAgaveCommandValidator()
        {
            RuleFor(x => x.AgaveRoyaltyRefund).NotNull();
            RuleFor(x => x.AgaveRoyaltyRefund.TransactionRequest).NotNull();
            RuleFor(x => x.AgaveRoyaltyRefund.TransactionRequest.Order.Return).NotNull();
            RuleFor(x => x.AgaveRoyaltyRefund.TransactionRequest.Order.Return.ReferenceNum).NotNull().NotEmpty();
            RuleFor(x => x.AgaveRoyaltyRefund.TransactionRequest.Order.Return.OrderID).NotNull().NotEmpty();
            RuleFor(x => x.AgaveRoyaltyRefund.TransactionRequest.Order.Return.Payment).NotNull();
            RuleFor(x => x.AgaveRoyaltyRefund.TransactionRequest.Order.Return.Payment.ChargeTotal).NotNull().NotEmpty();
        }
    }
}