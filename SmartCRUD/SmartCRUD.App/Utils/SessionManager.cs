using SmartCRUD.App.Models;

namespace SmartCRUD.App.Utils
{
    public static class SessionManager
    {
        public static UserModel? CurrentUser { get; private set; }
        public static bool IsLoggedIn => CurrentUser != null;
        public static bool IsAdmin    => CurrentUser?.Role == "Admin";
        public static bool IsManager  => CurrentUser?.Role is "Admin" or "Manager";

        public static void Login(UserModel user)  => CurrentUser = user;
        public static void Logout()               => CurrentUser = null;
    }
}
