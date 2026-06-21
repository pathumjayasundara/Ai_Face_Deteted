namespace SmartCRUD.App.Models
{
    public class UserModel
    {
        public int      UserID    { get; set; }
        public string   Username  { get; set; } = string.Empty;
        public string   Password  { get; set; } = string.Empty;
        public string   Role      { get; set; } = "User";
        public bool     IsActive  { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
