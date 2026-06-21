using Microsoft.Data.SqlClient;
using SmartCRUD.App.Models;

namespace SmartCRUD.App.Data
{
    public class UserRepository
    {
        public UserModel? GetByCredentials(string username, string password)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                SELECT UserID, Username, Password, Role, IsActive, CreatedAt
                FROM   Users
                WHERE  Username = @Username
                  AND  Password = @Password
                  AND  IsActive = 1";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Password", password);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapRow(reader);

            return null;
        }

        public List<UserModel> GetAll()
        {
            var list = new List<UserModel>();
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                SELECT UserID, Username, Password, Role, IsActive, CreatedAt
                FROM   Users
                ORDER  BY Username";

            using var cmd    = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapRow(reader));

            return list;
        }

        public int Insert(UserModel user)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                INSERT INTO Users (Username, Password, Role)
                VALUES (@Username, @Password, @Role);
                SELECT SCOPE_IDENTITY();";

            using var cmd = new SqlCommand(sql, conn);
            AddParameters(cmd, user);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Update(UserModel user)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                UPDATE Users
                SET    Username = @Username,
                       Password = @Password,
                       Role     = @Role
                WHERE  UserID   = @UserID";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", user.UserID);
            AddParameters(cmd, user);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int userId)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = "UPDATE Users SET IsActive = 0 WHERE UserID = @UserID";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserID", userId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UsernameExists(string username, int excludeUserId = 0)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                SELECT COUNT(1) FROM Users
                WHERE  Username = @Username
                  AND  UserID  != @ExcludeID
                  AND  IsActive = 1";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Username",  username);
            cmd.Parameters.AddWithValue("@ExcludeID", excludeUserId);
            return (int)cmd.ExecuteScalar()! > 0;
        }

        private static void AddParameters(SqlCommand cmd, UserModel u)
        {
            cmd.Parameters.AddWithValue("@Username", u.Username);
            cmd.Parameters.AddWithValue("@Password", u.Password);
            cmd.Parameters.AddWithValue("@Role",     u.Role);
        }

        private static UserModel MapRow(SqlDataReader r) => new()
        {
            UserID    = r.GetInt32(0),
            Username  = r.GetString(1),
            Password  = r.GetString(2),
            Role      = r.GetString(3),
            IsActive  = r.GetBoolean(4),
            CreatedAt = r.GetDateTime(5)
        };
    }
}
