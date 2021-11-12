using FluentValidation;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class SaleAgaveCommandValidator : AbstractValidator<SaleAgaveCommand>
    {
        public SaleAgaveCommandValidator()//todo clarification needed
        {
            RuleFor(x => x.AgaveSaleRequest).NotNull().NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest).NotNull().NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order).NotNull().NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale).NotNull().NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.ReferenceNum).NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.Payment).NotNull().NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.Payment.ChargeTotal).NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.TransactionDetail).NotNull().NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.TransactionDetail.PayType).NotNull().NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.TransactionDetail.PayType.CreditCard).NotNull().NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.TransactionDetail.PayType.CreditCard.CvvNumber).NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.TransactionDetail.PayType.CreditCard.ExpMonth).NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.TransactionDetail.PayType.CreditCard.ExpYear).NotEmpty();
            RuleFor(x => x.AgaveSaleRequest.TransactionRequest.Order.Sale.TransactionDetail.PayType.CreditCard.Number).NotEmpty();
        }
    }
}