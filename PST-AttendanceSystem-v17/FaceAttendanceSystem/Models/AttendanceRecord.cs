namespace FaceAttendanceSystem.Models
{
    public class AttendanceRecord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string RegNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime TimeMarked { get; set; }
        public string Status { get; set; } = "Present";
    }
}
