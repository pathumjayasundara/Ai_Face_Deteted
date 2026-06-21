using System.Text.RegularExpressions;

namespace SmartCampusAI.Utils
{
    public static class Validators
    {
        public static bool IsNotEmpty(string? value)
            => !string.IsNullOrWhiteSpace(value);

        public static bool IsValidAge(string value, out int age)
            => int.TryParse(value, out age) && age > 0 && age < 150;

        public static bool IsValidPhone(string phone)
            => Regex.IsMatch(phone.Trim(), @"^[0-9\+\-\s\(\)]{7,20}$");

        public static bool IsValidUsername(string username)
            => Regex.IsMatch(username.Trim(), @"^[a-zA-Z0-9_\.]{3,50}$");
    }
}
