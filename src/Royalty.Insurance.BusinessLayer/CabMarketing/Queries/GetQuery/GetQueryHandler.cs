using MediatR;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System;
using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Common.Network;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetQueryHandler : IRequestHandler<GetQuery, PaginationResponse<DetailedSearch>>
    {
        private readonly IRequestHandler<GetSearchingRootQuery, SearchingRoot> _handler;
        private readonly IHttpHelper _client;
        private readonly AppSetting _appSetting;

        public GetQueryHandler(IRequestHandler<GetSearchingRootQuery, SearchingRoot> handler, IHttpHelper client, IOptions<AppSetting> options)
        {
            _handler = handler;
            _client = client;
            _appSetting = options.Value;
        }

        public async Task<PaginationResponse<DetailedSearch>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var responseUUID = await _handler.Handle(new GetSearchingRootQuery
            {
                Request = request.Request.ToString()
            }, cancellationToken);

            if (responseUUID.amt == 0)
            {
                throw new RestApiResponseException(ResourceCommonMessage.CarrierNotFoundByCondition);
            }
            var returnModel = await _client.Get<List<DetailedSearch>>(
                $@"https://ws.cabadvantage.com/rest/services/sales/json/results/{responseUUID.UUID}/{request.CabIndex}?key={_appSetting.CabKey}");

            var response = new PaginationResponse<DetailedSearch>
            {
                CurrentPage = request.PageIndex,
                PageSize = request.PageSize,
                RowCount = returnModel.Count
            };

            var pageCount = (double)response.RowCount / request.PageSize;
            response.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (request.PageIndex - 1) * request.PageSize;

            response.Response = returnModel.Skip(skip) // default
                .Take(request.PageSize)
                .ToList();

            return response;

        }
    }
}
