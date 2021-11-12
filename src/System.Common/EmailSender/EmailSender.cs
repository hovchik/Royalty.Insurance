using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Common.Validator;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace System.Common.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSetting _emailSetting;
        private readonly SendGridClient _client;

        public EmailSender(IOptions<AppSetting> options)
        {
            _emailSetting = options.Value.EmailSetting;
            _client = new SendGridClient(_emailSetting.ApiKey);
        }

        public async Task Send(EmailMessage message)
        {
            if (EmailValidator.IsValidEmail(message.To))
            {
                throw  new RestApiResponseException(ResourceCommonMessage.EmailAddressIsNotValid);
            }
            var from = new EmailAddress(_emailSetting.Email, "No Reply");
            var subject = message.Subject;
            var to = new EmailAddress(message.To, "User");
            var plainTextContent = message.Content;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, plainTextContent);
            await _client.SendEmailAsync(msg);
        }
    }
}
