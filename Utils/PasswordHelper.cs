using Microsoft.AspNetCore.Identity;

namespace DeliveryYaBackend.Utils
{
    public static class PasswordHelper
    {
        private static readonly PasswordHasher<object> _hasher = new PasswordHasher<object>();

        public static string Hash(string password)
        {
            return _hasher.HashPassword(null, password);
        }

        public static bool Verify(string hashedPassword, string plainPassword)
        {
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, plainPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
