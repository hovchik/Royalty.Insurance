using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Common.Validator;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Settings;

namespace System.Common.EmailSender
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly ILogger<SmtpEmailSender> _logger;
        private readonly AppSetting _appSetting;

        public SmtpEmailSender(ILogger<SmtpEmailSender> logger, IOptions<AppSetting> options)
        {
            _logger = logger;
            _appSetting = options.Value;
        }

        public async Task Send(EmailMessage message)
        {
            try
            {
                if (!EmailValidator.IsValidEmail(message.To))
                {
                    throw new RestApiResponseException(ResourceCommonMessage.EmailAddressIsNotValid);
                }
                MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(message.To, message.To));

                // From
                mailMsg.From = new MailAddress(_appSetting.Smtp.From, "do not reply");

                // Subject and multipart/alternative Body
                mailMsg.Subject = message.Subject;

                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Content, null, MediaTypeNames.Text.Plain));
                await Task.Run(() => {
                    using SmtpClient smtpClient = new SmtpClient(_appSetting.Smtp.Server)
                    {
                        Port = _appSetting.Smtp.Port,
                        EnableSsl = true,
                        Credentials = new NetworkCredential(_appSetting.Smtp.Login, _appSetting.Smtp.Password)
                    };

                    smtpClient.Send(mailMsg);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
