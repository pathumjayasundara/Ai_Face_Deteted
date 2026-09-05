using System.Xml.Linq;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Data
{
    public class LecturerRepository
    {
        public LecturerRepository()
        {
            if (!File.Exists(FilePaths.LecturersFilePath))
            {
                new XElement("lecturers").Save(FilePaths.LecturersFilePath);
            }
        }

        public List<Lecturer> LoadAll()
        {
            XElement root = XElement.Load(FilePaths.LecturersFilePath);
            return root.Elements("lecturer").Select(e => new Lecturer
            {
                LecturerId = (string?)e.Element("lecturerId") ?? string.Empty,
                FullName = (string?)e.Element("fullName") ?? string.Empty,
                Position = (string?)e.Element("position") ?? string.Empty,
                Email = (string?)e.Element("email") ?? string.Empty,
                Username = (string?)e.Element("username") ?? string.Empty,
                PasswordHash = (string?)e.Element("passwordHash") ?? string.Empty
            }).ToList();
        }

        public bool UsernameExists(string username)
        {
            return LoadAll().Any(l => l.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public Lecturer? FindByUsername(string username)
        {
            return LoadAll().FirstOrDefault(l => l.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Lecturer lecturer)
        {
            XElement root = XElement.Load(FilePaths.LecturersFilePath);
            root.Add(new XElement("lecturer",
                new XElement("lecturerId", lecturer.LecturerId),
                new XElement("fullName", lecturer.FullName),
                new XElement("position", lecturer.Position),
                new XElement("email", lecturer.Email),
                new XElement("username", lecturer.Username),
                new XElement("passwordHash", lecturer.PasswordHash)
            ));
            root.Save(FilePaths.LecturersFilePath);
        }

        public void UpdatePassword(string username, string newPasswordHash)
        {
            XElement root = XElement.Load(FilePaths.LecturersFilePath);
            XElement? existing = root.Elements("lecturer")
                .FirstOrDefault(e => (string?)e.Element("username") == username);

            if (existing != null)
            {
                existing.SetElementValue("passwordHash", newPasswordHash);
                root.Save(FilePaths.LecturersFilePath);
            }
        }

        // Used by the Admin panel to edit a lecturer's basic details (not their password).
        public void Update(Lecturer lecturer)
        {
            XElement root = XElement.Load(FilePaths.LecturersFilePath);
            XElement? existing = root.Elements("lecturer")
                .FirstOrDefault(e => (string?)e.Element("username") == lecturer.Username);

            if (existing != null)
            {
                existing.SetElementValue("fullName", lecturer.FullName);
                existing.SetElementValue("position", lecturer.Position);
                existing.SetElementValue("lecturerId", lecturer.LecturerId);
                existing.SetElementValue("email", lecturer.Email);
                root.Save(FilePaths.LecturersFilePath);
            }
        }

        public void Delete(string username)
        {
            XElement root = XElement.Load(FilePaths.LecturersFilePath);
            root.Elements("lecturer")
                .Where(e => (string?)e.Element("username") == username)
                .ToList()
                .ForEach(e => e.Remove());
            root.Save(FilePaths.LecturersFilePath);
        }
    }
}
