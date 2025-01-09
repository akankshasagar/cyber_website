using System.Security.Cryptography;

namespace CyberSecurity_new.Helper
{
    public class PasswordHasher2
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iterations = 10000;

        public static (string passwordHash, string passwordSalt) HashPassword(string password)
        {
            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = key.GetBytes(HashSize);

            var saltBase64 = Convert.ToBase64String(salt);
            var hashBase64 = Convert.ToBase64String(hash);

            return (hashBase64, saltBase64);
        }

        public static bool VerifyPassword(string password, string passwordHash, string passwordSalt)
        {
            var salt = Convert.FromBase64String(passwordSalt);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = key.GetBytes(HashSize);

            var expectedHash = Convert.FromBase64String(passwordHash);

            for (var i = 0; i < HashSize; i++)
            {
                if (expectedHash[i] != hash[i])
                    return false;
            }
            return true;
        }

    }
}
