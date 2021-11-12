namespace System.Common.Exceptions
{
    public class FoundException : Exception
    {
        public FoundException(string data)
        {
            Data = data;
        }
        public string Data { get; set; }
    }
}
