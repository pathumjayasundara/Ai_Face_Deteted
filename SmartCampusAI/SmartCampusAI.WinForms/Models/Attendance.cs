using System;

namespace SmartCampusAI.Models
{
    public class Attendance
    {
        public int      AttendanceID    { get; set; }
        public int      StudentID       { get; set; }
        public string   StudentName     { get; set; } = string.Empty;
        public DateTime AttendanceDate  { get; set; }
        public TimeSpan AttendanceTime  { get; set; }
        public string   Status          { get; set; } = "Present";
        public string   MarkedBy        { get; set; } = "FaceRecognition";
        public double   ConfidenceScore { get; set; }
    }
}
