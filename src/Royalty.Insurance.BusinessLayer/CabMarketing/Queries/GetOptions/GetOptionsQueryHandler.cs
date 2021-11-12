using MediatR;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Common.Converters;
using System.Common.Network;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetOptionsQueryHandler : IRequestHandler<GetOptionsQuery, CabMarketingOptions>
    {
        private readonly IHttpHelper _client;
        private readonly AppSetting _appSetting;

        public GetOptionsQueryHandler(IHttpHelper client, IOptions<AppSetting> options)
        {
            _client = client;
            _appSetting = options.Value;
        }

        public async Task<CabMarketingOptions> Handle(GetOptionsQuery request, CancellationToken cancellationToken)
        {
            var dotCabModel = await _client.Get<CabMarketingOptions>($@"https://ws.cabadvantage.com/rest/services/sales/filters/options?key={_appSetting.CabKey}", new List<JsonConverter> { new Int32Converter() });

            return dotCabModel;
        }
    }
}
