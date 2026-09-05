using System.Text.RegularExpressions;

namespace FaceAttendanceSystem.Services
{
    public static class ValidationHelper
    {
        // Student registration numbers must look like 21APP1234, 22APP5678, etc.
        // - exactly 2 digits (enrollment year), then "APP", then one or more digits.
        private static readonly Regex StudentIdPattern = new(@"^\d{2}APP\d+$", RegexOptions.IgnoreCase);

        // Student university email addresses must end in @ms.sab.ac.lk (not a personal Gmail/Yahoo/etc.)
        private static readonly Regex StudentEmailPattern = new(@"^[^@\s]+@ms\.sab\.ac\.lk$", RegexOptions.IgnoreCase);

        // Lecturer official email addresses must end in @appsc.sab.ac.lk
        private static readonly Regex LecturerEmailPattern = new(@"^[^@\s]+@appsc\.sab\.ac\.lk$", RegexOptions.IgnoreCase);

        public const string StudentIdExample = "e.g. 21APP1234, 22APP5678, 23APP1122, 24APP3344";
        public const string StudentEmailExample = "e.g. 22app6134@ms.sab.ac.lk";
        public const string LecturerEmailExample = "e.g. kamal@appsc.sab.ac.lk";

        public static bool IsValidStudentId(string regNumber) => StudentIdPattern.IsMatch(regNumber.Trim());

        public static bool IsValidStudentEmail(string email) => StudentEmailPattern.IsMatch(email.Trim());

        public static bool IsValidLecturerEmail(string email) => LecturerEmailPattern.IsMatch(email.Trim());
    }
}
