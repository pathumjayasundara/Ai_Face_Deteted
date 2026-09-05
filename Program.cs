namespace FaceAttendanceSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            while (true)
            {
                Models.Lecturer? loggedInLecturer;
                using (var loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() != DialogResult.OK || loginForm.LoggedInLecturer == null)
                    {
                        break; // user closed the login window without logging in
                    }
                    loggedInLecturer = loginForm.LoggedInLecturer;
                }

                using var dashboard = new MainDashboard(loggedInLecturer);
                Application.Run(dashboard);

                if (!dashboard.LoggedOut)
                {
                    break; // window was closed normally (not via Logout) - exit the app
                }
                // else: loop back around and show the login screen again
            }
        }
    }
}
