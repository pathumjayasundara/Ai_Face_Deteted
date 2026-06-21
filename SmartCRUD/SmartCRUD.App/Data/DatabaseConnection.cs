using Microsoft.Data.SqlClient;

namespace SmartCRUD.App.Data
{
    /// <summary>
    /// Central SQL Server connection factory.
    /// Edit the ConnectionString constant to match your SQL Server instance.
    /// </summary>
    public static class DatabaseConnection
    {
        // ── EDIT THIS ────────────────────────────────────────────────────────
        // Windows Authentication (most common for local dev):
        //   "Server=.\SQLEXPRESS;Database=SmartCRUDDB;Trusted_Connection=True;TrustServerCertificate=True;"
        // SQL Server Authentication:
        //   "Server=.\SQLEXPRESS;Database=SmartCRUDDB;User Id=sa;Password=YourPass;TrustServerCertificate=True;"
        // ─────────────────────────────────────────────────────────────────────
        private const string ConnectionString =
    "Server=(localdb)\\MSSQLLocalDB;Database=SmartCRUDDB;Trusted_Connection=True;TrustServerCertificate=True;";

        /// <summary>Returns an open SqlConnection. Caller is responsible for disposing.</summary>
        public static SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        /// <summary>Async version. Caller is responsible for disposing.</summary>
        public static async Task<SqlConnection> GetOpenConnectionAsync()
        {
            var conn = new SqlConnection(ConnectionString);
            await conn.OpenAsync();
            return conn;
        }

        /// <summary>Quick connectivity test. Returns true if the DB is reachable.</summary>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var conn = GetOpenConnection();
                return conn.State == System.Data.ConnectionState.Open;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
