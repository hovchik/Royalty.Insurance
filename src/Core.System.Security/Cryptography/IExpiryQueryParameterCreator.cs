namespace Core.System.Security.Cryptography
{
    public interface IExpiryQueryParameterCreator
    {
        bool IsValidRequest(long expiryTicks, string hash);
        string GetHashForExpiryTicks(long expiryTicks);
        string GetAvatarQueryParam(int id, long queryParamExpiry);
    }
}
