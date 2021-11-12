
namespace Royalty.Insurance.Proxy.Response
{
    public class BaseResponse<T>
    {
        public T Data { get; }

        public BaseResponse(T data)
        {
            Data = data;
        }
    }
}
