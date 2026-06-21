using SmartCRUD.App.Data;
using SmartCRUD.App.Forms;

namespace SmartCRUD.App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Optional: verify DB connection before showing UI
            if (!DatabaseConnection.TestConnection(out string err))
            {
                MessageBox.Show(
                    $"Cannot connect to SQL Server.\n\n{err}\n\n" +
                    "Please update the connection string in:\n" +
                    "Data\\DatabaseConnection.cs",
                    "Database Connection Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                // Allow app to open anyway so user can see the form
            }

            Application.Run(new LoginForm());
        }
    }
}
