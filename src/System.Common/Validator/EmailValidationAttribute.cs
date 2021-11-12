using System.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using Royalty.Insurance.Settings;

namespace System.Common.Validator
{
    public class EmailValidationAttribute : ValidationAttribute
    {

        public string GetErrorMessage() =>
            ResourceCommonMessage.EmailAddressIsNotValid;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value is string)
            {
                return EmailValidator.IsValidEmail(value.ToString()) ? ValidationResult.Success : new ValidationResult(GetErrorMessage());
            }

            throw new RestApiResponseException(ResourceCommonMessage.EmailAddressIsNotValid);
        }
    }
}
