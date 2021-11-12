namespace System.Common.Authentication.TwoFactor
{
    public interface ITokenManager
    {
        string GenerateToken(string username);
    }
}
