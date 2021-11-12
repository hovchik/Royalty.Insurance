
using System.Common.Exceptions;
using Royalty.Insurance.Settings;

namespace System.Common.Validator
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
#if DEBUG
            return true;
#endif
            try
            {
                var eMailAddress = new System.Net.Mail.MailAddress(email);
                return eMailAddress.Address == email;
            }
            catch
            {
                throw new RestApiResponseException(ResourceCommonMessage.EmailAddressIsNotValid);
            }
        }
    }
}
