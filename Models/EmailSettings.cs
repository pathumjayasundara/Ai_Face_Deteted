namespace FaceAttendanceSystem.Models
{
    // SMTP settings used to send confirmation emails. Fill these in with your own
    // mail account details in "XML Local Data Files\email-settings.xml" - see the
    // README for step-by-step instructions (Gmail App Password etc.).
    public class EmailSettings
    {
        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
        public bool UseSsl { get; set; } = true;
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderPassword { get; set; } = string.Empty;
        public string SenderDisplayName { get; set; } = "PST Attendance System";

        public bool IsConfigured =>
            !string.IsNullOrWhiteSpace(SenderEmail) && !string.IsNullOrWhiteSpace(SenderPassword);
    }
}
