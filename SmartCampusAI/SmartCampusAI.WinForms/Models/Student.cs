using System;

namespace SmartCampusAI.Models
{
    public class Student
    {
        public int    StudentID { get; set; }
        public string FullName  { get; set; } = string.Empty;
        public int    Age       { get; set; }
        public string Gender    { get; set; } = string.Empty;
        public string Phone     { get; set; } = string.Empty;
        public string Address   { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
