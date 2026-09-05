using System.Xml.Linq;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Data
{
    public class TimetableRepository
    {
        public TimetableRepository()
        {
            if (!File.Exists(FilePaths.TimetableFilePath))
            {
                new XElement("timetable").Save(FilePaths.TimetableFilePath);
            }
        }

        public List<TimetableEntry> LoadAll()
        {
            XElement root = XElement.Load(FilePaths.TimetableFilePath);
            return root.Elements("entry").Select(e => new TimetableEntry
            {
                Id = (string?)e.Element("id") ?? Guid.NewGuid().ToString("N"),
                Day = Enum.TryParse((string?)e.Element("day"), out DayOfWeek day) ? day : DayOfWeek.Monday,
                StartTime = (string?)e.Element("startTime") ?? "08:00",
                EndTime = (string?)e.Element("endTime") ?? "09:00",
                Room = (string?)e.Element("room") ?? string.Empty,
                SubjectCode = (string?)e.Element("subjectCode") ?? string.Empty,
                SubjectName = (string?)e.Element("subjectName") ?? string.Empty,
                Year = (int?)e.Element("year") ?? 1,
                Semester = (int?)e.Element("semester") ?? 1
            }).ToList();
        }

        public void Add(TimetableEntry entry)
        {
            XElement root = XElement.Load(FilePaths.TimetableFilePath);
            root.Add(new XElement("entry",
                new XElement("id", entry.Id),
                new XElement("day", entry.Day.ToString()),
                new XElement("startTime", entry.StartTime),
                new XElement("endTime", entry.EndTime),
                new XElement("room", entry.Room),
                new XElement("subjectCode", entry.SubjectCode),
                new XElement("subjectName", entry.SubjectName),
                new XElement("year", entry.Year),
                new XElement("semester", entry.Semester)
            ));
            root.Save(FilePaths.TimetableFilePath);
        }

        public void Delete(string id)
        {
            XElement root = XElement.Load(FilePaths.TimetableFilePath);
            root.Elements("entry")
                .Where(e => (string?)e.Element("id") == id)
                .ToList()
                .ForEach(e => e.Remove());
            root.Save(FilePaths.TimetableFilePath);
        }
    }
}
