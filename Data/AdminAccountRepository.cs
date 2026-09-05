using System.Xml.Linq;
using FaceAttendanceSystem.Models;
using FaceAttendanceSystem.Services;

namespace FaceAttendanceSystem.Data
{
    public class AdminAccountRepository
    {
        public AdminAccountRepository()
        {
            if (!File.Exists(FilePaths.AdminAccountFilePath))
            {
                // First run: seed with the default admin password.
                string defaultHash = PasswordHelper.Hash(Lecturer.AdminDefaultPassword);
                new XElement("admin", new XElement("passwordHash", defaultHash))
                    .Save(FilePaths.AdminAccountFilePath);
            }
        }

        public string GetPasswordHash()
        {
            XElement root = XElement.Load(FilePaths.AdminAccountFilePath);
            return (string?)root.Element("passwordHash") ?? PasswordHelper.Hash(Lecturer.AdminDefaultPassword);
        }

        public void SetPasswordHash(string newHash)
        {
            var root = new XElement("admin", new XElement("passwordHash", newHash));
            root.Save(FilePaths.AdminAccountFilePath);
        }
    }
}
