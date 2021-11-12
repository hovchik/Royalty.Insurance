using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Common.Converters;
using System.Common.Exceptions;
using System.Common.Network;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class eCheckAgaveCommandHandler : IRequestHandler<eCheckAgaveCommand, AgaveRoyaltyResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpHelper _httpHelper;
        private readonly AppSetting _appSetting;
        private readonly IAgaveSaleMapperService _mapper;

        public eCheckAgaveCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IHttpHelper httpHelper, IAgaveSaleMapperService mapper, IOptions<AppSetting> appSetting)
        {
            _context = context;
            _currentUserService = currentUserService;
            _httpHelper = httpHelper;
            _mapper = mapper;
            _appSetting = appSetting.Value;
        }

        public async Task<AgaveRoyaltyResponse> Handle(eCheckAgaveCommand request, CancellationToken cancellationToken)
        {
            var agaveRequest = _mapper.MapEntity(request.AgaveCheckRequest, _appSetting.AgaveSetting.MerchantId,
                              _appSetting.AgaveSetting.MerchantKey);

            var entity = new AgaveSalesHistory();
            _mapper.UpdateEntity(entity, agaveRequest, _currentUserService.UserId);
            await _context.AgaveSalesHistories.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var apiResponse = await _httpHelper.Post<AgaveSaleResponse, AgaveCheckRequest>(_appSetting.AgaveSetting.SaleUrl, agaveRequest, cancellationToken, new List<JsonConverter>(){new IntToStringConverter()});

            if (apiResponse.TransactionResponse == null)
            {
                throw  new RestApiResponseException((int)HttpStatusCode.BadRequest,"test");
            }

            var royaltyResponse = _mapper.MapResponse(apiResponse, request);

            _mapper.UpdateEntity(entity, royaltyResponse, _currentUserService.UserId);
            entity.TransactionTypeId = (int)AgaveTransactionTypes.eCheckSale;

            _context.AgaveSalesHistories.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);


            return royaltyResponse;

        }
    }
}