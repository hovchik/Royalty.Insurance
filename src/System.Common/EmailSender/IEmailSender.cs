using System.Common.Authentication.Models;
using System.Threading.Tasks;

namespace System.Common.EmailSender
{
    public interface IEmailSender
    {
        Task Send(EmailMessage message);
    }
}
