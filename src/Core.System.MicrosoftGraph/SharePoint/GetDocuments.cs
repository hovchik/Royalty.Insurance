using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.Helpers;
using Microsoft.Extensions.Options;

namespace Core.System.MicrosoftGraph
{
    public class GetDocuments : IGetDocuments
    {
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;
        private readonly IGetDefaultPrivateGroupId _defaultPrivateGroup;

        public GetDocuments(IOptions<AppSetting> options, IGetDefaultPrivateGroupId defaultPrivateGroup)
        {
            _defaultPrivateGroup = defaultPrivateGroup;
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }

        public async Task<DocumentListViewModel> Handle(GetDocumentsRequest request, CancellationToken cancellationToken)
        {
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);

            var group = await _defaultPrivateGroup.Handle(cancellationToken);// we will have only one group
            var groupFiles = await graphClient
                .Groups[group]
                .Drive.Root.Children
                .Request()//TODO: maybe filter needed
                .FilterStartWith("name", request.FileName)
                //.OrderBy("createdDateTime desc") this is not working leaving here in case next version supports it, uncomment
                .SkipToken(request.SkipToken)
                .Top(request.PageSize)
                .GetAsync(cancellationToken);

            List<UserDriveItem> documents = groupFiles
                .Select(item => new UserDriveItem { Id = item.Id, CreateDateTime = item.CreatedDateTime?.UtcDateTime, Name = item.Name, WebUrl = item.WebUrl })
                .ToList();

            return new DocumentListViewModel { Documents = documents, SkipToken = groupFiles.NextPageRequest?.QueryOptions?.FirstOrDefault(i => i.Name == "$skiptoken")?.Value };
        }
    }
}
