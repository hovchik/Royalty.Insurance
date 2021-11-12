using MediatR;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.SavedRequests;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using Royalty.Insurance.Proxy.Response;
using System.Common.Authentication.Models;
using System.Common.Network;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetSearchingRootQueryHandler : IRequestHandler<GetSearchingRootQuery, SearchingRoot>
    {
        private readonly IHttpHelper _client;
        private readonly AppSetting _appSetting;
        private readonly IRequestHandler<CreateSavedRequestCommand, SavedRequestResponse> _handler;

        public GetSearchingRootQueryHandler(IOptions<AppSetting> options, IHttpHelper client, IRequestHandler<CreateSavedRequestCommand, SavedRequestResponse> handler)
        {
            _appSetting = options.Value;
            _client = client;
            _handler = handler;
        }

        public async Task<SearchingRoot> Handle(GetSearchingRootQuery request, CancellationToken cancellationToken)
        {
            var result = await _client.Get<SearchingRoot>($@"https://ws.cabadvantage.com/rest/services/sales/search?key={_appSetting.CabKey}&{request}");

            var resp = await _handler.Handle(new CreateSavedRequestCommand
            {
                ShortDescription = request.Request.Substring(0, 30),
                Request = request.Request
            }, cancellationToken);

            return result;
        }
    }
}
