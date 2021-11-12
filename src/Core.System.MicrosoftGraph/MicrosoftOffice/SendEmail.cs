using System.Common.Authentication.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public class SendEmail : ISendEmail
    {
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;
        public SendEmail(IOptions<AppSetting> options)
        {
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }

        public async Task Handle(MicrosoftOfficeMessageRequest request, CancellationToken cancellationToken)
        {
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);

            var message = new Message
            {
                Subject = request.Subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = request.Body
                },
                ToRecipients = request.ToRecipients.Select(item =>
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = item
                        }
                    }),
                CcRecipients = request.CcRecipients.Select(item =>
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = item
                        }
                    }),
                Attachments = await GraphServiceClientHelper.GetAttachments(request.Attachments)

            };

            await graphClient.Users[request.FromEmail].SendMail(message, true).Request().PostAsync(cancellationToken);
        }
    }
}
