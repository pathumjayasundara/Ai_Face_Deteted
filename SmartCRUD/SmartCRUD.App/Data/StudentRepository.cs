using Microsoft.Data.SqlClient;
using SmartCRUD.App.Models;

namespace SmartCRUD.App.Data
{
    public class StudentRepository
    {
        public List<StudentModel> GetAll()
        {
            var list = new List<StudentModel>();
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                SELECT StudentID, FullName, Age, Gender, Phone, Email, Address, CreatedAt, UpdatedAt
                FROM   Students
                WHERE  IsActive = 1
                ORDER  BY FullName";

            using var cmd    = new SqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapRow(reader));

            return list;
        }

        public StudentModel? GetById(int studentId)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                SELECT StudentID, FullName, Age, Gender, Phone, Email, Address, CreatedAt, UpdatedAt
                FROM   Students
                WHERE  StudentID = @StudentID
                  AND  IsActive  = 1";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StudentID", studentId);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapRow(reader) : null;
        }

        public List<StudentModel> Search(string keyword)
        {
            var list = new List<StudentModel>();
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                SELECT StudentID, FullName, Age, Gender, Phone, Email, Address, CreatedAt, UpdatedAt
                FROM   Students
                WHERE  IsActive = 1
                  AND (FullName   LIKE @Kw
                    OR Phone      LIKE @Kw
                    OR Email      LIKE @Kw
                    OR CAST(StudentID AS NVARCHAR) LIKE @Kw)
                ORDER  BY FullName";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Kw", $"%{keyword}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapRow(reader));

            return list;
        }

        public int Insert(StudentModel s)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                INSERT INTO Students (FullName, Age, Gender, Phone, Email, Address)
                VALUES (@FullName, @Age, @Gender, @Phone, @Email, @Address);
                SELECT SCOPE_IDENTITY();";

            using var cmd = new SqlCommand(sql, conn);
            AddParameters(cmd, s);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Update(StudentModel s)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                UPDATE Students
                SET    FullName  = @FullName,
                       Age       = @Age,
                       Gender    = @Gender,
                       Phone     = @Phone,
                       Email     = @Email,
                       Address   = @Address,
                       UpdatedAt = SYSDATETIME()
                WHERE  StudentID = @StudentID
                  AND  IsActive  = 1";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StudentID", s.StudentID);
            AddParameters(cmd, s);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int studentId)
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = @"
                UPDATE Students
                SET    IsActive = 0, UpdatedAt = SYSDATETIME()
                WHERE  StudentID = @StudentID";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StudentID", studentId);
            return cmd.ExecuteNonQuery() > 0;
        }

        public int Count()
        {
            using var conn = DatabaseConnection.GetOpenConnection();
            const string sql = "SELECT COUNT(1) FROM Students WHERE IsActive = 1";
            using var cmd    = new SqlCommand(sql, conn);
            return (int)cmd.ExecuteScalar()!;
        }

        private static void AddParameters(SqlCommand cmd, StudentModel s)
        {
            cmd.Parameters.AddWithValue("@FullName", s.FullName);
            cmd.Parameters.AddWithValue("@Age",      s.Age);
            cmd.Parameters.AddWithValue("@Gender",   s.Gender);
            cmd.Parameters.AddWithValue("@Phone",    s.Phone);
            cmd.Parameters.AddWithValue("@Email",    s.Email);
            cmd.Parameters.AddWithValue("@Address",  s.Address);
        }

        private static StudentModel MapRow(SqlDataReader r) => new()
        {
            StudentID = r.GetInt32(0),
            FullName  = r.GetString(1),
            Age       = r.GetInt32(2),
            Gender    = r.GetString(3),
            Phone     = r.GetString(4),
            Email     = r.GetString(5),
            Address   = r.GetString(6),
            CreatedAt = r.GetDateTime(7),
            UpdatedAt = r.GetDateTime(8)
        };
    }
}
