using System.Security.Cryptography;
using System.Text;

namespace FaceAttendanceSystem.Services
{
    // Simple, dependency-free password hashing. Not bank-grade security,
    // but passwords are never stored or shown in plain text.
    public static class PasswordHelper
    {
        public static string Hash(string plainTextPassword)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainTextPassword);
            byte[] hashBytes = SHA256.HashData(bytes);
            return Convert.ToHexString(hashBytes);
        }

        public static bool Verify(string plainTextPassword, string storedHash)
        {
            return Hash(plainTextPassword).Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
