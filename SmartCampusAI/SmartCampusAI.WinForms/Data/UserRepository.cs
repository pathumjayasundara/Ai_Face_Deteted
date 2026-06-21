using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartCampusAI.Models;

namespace SmartCampusAI.Data
{
    public class UserRepository
    {
        public List<User> GetAll()
        {
            List<User> list = new();
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT UserID,Username,Password,Role,CreatedAt FROM Users ORDER BY Username";
            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
                list.Add(MapRow(r));
            return list;
        }

        public User? GetByCredentials(string username, string password)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT UserID,Username,Password,Role,CreatedAt FROM Users WHERE Username=@u AND Password=@p";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            using SqlDataReader r = cmd.ExecuteReader();
            return r.Read() ? MapRow(r) : null;
        }

        public int Insert(User u)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"INSERT INTO Users (Username,Password,Role) VALUES (@u,@p,@r);
                           SELECT SCOPE_IDENTITY();";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@u", u.Username);
            cmd.Parameters.AddWithValue("@p", u.Password);
            cmd.Parameters.AddWithValue("@r", u.Role);
            object? result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public bool Update(User u)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "UPDATE Users SET Username=@u,Password=@p,Role=@r WHERE UserID=@id";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@id", u.UserID);
            cmd.Parameters.AddWithValue("@u",  u.Username);
            cmd.Parameters.AddWithValue("@p",  u.Password);
            cmd.Parameters.AddWithValue("@r",  u.Role);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "DELETE FROM Users WHERE UserID=@id";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UsernameExists(string username, int excludeId = 0)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT COUNT(*) FROM Users WHERE Username=@u AND UserID<>@id";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@u",  username);
            cmd.Parameters.AddWithValue("@id", excludeId);
            return (int)cmd.ExecuteScalar()! > 0;
        }

        private static User MapRow(SqlDataReader r) => new()
        {
            UserID    = r.GetInt32(0),
            Username  = r.GetString(1),
            Password  = r.GetString(2),
            Role      = r.GetString(3),
            CreatedAt = r.GetDateTime(4)
        };
    }
}
