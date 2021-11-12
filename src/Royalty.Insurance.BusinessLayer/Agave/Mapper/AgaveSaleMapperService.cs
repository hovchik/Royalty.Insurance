using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class AgaveSaleMapperService : IAgaveSaleMapperService
    {

        public AgaveSaleRequest MapEntity(AgaveRoyaltySaleRequest source, int merchantId, string merchantKey)
        {
            return new AgaveSaleRequest
            {
                TransactionRequest = new TransactionRequest
                {
                    Order = source.TransactionRequest.Order,
                    Version = source.TransactionRequest.Version,
                    Verification = new Verification
                    {
                        MerchantId = merchantId,
                        MerchantKey = merchantKey
                    }
                }
            };
        }

        public AgaveCheckRequest MapEntity(AgaveRoyaltyCheckRequest source, int merchantId, string merchantKey)
        {
            return new AgaveCheckRequest
            {
                TransactionRequest = new eCheckTransactionRequest
                {
                    Order = source.TransactionRequest.Order,
                    Version = source.TransactionRequest.Version,
                    Verification = new Verification
                    {
                        MerchantId = merchantId,
                        MerchantKey = merchantKey
                    }
                }
            };
        }

        public AgaveRefundRequest MapEntity(AgaveMapParameters mapObject)
        {
            return new AgaveRefundRequest
            {
                TransactionRequest = new TransactionRequestRefund
                {
                    Order = mapObject.RequestModel.TransactionRequest.Order,
                    Version = mapObject.RequestModel.TransactionRequest.Version,
                    Verification = new Verification
                    {
                        MerchantKey = mapObject.MerchantKey,
                        MerchantId = mapObject.MerchantId
                    }
                }
            };
        }

        public AgaveRoyaltyResponse MapResponse(AgaveSaleResponse apiResponse, SaleAgaveCommand request)
        {
            return new AgaveRoyaltyResponse
            {
                AccountNumber = apiResponse.TransactionResponse.AccountNumber,
                AuthCode = apiResponse.TransactionResponse.AuthCode,
                AvsResponseCode = apiResponse.TransactionResponse.AvsResponseCode,
                CardHolderAddress = request.AgaveSaleRequest.TransactionRequest.Order.Sale.Billing.Address,
                CardHolderCity = request.AgaveSaleRequest.TransactionRequest.Order.Sale.Billing.City,
                CardHolderEmail = request.AgaveSaleRequest.TransactionRequest.Order.Sale.Billing.Email,
                CardholderName = request.AgaveSaleRequest.TransactionRequest.Order.Sale.Billing.Name,
                CardHolderPhone = request.AgaveSaleRequest.TransactionRequest.Order.Sale.Billing.Phone,
                CardHolderState = request.AgaveSaleRequest.TransactionRequest.Order.Sale.Billing.State,
                CardHolderZip = request.AgaveSaleRequest.TransactionRequest.Order.Sale.Billing.Postalcode,
                ChargeTotal = apiResponse.TransactionResponse.ChargeTotal,
                CreateDateTimeUtc = DateTime.UtcNow,
                CreditCardCountry = apiResponse.TransactionResponse.CreditCardCountry,
                CreditCardScheme = apiResponse.TransactionResponse.CreditCardScheme,
                CvvResponseCode = apiResponse.TransactionResponse.CvvResponseCode,
                ErrorMessage = apiResponse.TransactionResponse.ErrorMessage,
                MerchantTransactionDate = apiResponse.TransactionResponse.MerchantTransactionDate,
                MerchantTransactionTime = apiResponse.TransactionResponse.MerchantTransactionTime,
                OrderID = apiResponse.TransactionResponse.OrderID,
                ProcessorCode = apiResponse.TransactionResponse.ProcessorCode,
                ProcessorMessage = apiResponse.TransactionResponse.ProcessorMessage,
                ReferenceNum = apiResponse.TransactionResponse.ReferenceNum,
                ResponseCode = apiResponse.TransactionResponse.ResponseCode,
                ResponseMessage = apiResponse.TransactionResponse.ResponseMessage,
                TransactionID = apiResponse.TransactionResponse.TransactionID,
                TransactionTimestamp = apiResponse.TransactionResponse.TransactionTimestamp,
            };
        }

        public AgaveRoyaltyResponse MapResponse(AgaveSaleResponse apiResponse, RefundAgaveCommand request, int userId, AgaveSalesHistory refundTransaction)
        {
            return new AgaveRoyaltyResponse
            {
                AccountNumber = apiResponse.TransactionResponse.AccountNumber,
                AuthCode = apiResponse.TransactionResponse.AuthCode,
                AvsResponseCode = apiResponse.TransactionResponse.AvsResponseCode,
                ChargeTotal = apiResponse.TransactionResponse.ChargeTotal,
                CreateDateTimeUtc = DateTime.UtcNow,
                CreditCardCountry = apiResponse.TransactionResponse.CreditCardCountry,
                CreditCardScheme = apiResponse.TransactionResponse.CreditCardScheme,
                CvvResponseCode = apiResponse.TransactionResponse.CvvResponseCode,
                ErrorMessage = apiResponse.TransactionResponse.ErrorMessage,
                MerchantTransactionDate = apiResponse.TransactionResponse.MerchantTransactionDate,
                MerchantTransactionTime = apiResponse.TransactionResponse.MerchantTransactionTime,
                OrderID = apiResponse.TransactionResponse.OrderID,
                ProcessorCode = apiResponse.TransactionResponse.ProcessorCode,
                ProcessorMessage = apiResponse.TransactionResponse.ProcessorMessage,
                ReferenceNum = apiResponse.TransactionResponse.ReferenceNum,
                ResponseCode = apiResponse.TransactionResponse.ResponseCode,
                ResponseMessage = apiResponse.TransactionResponse.ResponseMessage,
                TransactionID = apiResponse.TransactionResponse.TransactionID,
                TransactionTimestamp = apiResponse.TransactionResponse.TransactionTimestamp,
                UserId = userId,
                CardHolderCity = refundTransaction.CardHolderCity,
                CardHolderEmail = refundTransaction.CardHolderEmail,
                CardholderName = refundTransaction.CardHolderName,
                CardHolderPhone = refundTransaction.CardHolderPhone,
                CardHolderState = refundTransaction.CardHolderState,
                CardHolderZip = refundTransaction.CardHolderZip ?? default,
                CardHolderAddress = refundTransaction.CardHolderAddress
            };
        }

        public AgaveRoyaltyResponse MapResponse(AgaveSaleResponse apiResponse, eCheckAgaveCommand request)
        {
            return new AgaveRoyaltyResponse
            {
                AccountNumber = apiResponse.TransactionResponse.AccountNumber,
                AuthCode = apiResponse.TransactionResponse.AuthCode,
                AvsResponseCode = apiResponse.TransactionResponse.AvsResponseCode,
                CardHolderAddress = request.AgaveCheckRequest.TransactionRequest.Order.Sale.Billing.Address,
                CardHolderCity = request.AgaveCheckRequest.TransactionRequest.Order.Sale.Billing.City,
                CardHolderEmail = request.AgaveCheckRequest.TransactionRequest.Order.Sale.Billing.Email,
                CardholderName = request.AgaveCheckRequest.TransactionRequest.Order.Sale.Billing.Name,
                CardHolderPhone = request.AgaveCheckRequest.TransactionRequest.Order.Sale.Billing.Phone,
                CardHolderState = request.AgaveCheckRequest.TransactionRequest.Order.Sale.Billing.State,
                CardHolderZip = request.AgaveCheckRequest.TransactionRequest.Order.Sale.Billing.Postalcode,
                ChargeTotal = apiResponse.TransactionResponse.ChargeTotal,
                CreateDateTimeUtc = DateTime.UtcNow,
                CreditCardCountry = apiResponse.TransactionResponse.CreditCardCountry,
                CreditCardScheme = apiResponse.TransactionResponse.CreditCardScheme,
                CvvResponseCode = apiResponse.TransactionResponse.CvvResponseCode,
                ErrorMessage = apiResponse.TransactionResponse.ErrorMessage,
                MerchantTransactionDate = apiResponse.TransactionResponse.MerchantTransactionDate,
                MerchantTransactionTime = apiResponse.TransactionResponse.MerchantTransactionTime,
                OrderID = apiResponse.TransactionResponse.OrderID,
                ProcessorCode = apiResponse.TransactionResponse.ProcessorCode,
                ProcessorMessage = apiResponse.TransactionResponse.ProcessorMessage,
                ReferenceNum = apiResponse.TransactionResponse.ReferenceNum,
                ResponseCode = apiResponse.TransactionResponse.ResponseCode,
                ResponseMessage = apiResponse.TransactionResponse.ResponseMessage,
                TransactionID = apiResponse.TransactionResponse.TransactionID,
                TransactionTimestamp = apiResponse.TransactionResponse.TransactionTimestamp,
            };
        }

        public void UpdateEntity(AgaveSalesHistory entity, AgaveRoyaltyResponse royaltyResponse, int userId)
        {
            entity.AuthCode = royaltyResponse.AuthCode;
            entity.AvsResponseCode = royaltyResponse.AvsResponseCode;
            entity.UserId = royaltyResponse.UserId;
            entity.CardHolderAddress = royaltyResponse.CardHolderAddress;
            entity.ErrorMessage = royaltyResponse.ErrorMessage;
            entity.ResponseCode = royaltyResponse.ResponseCode;
            entity.CvvResponseCode = royaltyResponse.CvvResponseCode;
            entity.ProcessorMessage = royaltyResponse.ProcessorMessage;
            entity.ReferenceNum = royaltyResponse.ReferenceNum;
            entity.ResponseMessage = royaltyResponse.ResponseMessage;
            entity.ProcessorCode = royaltyResponse.ProcessorCode;
            entity.TransactionTimestamp = royaltyResponse.TransactionTimestamp;
            entity.CardHolderCity = royaltyResponse.CardHolderCity;
            entity.CardHolderEmail = royaltyResponse.CardHolderEmail;
            entity.CardHolderName = royaltyResponse.CardholderName;
            entity.CardHolderPhone = royaltyResponse.CardHolderPhone;
            entity.CardHolderState = royaltyResponse.CardHolderState;
            entity.CardHolderZip = royaltyResponse.CardHolderZip;
            entity.ChargeTotal = royaltyResponse.ChargeTotal;
            entity.TransactionId = royaltyResponse.TransactionID;
            entity.MerchantTransactionDate = royaltyResponse.MerchantTransactionDate;
            entity.MerchantTransactionTime = royaltyResponse.MerchantTransactionTime;
            entity.UserId = userId;
            entity.AccountNumber = royaltyResponse.AccountNumber;
            entity.CreditCardScheme = royaltyResponse.CreditCardScheme;
            entity.OrderId = royaltyResponse.OrderID;
        }

        public void UpdateEntity(AgaveSalesHistory entity, AgaveSaleRequest apiResponse, int userId)
        {
            entity.CardHolderAddress = apiResponse.TransactionRequest.Order.Sale.Billing.Address;
            entity.CardHolderCity = apiResponse.TransactionRequest.Order.Sale.Billing.City;
            entity.CardHolderEmail = apiResponse.TransactionRequest.Order.Sale.Billing.Email;
            entity.CardHolderName = apiResponse.TransactionRequest.Order.Sale.Billing.Name;
            entity.CardHolderPhone = apiResponse.TransactionRequest.Order.Sale.Billing.Phone;
            entity.CardHolderState = apiResponse.TransactionRequest.Order.Sale.Billing.State;
            entity.CardHolderZip = apiResponse.TransactionRequest.Order.Sale.Billing.Postalcode;
            entity.ChargeTotal = Convert.ToInt32(apiResponse.TransactionRequest.Order.Sale.Payment.ChargeTotal);
            entity.UserId = userId;
            entity.AccountNumber = GenerateCreditCardNumber(apiResponse.TransactionRequest.Order.Sale.TransactionDetail.PayType.CreditCard.Number);
            entity.TransactionTypeId = (int)AgaveTransactionTypes.PreResponse;
            entity.AuthCode = default;
            entity.AvsResponseCode = default;
            entity.ErrorMessage = default;
            entity.ResponseCode = default;
            entity.CvvResponseCode = default;
            entity.ProcessorMessage = default;
            entity.ReferenceNum = default;
            entity.ResponseMessage = default;
            entity.ProcessorCode = default;
            entity.TransactionTimestamp = default;
            entity.TransactionId = default;
            entity.MerchantTransactionDate = default;
            entity.MerchantTransactionTime = default;
            entity.CreditCardScheme = default;
            entity.OrderId = default;
        }

        public void UpdateEntity(AgaveSalesHistory entity, AgaveCheckRequest apiResponse, int userId)
        {
            entity.CardHolderAddress = apiResponse.TransactionRequest.Order.Sale.Billing.Address;
            entity.CardHolderCity = apiResponse.TransactionRequest.Order.Sale.Billing.City;
            entity.CardHolderEmail = apiResponse.TransactionRequest.Order.Sale.Billing.Email;
            entity.CardHolderName = apiResponse.TransactionRequest.Order.Sale.Billing.Name;
            entity.CardHolderPhone = apiResponse.TransactionRequest.Order.Sale.Billing.Phone;
            entity.CardHolderState = apiResponse.TransactionRequest.Order.Sale.Billing.State;
            entity.CardHolderZip = apiResponse.TransactionRequest.Order.Sale.Billing.Postalcode;
            entity.ChargeTotal = Convert.ToInt32(apiResponse.TransactionRequest.Order.Sale.Payment.ChargeTotal);
            entity.UserId = userId;
            entity.AccountNumber =
                apiResponse.TransactionRequest.Order.Sale.TransactionDetail.PayType.Ach.AchAccountNumber;
            entity.TransactionTypeId = (int)AgaveTransactionTypes.PreResponse;
        }

        public Expression<Func<AgaveSalesHistory, AgaveRoyaltyResponse>> MapSalesResponse => entity => new AgaveRoyaltyResponse
        {
            ErrorMessage = entity.ErrorMessage,
            ReferenceNum = entity.ReferenceNum,
            CardHolderAddress = entity.CardHolderAddress,
            AuthCode = entity.AuthCode ?? default,
            ResponseMessage = entity.ResponseMessage,
            ChargeTotal = entity.ChargeTotal,
            OrderID = entity.OrderId,
            CardholderName = entity.CardHolderName,
            AccountNumber = entity.AccountNumber,
            CardHolderPhone = entity.CardHolderPhone,
            CardHolderCity = entity.CardHolderCity,
            CardHolderState = entity.CardHolderState,
            CardHolderEmail = entity.CardHolderEmail,
            CardHolderZip = entity.CardHolderZip ?? default,
            ResponseCode = entity.ResponseCode,
            CvvResponseCode = entity.CvvResponseCode,
            ProcessorMessage = entity.ProcessorMessage,
            AvsResponseCode = entity.AvsResponseCode,
            ProcessorCode = entity.ProcessorCode,
            TransactionTimestamp = entity.TransactionTimestamp ?? default,
            CreditCardScheme = entity.CreditCardScheme,
            MerchantTransactionTime = entity.MerchantTransactionTime,
            MerchantTransactionDate = entity.MerchantTransactionDate,
            CreateDateTimeUtc = entity.CreateDateTimeUtc,
            UserId = entity.UserId
        };

        public Expression<Func<AgaveTransactionType, AgaveTransactionTypeResponse>> MapTransactionTypes => entity =>
            new AgaveTransactionTypeResponse
            {
                Name = entity.Name,
                Id = entity.Id
            };

        private int GenerateCreditCardNumber(long number)
        {
            return Convert.ToInt32($"{number.ToString().Substring(0, 4)}{number.ToString().Substring(12, 4)}");
        }
    }
}