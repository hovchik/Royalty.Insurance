namespace System.Common.Exceptions
{
    public class PreconditionRequiredException : Exception
    {
        public PreconditionRequiredException(string data)
        {
            Data = data;
        }
        public string Data { get; set; }
    }
}
