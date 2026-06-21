using SmartCampusAI.Models;

namespace SmartCampusAI.Utils
{
    /// <summary>Holds the currently logged-in user for the application session.</summary>
    public static class SessionManager
    {
        public static User? CurrentUser { get; private set; }
        public static bool IsLoggedIn   => CurrentUser != null;
        public static bool IsAdmin      => CurrentUser?.Role == "Admin";

        public static void Login(User user)  => CurrentUser = user;
        public static void Logout()          => CurrentUser = null;
    }
}
