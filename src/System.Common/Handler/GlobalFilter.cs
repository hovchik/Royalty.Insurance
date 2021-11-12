using System.Collections.Generic;
using System.Common.Exceptions;
using System.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using  Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Royalty.Insurance.Settings;

namespace System.Common.Handler
{
    public class GlobalFilter : IHubFilter
    {
        private readonly ILogger<GlobalFilter> _logger;

        public GlobalFilter(ILogger<GlobalFilter> logger)
        {
            _logger = logger;
        }

        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            try
            {
                Validate(invocationContext.HubMethodArguments);
                return await next(invocationContext);
            }
            catch (Exception e)
            {
                throw HandleErrorAndSendProperMessage(e);
            }
        }

        /// <summary>
        /// Validate model from signalr --Signalr does not have global validation yet, update when Microsoft implement
        /// </summary>
        /// <param name="hubarguments"></param>
        private void Validate(IReadOnlyList<object?>  hubarguments)
        {
            if (hubarguments == null)
            {
                return;
            }
            bool isValid = true;
            string errors = string.Empty;
            foreach (var argument in hubarguments)
            {
                var instanceType = argument.GetType();
                if (!instanceType.IsPrimitive)
                {
                    var validationContext = new ValidationContext(argument);
                    foreach (var propertyInfo in instanceType.GetProperties())
                    {
                        foreach (var validationAttribute in propertyInfo.GetCustomAttributes(typeof(ValidationAttribute), true))
                        {
                            ValidationAttribute validation = validationAttribute as ValidationAttribute;
                            if (!validation.IsValid(propertyInfo.GetValue(argument)))
                            {
                                isValid = false;
                                var result = validation.GetValidationResult(propertyInfo.GetValue(argument), validationContext);
                                errors = $"{result.ErrorMessage}{Environment.NewLine}";
                            }
                        }
                        
                    }
                }
            }

            if (!isValid)
            {
                throw  new RestApiResponseException(errors);
            }
        }

        private HubException HandleErrorAndSendProperMessage(Exception exception)
        {
            ApiErrorResponse apiErrorResponse;
            //TODO: change to return code according to case
            switch (exception)
            {

                case DbUpdateException dbUpdateException:
                    apiErrorResponse = HandleDbUpdateException(exception, dbUpdateException);
                    break;
                case RestApiResponseException responseException:
                    apiErrorResponse = new ApiErrorResponse(responseException.ErrorCode, responseException.Message);
                    break;
                default:
                    apiErrorResponse = new ApiErrorResponse((int)HttpStatusCode.InternalServerError, exception.Message);
                    break;
            }

            var serializerSetting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var jsonResult = JsonConvert.SerializeObject(apiErrorResponse, serializerSetting);
            _logger.LogError(exception, string.Join(",", apiErrorResponse.Message));
            return  new HubException(jsonResult);
        }

        private ApiErrorResponse HandleDbUpdateException(Exception exception, DbUpdateException sqlException)
        {
            _logger.LogError(exception, string.Join(",", sqlException.Message));
            //TODO : define our own error code rather than http
            ApiErrorResponse apiErrorResponse;
            if (sqlException.InnerException != null && sqlException.InnerException is SqlException sqlExceptionInnerException)
            {
                switch (sqlExceptionInnerException.Number)
                {
                    case 547: // Foreign Key violation
                        apiErrorResponse =
                            new ApiErrorResponse((int)HttpStatusCode.BadRequest, ResourceCommonMessage.RecordNotFound);
                        _logger.LogError(exception, string.Join(",", sqlExceptionInnerException.Message));
                        break;
                    case 2601: // Unique
                        apiErrorResponse =
                            new ApiErrorResponse((int)HttpStatusCode.BadRequest, ResourceCommonMessage.AlreadyExistingRecord);
                        _logger.LogError(exception, string.Join(",", sqlExceptionInnerException.Message));
                        break;
                    default:
                    {
                        apiErrorResponse =
                            new ApiErrorResponse((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.ErrorOccurred);
                        _logger.LogError(exception, string.Join(",", sqlExceptionInnerException.Message));
                    }
                        break;
                }
            }
            else
            {
                apiErrorResponse = new ApiErrorResponse((int)HttpStatusCode.InternalServerError, exception.Message);
            }

            return apiErrorResponse;
        }
    }
}
