using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.Helpers;
using Microsoft.Extensions.Options;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public class GetMailFolder : IGetMailFolder
    {
        //todo map folders from Office 365 to ours
        private readonly Dictionary<string, string> _folders = new Dictionary<string, string>()
        {
            {
                "Inbox", "Inbox"
            },
            {
                "Drafts", "Drafts"
            },
            {
                "Sent Items", "Sent Items"
            },
            {
                "Deleted Items", "Deleted"
            },
            // "Starred",TODO: maybe need just filter
            {
                "Junk Email" , "Spam"
            }

        };

        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;
        public GetMailFolder(IOptions<AppSetting> options)
        {
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }

        public async Task<IEnumerable<MicrosoftOfficeMailFolderResponse>> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);
            var response = await graphClient.Users[request.Email].MailFolders.Request().GetAsync(cancellationToken);

            return response.Where(item => _folders.Keys.Contains(item.DisplayName))
                .Select(item => new MicrosoftOfficeMailFolderResponse
                {
                    DisplayName = _folders[item.DisplayName],
                    FolderId = item.Id
                });
        }
    }
}
