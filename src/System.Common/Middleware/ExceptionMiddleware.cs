using System.Common.Exceptions;
using System.Common.Response;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Royalty.Insurance.Settings;

namespace System.Common.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;


        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invoke Exception Middleware
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAndSendProperMessage(context, exception);
            }
        }

        /// <summary>
        /// Handle Global error and throw proper Http exception with status code
        /// </summary>
        /// <param name="context">Http context</param>
        /// <param name="exception">Exception</param>
        /// <returns>error message</returns>
        private async Task HandleErrorAndSendProperMessage(HttpContext context, Exception exception)
        {
            ApiErrorResponse apiErrorResponse;
            switch (exception)
            {

                case DbUpdateException dbUpdateException:
                    apiErrorResponse = HandleDbUpdateException(exception, dbUpdateException);
                    break;
                case RestApiResponseException responseException:
                    apiErrorResponse = new ApiErrorResponse(responseException.ErrorCode, responseException.Message);
                    break;
                case PreconditionRequiredException preconditionRequiredException:
                    apiErrorResponse = new ApiErrorResponse((int)HttpStatusCode.PreconditionRequired, null, preconditionRequiredException.Data);
                    break;
                case FoundException foundException:
                    apiErrorResponse = new ApiErrorResponse((int)HttpStatusCode.Found, null, foundException.Data);
                    break;
                case ValidationException validationException:
                    apiErrorResponse = new ApiErrorResponse((int)HttpStatusCode.BadRequest, string.Join('\n', validationException.Errors.Select(x => $"{x.Key} {string.Join('\n', x.Value)}")));
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
            await CreateContextResponse(context, apiErrorResponse.Status, jsonResult);
            _logger.LogError(exception, string.Join(",", apiErrorResponse.Message));
        }

        private ApiErrorResponse HandleDbUpdateException(Exception exception, DbUpdateException sqlException)
        {
            _logger.LogError(exception, string.Join(",", sqlException.Message));
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

        /// <summary>
        /// Put message in Http context
        /// </summary>        
        /// <param name="context">http context</param>
        /// <param name="statusCode">http status code</param>
        /// <param name="errorMessageSerialized">error message serialized</param>
        /// <returns></returns>
        private static async Task CreateContextResponse(HttpContext context, int statusCode, string errorMessageSerialized)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = SystemConstants.MediaType;
            await context.Response.WriteAsync(errorMessageSerialized);
        }
    }

}
