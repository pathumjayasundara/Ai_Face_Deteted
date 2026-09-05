using System.Globalization;
using System.Xml.Linq;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Data
{
    public class StudentRepository
    {
        public StudentRepository()
        {
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(FilePaths.StudentsFilePath))
            {
                new XElement("students").Save(FilePaths.StudentsFilePath);
            }
        }

        public List<Student> LoadAll()
        {
            var students = new List<Student>();
            XElement root = XElement.Load(FilePaths.StudentsFilePath);

            foreach (XElement element in root.Elements("student"))
            {
                string encodingText = (string?)element.Element("faceEncoding") ?? string.Empty;
                double[] encoding = encodingText.Length == 0
                    ? Array.Empty<double>()
                    : encodingText
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(v => double.Parse(v, CultureInfo.InvariantCulture))
                        .ToArray();

                students.Add(new Student
                {
                    RegNumber = (string?)element.Element("regNumber") ?? string.Empty,
                    FullName = (string?)element.Element("fullName") ?? string.Empty,
                    ClassName = (string?)element.Element("className") ?? string.Empty,
                    Course = (string?)element.Element("course") ?? string.Empty,
                    Email = (string?)element.Element("email") ?? string.Empty,
                    PhotoPath = (string?)element.Element("photoPath") ?? string.Empty,
                    FaceEncoding = encoding
                });
            }

            return students;
        }

        public bool RegNumberExists(string regNumber)
        {
            return LoadAll().Any(s => s.RegNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase));
        }

        public void Add(Student student)
        {
            XElement root = XElement.Load(FilePaths.StudentsFilePath);

            string encodingText = string.Join(",",
                student.FaceEncoding.Select(v => v.ToString(CultureInfo.InvariantCulture)));

            root.Add(new XElement("student",
                new XElement("regNumber", student.RegNumber),
                new XElement("fullName", student.FullName),
                new XElement("className", student.ClassName),
                new XElement("course", student.Course),
                new XElement("email", student.Email),
                new XElement("photoPath", student.PhotoPath),
                new XElement("faceEncoding", encodingText)
            ));

            root.Save(FilePaths.StudentsFilePath);
        }

        // Used by the Admin panel to edit a student's basic details (not their face data).
        public void Update(Student student)
        {
            XElement root = XElement.Load(FilePaths.StudentsFilePath);
            XElement? existing = root.Elements("student")
                .FirstOrDefault(e => (string?)e.Element("regNumber") == student.RegNumber);

            if (existing != null)
            {
                existing.SetElementValue("fullName", student.FullName);
                existing.SetElementValue("course", student.Course);
                existing.SetElementValue("email", student.Email);
                root.Save(FilePaths.StudentsFilePath);
            }
        }

        public void Delete(string regNumber)
        {
            XElement root = XElement.Load(FilePaths.StudentsFilePath);
            root.Elements("student")
                .Where(e => (string?)e.Element("regNumber") == regNumber)
                .ToList()
                .ForEach(e => e.Remove());
            root.Save(FilePaths.StudentsFilePath);
        }
    }
}
