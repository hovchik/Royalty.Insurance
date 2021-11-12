using System;
using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Common.Converters;
using System.Common.Exceptions;
using System.Common.Network;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class RefundAgaveCommandHandler : IRequestHandler<RefundAgaveCommand, AgaveRoyaltyResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpHelper _httpHelper;
        private readonly AppSetting _appSetting;
        private readonly IAgaveSaleMapperService _mapper;

        public RefundAgaveCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IHttpHelper httpHelper, IOptions<AppSetting> appSetting, IAgaveSaleMapperService mapper)
        {
            _context = context;
            _currentUserService = currentUserService;
            _httpHelper = httpHelper;
            _appSetting = appSetting.Value;
            _mapper = mapper;
        }

        public async Task<AgaveRoyaltyResponse> Handle(RefundAgaveCommand request, CancellationToken cancellationToken)
        {
            var refNumber = request.AgaveRoyaltyRefund.TransactionRequest.Order.Return.ReferenceNum;
            var orderId = request.AgaveRoyaltyRefund.TransactionRequest.Order.Return.OrderID;
            var totalCharge = request.AgaveRoyaltyRefund.TransactionRequest.Order.Return.Payment.ChargeTotal;

            var existingHistoryRecord = await _context.AgaveSalesHistories.Where(rec =>
                rec.ReferenceNum.ToString().Equals(refNumber) && rec.OrderId.Equals(orderId) && rec.ResponseMessage.Equals(AgaveResponseType.CAPTURED.ToString())).FirstOrDefaultAsync(cancellationToken);

            if (existingHistoryRecord == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            if (totalCharge > existingHistoryRecord.ChargeTotal)
            {
                throw new RestApiResponseException((int)HttpStatusCode.BadRequest, ResourceCommonMessage.ChargeTotalOverflow);
            }

            var mapObject = new AgaveMapParameters
            {
                MerchantId = _appSetting.AgaveSetting.MerchantId,
                MerchantKey = _appSetting.AgaveSetting.MerchantKey,
                RequestModel = request.AgaveRoyaltyRefund
            };

            var agaveRequest = _mapper.MapEntity(mapObject);

            var apiResponse = await _httpHelper.Post<AgaveSaleResponse, AgaveRefundRequest>(_appSetting.AgaveSetting.RefundUrl, agaveRequest, cancellationToken, new List<JsonConverter> { new IntToStringConverter() });

            AgaveRoyaltyResponse royaltyResponse = _mapper.MapResponse(apiResponse, request, _currentUserService.UserId, existingHistoryRecord);

            var entity = new AgaveSalesHistory { TransactionTypeId = (int)AgaveTransactionTypes.Refund };
            _mapper.UpdateEntity(entity, royaltyResponse, _currentUserService.UserId);

            try
            {
                await _context.AgaveSalesHistories.AddAsync(entity, cancellationToken);
                await using var transaction = await _context.BeginTransactionAsync(cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception e)
            {
                //
            }


            if (!string.IsNullOrEmpty(apiResponse.TransactionResponse.ErrorMessage))
            {
                throw new RestApiResponseException((int)HttpStatusCode.BadRequest, apiResponse.TransactionResponse.ErrorMessage);
            }

            return royaltyResponse;
        }
    }
}