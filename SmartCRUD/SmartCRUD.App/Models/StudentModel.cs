namespace SmartCRUD.App.Models
{
    public class StudentModel
    {
        public int      StudentID { get; set; }
        public string   FullName  { get; set; } = string.Empty;
        public int      Age       { get; set; }
        public string   Gender    { get; set; } = string.Empty;
        public string   Phone     { get; set; } = string.Empty;
        public string   Email     { get; set; } = string.Empty;
        public string   Address   { get; set; } = string.Empty;
        public bool     IsActive  { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
