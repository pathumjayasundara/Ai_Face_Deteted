using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SmartCampusAI.Models;

namespace SmartCampusAI.Data
{
    public class StudentRepository
    {
        // ── GET ALL ────────────────────────────────────────────────────────────
        public List<Student> GetAll()
        {
            List<Student> list = new();
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT StudentID,FullName,Age,Gender,Phone,Address,CreatedAt FROM Students ORDER BY FullName";
            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapRow(reader));
            return list;
        }

        // ── GET BY ID ──────────────────────────────────────────────────────────
        public Student? GetById(int id)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT StudentID,FullName,Age,Gender,Phone,Address,CreatedAt FROM Students WHERE StudentID=@id";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            return reader.Read() ? MapRow(reader) : null;
        }

        // ── SEARCH ─────────────────────────────────────────────────────────────
        public List<Student> Search(string keyword)
        {
            List<Student> list = new();
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"SELECT StudentID,FullName,Age,Gender,Phone,Address,CreatedAt
                           FROM Students
                           WHERE FullName LIKE @kw
                              OR CAST(StudentID AS NVARCHAR) LIKE @kw
                           ORDER BY FullName";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(MapRow(reader));
            return list;
        }

        // ── INSERT ─────────────────────────────────────────────────────────────
        public int Insert(Student s)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"INSERT INTO Students (FullName,Age,Gender,Phone,Address)
                           VALUES (@name,@age,@gender,@phone,@addr);
                           SELECT SCOPE_IDENTITY();";
            using SqlCommand cmd = new(sql, conn);
            AddParams(cmd, s);
            object? result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // ── UPDATE ─────────────────────────────────────────────────────────────
        public bool Update(Student s)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"UPDATE Students
                           SET FullName=@name,Age=@age,Gender=@gender,Phone=@phone,Address=@addr
                           WHERE StudentID=@id";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@id", s.StudentID);
            AddParams(cmd, s);
            return cmd.ExecuteNonQuery() > 0;
        }

        // ── DELETE ─────────────────────────────────────────────────────────────
        public bool Delete(int id)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "DELETE FROM Students WHERE StudentID=@id";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        // ── COUNT ──────────────────────────────────────────────────────────────
        public int Count()
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT COUNT(*) FROM Students";
            using SqlCommand cmd = new(sql, conn);
            return (int)cmd.ExecuteScalar()!;
        }

        // ── HELPERS ────────────────────────────────────────────────────────────
        private static Student MapRow(SqlDataReader r) => new()
        {
            StudentID = r.GetInt32(0),
            FullName  = r.GetString(1),
            Age       = r.GetInt32(2),
            Gender    = r.GetString(3),
            Phone     = r.GetString(4),
            Address   = r.GetString(5),
            CreatedAt = r.GetDateTime(6)
        };

        private static void AddParams(SqlCommand cmd, Student s)
        {
            cmd.Parameters.AddWithValue("@name",   s.FullName);
            cmd.Parameters.AddWithValue("@age",    s.Age);
            cmd.Parameters.AddWithValue("@gender", s.Gender);
            cmd.Parameters.AddWithValue("@phone",  s.Phone);
            cmd.Parameters.AddWithValue("@addr",   s.Address);
        }
    }
}
