
using System.Net;

namespace System.Common.Exceptions
{
    public class RestApiResponseException : Exception
    {
        public RestApiResponseException(string message) : this((int) HttpStatusCode.BadRequest, message)
        {
            
        }

        public RestApiResponseException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }
}
