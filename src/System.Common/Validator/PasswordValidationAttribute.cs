using System.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Royalty.Insurance.Settings;

namespace System.Common.Validator
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            ResourceCommonMessage.PasswordIsNotValid;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value is string)
            {
                string password = value.ToString();
                Debug.Assert(password != null, nameof(password) + " != null");
                var regex = new Regex(SystemConstants.PasswordValidationRegex);
                if (password.Length < 8 || !regex.IsMatch(password))
                {
                    throw new RestApiResponseException(ResourceCommonMessage.PasswordIsNotValid);
                }

                return ValidationResult.Success;
            }

            throw new RestApiResponseException(ResourceCommonMessage.PasswordIsNotValid);
        }
    }
}
