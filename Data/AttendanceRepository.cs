using System.Globalization;
using System.Xml.Linq;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Data
{
    public class AttendanceRepository
    {
        private const string DateFormat = "yyyy-MM-dd";
        private const string TimeFormat = "yyyy-MM-dd HH:mm:ss";

        public AttendanceRepository()
        {
            if (!File.Exists(FilePaths.AttendanceFilePath))
            {
                new XElement("attendanceRecords").Save(FilePaths.AttendanceFilePath);
            }
        }

        public List<AttendanceRecord> LoadAll()
        {
            var records = new List<AttendanceRecord>();
            XElement root = XElement.Load(FilePaths.AttendanceFilePath);

            foreach (XElement element in root.Elements("record"))
            {
                records.Add(new AttendanceRecord
                {
                    Id = (string?)element.Element("id") ?? Guid.NewGuid().ToString("N"),
                    RegNumber = (string?)element.Element("regNumber") ?? string.Empty,
                    FullName = (string?)element.Element("fullName") ?? string.Empty,
                    ClassName = (string?)element.Element("className") ?? string.Empty,
                    Course = (string?)element.Element("course") ?? string.Empty,
                    SubjectCode = (string?)element.Element("subjectCode") ?? string.Empty,
                    SubjectName = (string?)element.Element("subjectName") ?? string.Empty,
                    Date = DateTime.ParseExact((string?)element.Element("date") ?? DateTime.Today.ToString(DateFormat), DateFormat, CultureInfo.InvariantCulture),
                    TimeMarked = DateTime.ParseExact((string?)element.Element("timeMarked") ?? DateTime.Now.ToString(TimeFormat), TimeFormat, CultureInfo.InvariantCulture),
                    Status = (string?)element.Element("status") ?? "Present"
                });
            }

            return records;
        }

        // Prevents marking the same student present twice for the same
        // subject lecture on the same day.
        public bool AlreadyMarkedToday(string regNumber, string subjectCode)
        {
            DateTime today = DateTime.Today;
            return LoadAll().Any(r =>
                r.RegNumber.Equals(regNumber, StringComparison.OrdinalIgnoreCase) &&
                r.SubjectCode.Equals(subjectCode, StringComparison.OrdinalIgnoreCase) &&
                r.Date.Date == today);
        }

        public void Add(AttendanceRecord record)
        {
            XElement root = XElement.Load(FilePaths.AttendanceFilePath);

            root.Add(new XElement("record",
                new XElement("id", record.Id),
                new XElement("regNumber", record.RegNumber),
                new XElement("fullName", record.FullName),
                new XElement("className", record.ClassName),
                new XElement("course", record.Course),
                new XElement("subjectCode", record.SubjectCode),
                new XElement("subjectName", record.SubjectName),
                new XElement("date", record.Date.ToString(DateFormat, CultureInfo.InvariantCulture)),
                new XElement("timeMarked", record.TimeMarked.ToString(TimeFormat, CultureInfo.InvariantCulture)),
                new XElement("status", record.Status)
            ));

            root.Save(FilePaths.AttendanceFilePath);
        }

        // Used by the Admin panel to correct mistaken attendance marks.
        public void Delete(string id)
        {
            XElement root = XElement.Load(FilePaths.AttendanceFilePath);
            root.Elements("record")
                .Where(e => (string?)e.Element("id") == id)
                .ToList()
                .ForEach(e => e.Remove());
            root.Save(FilePaths.AttendanceFilePath);
        }

        public List<AttendanceRecord> GetByDate(DateTime date)
        {
            return LoadAll().Where(r => r.Date.Date == date.Date).ToList();
        }

        public List<AttendanceRecord> GetBySubject(string subjectCode)
        {
            return LoadAll()
                .Where(r => r.SubjectCode.Equals(subjectCode, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
