using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public interface IAgaveSaleMapperService
    {
        AgaveSaleRequest MapEntity(AgaveRoyaltySaleRequest source, int merchantId, string merchantKey);
        AgaveCheckRequest MapEntity(AgaveRoyaltyCheckRequest source, int merchantId, string merchantKey);
        AgaveRefundRequest MapEntity(AgaveMapParameters mapObject);

        AgaveRoyaltyResponse MapResponse(AgaveSaleResponse apiResponse, SaleAgaveCommand request);
        AgaveRoyaltyResponse MapResponse(AgaveSaleResponse apiResponse, RefundAgaveCommand request, int userId, AgaveSalesHistory refundTransaction);
        AgaveRoyaltyResponse MapResponse(AgaveSaleResponse apiResponse, eCheckAgaveCommand request);
        Expression<Func<AgaveSalesHistory, AgaveRoyaltyResponse>> MapSalesResponse { get; }

        void UpdateEntity(AgaveSalesHistory entity, AgaveCheckRequest apiResponse, int userId);
        void UpdateEntity(AgaveSalesHistory entity, AgaveSaleRequest apiResponse, int userId);
        void UpdateEntity(AgaveSalesHistory entity, AgaveRoyaltyResponse royaltyResponse, int userId);
        Expression<Func<AgaveTransactionType, AgaveTransactionTypeResponse>> MapTransactionTypes { get; }

    }
}