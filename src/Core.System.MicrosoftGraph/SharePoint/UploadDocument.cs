using System.Common.Authentication.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace Core.System.MicrosoftGraph
{
    public class UploadDocument : IUploadDocument
    {
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;

        public UploadDocument(IOptions<AppSetting> options)
        {
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }

        public async Task<UploadDocumentResponse> Handle(UploadDocumentRequest request, CancellationToken cancellationToken)
        {
            request.DocumentStream.Seek(0, SeekOrigin.Begin);
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);
            var rootFolderId = await graphClient.Groups[request.GroupId]
                .Drive.Root
                .Request()
                .GetAsync(cancellationToken);
            
            var result = await graphClient.Groups[request.GroupId]
                .Drive
                .Items[rootFolderId.Id]
                .ItemWithPath(request.FileName)
                .Content
                .Request()
                .PutAsync<DriveItem>(request.DocumentStream, cancellationToken);

            return new UploadDocumentResponse(request.GroupId, result.Id, result.WebUrl);
        }
    }
}
