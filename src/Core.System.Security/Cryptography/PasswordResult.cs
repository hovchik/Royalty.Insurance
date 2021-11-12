
namespace Core.System.Security.Cryptography
{
    public class PasswordResult
    {
        public byte[] PasswordHash { get; set; }

        public byte[] Salting { get; set; }
    }
}
