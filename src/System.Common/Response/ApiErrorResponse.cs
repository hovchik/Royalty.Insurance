namespace System.Common.Response
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int status, string message, string data = null)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public int Status{ get;}

        public string Message { get; }
        public string Data { get; set; }
    }
}
