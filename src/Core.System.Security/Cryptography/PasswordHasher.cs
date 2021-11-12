using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.System.Security.Cryptography
{
    public static  class PasswordHasher
    {
        private const int Size = 64;

        /// <summary>
        /// Generate
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="iterations">iterations</param>
        /// <returns>Password Result</returns>
        public static PasswordResult Generate(string password, int iterations = 10000)
        {
            //generate a random salt for hashing
            PasswordResult result = new PasswordResult();
            result.Salting = new byte[Size];
            new RNGCryptoServiceProvider().GetBytes(result.Salting);
            //hash password given salt and iterations (default to 1000)
            //iterations provide difficulty when cracking
            var deriveBytes = new Rfc2898DeriveBytes(password, result.Salting, iterations, HashAlgorithmName.SHA512);
            result.PasswordHash = deriveBytes.GetBytes(Size);

            return result;
        }


        /// <summary>
        /// Generate
        /// </summary>
        /// <param name="password">password</param>
        /// <param name="salting">salting</param>
        /// <param name="iterations">iterations</param>
        /// <returns>Password Result</returns>
        public static string Generate(string password, byte[] salting, int iterations = 10000)
        {
            //hash password given salt and iterations (default to 1000)
            //iterations provide difficulty when cracking

            var deriveBytes = new Rfc2898DeriveBytes(password, salting, iterations, HashAlgorithmName.SHA512);
            return Encoding.Unicode.GetString(deriveBytes.GetBytes(Size));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalPasswordHash">original Password Hash</param>
        /// <param name="password">password</param>
        /// <param name="salting">salting</param>
        /// <param name="iterations">iterations</param>
        /// <returns>True if valid otherwise false</returns>
        public static  bool IsValid(byte[] originalPasswordHash, string password, byte[] salting, int iterations = 10000)
        {
            //generate hash from test password and original salt and iterations
            var pbkdf2 = new Rfc2898DeriveBytes(password, salting, iterations, HashAlgorithmName.SHA512);
            byte[] hashBytes = pbkdf2.GetBytes(Size);


            return hashBytes.SequenceEqual(originalPasswordHash);
        }
    }
}
