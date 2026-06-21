using System.Text.RegularExpressions;

namespace SmartCRUD.App.Utils
{
    public static class Validator
    {
        public static bool IsNotEmpty(string? value)
            => !string.IsNullOrWhiteSpace(value);

        public static bool IsValidAge(string? value, out int age)
        {
            age = 0;
            return int.TryParse(value, out age) && age >= 1 && age <= 120;
        }

        public static bool IsValidPhone(string? phone)
            => !string.IsNullOrWhiteSpace(phone) &&
               Regex.IsMatch(phone.Trim(), @"^[\d\+\-\s\(\)]{7,20}$");

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return true; // email is optional
            return Regex.IsMatch(email.Trim(),
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        public static bool IsValidUsername(string? username)
            => !string.IsNullOrWhiteSpace(username) &&
               Regex.IsMatch(username.Trim(), @"^[a-zA-Z0-9_\.]{3,50}$");
    }
}
