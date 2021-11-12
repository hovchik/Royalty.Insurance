
namespace System.Common.Authentication.Models
{
    public class SmtpSetting
    {
        public int Port { get; set; }
        
        public string Server { get; set; }

        public string From { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

    }
}
