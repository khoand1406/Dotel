using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace Dotel2.Utils
{
    public static class ValidateUtils
    {
        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static bool IsValidEmail(string email) =>
            Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public static bool IsValidPhone(string phone) =>
            Regex.IsMatch(phone, @"^\d{10}$");
    }
}
