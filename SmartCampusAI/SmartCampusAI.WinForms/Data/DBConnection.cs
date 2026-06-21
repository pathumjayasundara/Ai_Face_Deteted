using System;
using Microsoft.Data.SqlClient;

namespace SmartCampusAI.Data
{
    /// <summary>
    /// Provides SQL Server connection management for SmartCampusAI.
    /// Change the connection string below to match your SQL Server instance.
    /// </summary>
    public static class DBConnection
    {
        // ─── EDIT THIS ────────────────────────────────────────────────────────
        // Windows Auth:  "Server=.\\SQLEXPRESS;Database=SmartCampusAI;Trusted_Connection=True;TrustServerCertificate=True;"
        // SQL Auth:      "Server=.\\SQLEXPRESS;Database=SmartCampusAI;User Id=sa;Password=YourPass;TrustServerCertificate=True;"
        private const string ConnectionString =
            "Server=.\\SQLEXPRESS;Database=SmartCampusAI;Trusted_Connection=True;TrustServerCertificate=True;";
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>Returns an open SqlConnection. Caller must dispose.</summary>
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new(ConnectionString);
            conn.Open();
            return conn;
        }

        /// <summary>Returns true if the database is reachable.</summary>
        public static bool TestConnection()
        {
            try
            {
                using SqlConnection conn = GetConnection();
                return conn.State == System.Data.ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }
    }
}
