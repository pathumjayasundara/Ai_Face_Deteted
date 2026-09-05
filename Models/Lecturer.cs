namespace FaceAttendanceSystem.Models
{
    public class Lecturer
    {
        public string LecturerId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // True only for the single built-in Administrator account. Admins can
        // view/edit/delete lecturers, students and attendance records - regular
        // lecturers can only add students and take attendance.
        public bool IsAdmin { get; set; }

        public const string AdminUsername = "Admin-suslpst";
        public const string AdminDefaultPassword = "PST123";

        public static Lecturer CreateAdminAccount()
        {
            return new Lecturer
            {
                LecturerId = "ADMIN",
                FullName = "Administrator",
                Position = "System Administrator",
                Username = AdminUsername,
                IsAdmin = true
            };
        }
    }
}
