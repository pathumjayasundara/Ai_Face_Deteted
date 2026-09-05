using System.Net;
using System.Net.Mail;
using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Services
{
    // Sends confirmation emails (registration success, attendance marked).
    // If no SMTP account has been configured yet (see email-settings.xml),
    // sending is skipped quietly instead of crashing the app - emails are a
    // nice-to-have, not something that should ever block attendance marking.
    public static class EmailService
    {
        private const string FacultyDescription =
            "The Department of Physical Science and Technology, Faculty of Applied Sciences, " +
            "Sabaragamuwa University of Sri Lanka, offers a multidisciplinary degree programme " +
            "combining Physics, Chemistry, Mathematics and Computer Science, preparing graduates " +
            "for careers in scientific research, industry and technology.";

        public static async Task<bool> SendAsync(string toAddress, string subject, string bodyHtml)
        {
            if (string.IsNullOrWhiteSpace(toAddress))
            {
                Log("Skipped: no recipient email address was provided.");
                return false;
            }

            EmailSettings settings = new EmailSettingsRepository().Load();
            if (!settings.IsConfigured)
            {
                Log("Skipped: email is not configured yet. Fill in 'XML Local Data Files\\email-settings.xml' with a real sender email + app password.");
                return false;
            }

            try
            {
                using var client = new SmtpClient(settings.SmtpHost, settings.SmtpPort)
                {
                    EnableSsl = settings.UseSsl,
                    Credentials = new NetworkCredential(settings.SenderEmail, settings.SenderPassword)
                };

                using var message = new MailMessage
                {
                    From = new MailAddress(settings.SenderEmail, settings.SenderDisplayName),
                    Subject = subject,
                    Body = bodyHtml,
                    IsBodyHtml = true
                };
                message.To.Add(toAddress);

                await client.SendMailAsync(message);
                Log($"Sent successfully to {toAddress} (subject: {subject}).");
                return true;
            }
            catch (Exception ex)
            {
                // Best-effort only: a failed/misconfigured email must never
                // interrupt registration or attendance marking.
                Log($"FAILED sending to {toAddress}: {ex.Message}");
                return false;
            }
        }

        private static void Log(string message)
        {
            try
            {
                string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
                File.AppendAllText(FilePaths.EmailLogFilePath, line);
            }
            catch
            {
                // Logging must never itself crash the app.
            }
        }

        public static Task<bool> SendLecturerRegistrationEmailAsync(Lecturer lecturer)
        {
            string body = $@"
                <div style='font-family:Segoe UI, Arial, sans-serif; color:#333;'>
                    <h2 style='color:#8D1538;'>Successfully Registered! Thank You.</h2>
                    <p>Dear {lecturer.FullName},</p>
                    <p>Your lecturer account for the <b>PST Attendance System</b> has been created successfully.</p>
                    <p><b>Username:</b> {lecturer.Username}</p>
                    <hr/>
                    <p style='font-size:13px; color:#555;'>{FacultyDescription}</p>
                </div>";

            return SendAsync(lecturer.Email, "Attendance System", body);
        }

        public static Task<bool> SendStudentAttendanceEmailAsync(Student student, string subjectName)
        {
            string body = $@"
                <div style='font-family:Segoe UI, Arial, sans-serif; color:#333;'>
                    <h2 style='color:#8D1538;'>Your Successful Attend This Lecture</h2>
                    <p>Dear {student.FullName},</p>
                    <p>Your attendance has been recorded for: <b>{subjectName}</b></p>
                    <p>Marked at: {DateTime.Now:dddd, dd MMMM yyyy - hh:mm tt}</p>
                    <hr/>
                    <p style='font-size:13px; color:#555;'>{FacultyDescription}</p>
                </div>";

            return SendAsync(student.Email, "Attendance System", body);
        }
    }
}
