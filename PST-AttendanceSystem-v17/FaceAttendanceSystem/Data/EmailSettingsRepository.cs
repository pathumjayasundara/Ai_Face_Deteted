using System.Xml.Linq;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Data
{
    public class EmailSettingsRepository
    {
        public EmailSettingsRepository()
        {
            if (!File.Exists(FilePaths.EmailSettingsFilePath))
            {
                Save(new EmailSettings());
            }
        }

        public EmailSettings Load()
        {
            XElement root = XElement.Load(FilePaths.EmailSettingsFilePath);
            return new EmailSettings
            {
                SmtpHost = (string?)root.Element("smtpHost") ?? "smtp.gmail.com",
                SmtpPort = (int?)root.Element("smtpPort") ?? 587,
                UseSsl = (bool?)root.Element("useSsl") ?? true,
                SenderEmail = (string?)root.Element("senderEmail") ?? string.Empty,
                SenderPassword = (string?)root.Element("senderPassword") ?? string.Empty,
                SenderDisplayName = (string?)root.Element("senderDisplayName") ?? "PST Attendance System"
            };
        }

        public void Save(EmailSettings settings)
        {
            var root = new XElement("emailSettings",
                new XElement("smtpHost", settings.SmtpHost),
                new XElement("smtpPort", settings.SmtpPort),
                new XElement("useSsl", settings.UseSsl),
                new XElement("senderEmail", settings.SenderEmail),
                new XElement("senderPassword", settings.SenderPassword),
                new XElement("senderDisplayName", settings.SenderDisplayName)
            );
            root.Save(FilePaths.EmailSettingsFilePath);
        }
    }
}
