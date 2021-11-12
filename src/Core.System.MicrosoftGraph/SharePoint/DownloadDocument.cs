using System;
using System.Common.Authentication.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.Helpers;
using Microsoft.Extensions.Options;

namespace Core.System.MicrosoftGraph
{
    public class DownloadDocument : IDownloadDocument
    {
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;

        public DownloadDocument(IOptions<AppSetting> options)
        {
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }

        public async Task<Stream> Handle(DownloadDocumentRequest request, CancellationToken cancellationToken)
        {
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);

            var stream = await graphClient
                .Groups[request.GroupId]
                .Drive.Items[request.DriveItemId].Content
                .Request()
                .GetAsync(cancellationToken);

            return stream;
        }
    }
}
