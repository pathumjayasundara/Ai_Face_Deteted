using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartCampusAI.Models;
using Newtonsoft.Json;

namespace SmartCampusAI.Data
{
    public class FaceRepository
    {
        // ── INSERT FACE ENCODING ───────────────────────────────────────────────
        public bool InsertFaceEncoding(int studentId, float[] encoding, string imagePath)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"INSERT INTO FaceData (StudentID,FaceEncoding,SampleImagePath)
                           VALUES (@sid,@enc,@path)";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@sid",  studentId);
            cmd.Parameters.AddWithValue("@enc",  JsonConvert.SerializeObject(encoding));
            cmd.Parameters.AddWithValue("@path", imagePath);
            return cmd.ExecuteNonQuery() > 0;
        }

        // ── GET ALL FACE DATA FOR RECOGNITION ─────────────────────────────────
        public List<FaceData> GetAllFaceData()
        {
            List<FaceData> list = new();
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"SELECT f.FaceID, f.StudentID, s.FullName,
                                  f.FaceEncoding, f.SampleImagePath, f.CapturedAt
                           FROM FaceData f
                           INNER JOIN Students s ON f.StudentID=s.StudentID
                           ORDER BY f.StudentID, f.CapturedAt";
            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                FaceData fd = MapRow(r);
                try
                {
                    fd.EncodingArray = JsonConvert.DeserializeObject<float[]>(fd.FaceEncoding);
                }
                catch
                {
                    fd.EncodingArray = Array.Empty<float>();
                }
                list.Add(fd);
            }
            return list;
        }

        // ── GET BY STUDENT ─────────────────────────────────────────────────────
        public List<FaceData> GetByStudent(int studentId)
        {
            List<FaceData> list = new();
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"SELECT f.FaceID, f.StudentID, s.FullName,
                                  f.FaceEncoding, f.SampleImagePath, f.CapturedAt
                           FROM FaceData f
                           INNER JOIN Students s ON f.StudentID=s.StudentID
                           WHERE f.StudentID=@sid";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@sid", studentId);
            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                FaceData fd = MapRow(r);
                try { fd.EncodingArray = JsonConvert.DeserializeObject<float[]>(fd.FaceEncoding); }
                catch { fd.EncodingArray = Array.Empty<float>(); }
                list.Add(fd);
            }
            return list;
        }

        // ── COUNT SAMPLES PER STUDENT ──────────────────────────────────────────
        public int CountSamples(int studentId)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT COUNT(*) FROM FaceData WHERE StudentID=@sid";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@sid", studentId);
            return (int)cmd.ExecuteScalar()!;
        }

        // ── DELETE FACE DATA ───────────────────────────────────────────────────
        public bool DeleteByStudent(int studentId)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "DELETE FROM FaceData WHERE StudentID=@sid";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@sid", studentId);
            return cmd.ExecuteNonQuery() > 0;
        }

        // ── HAS FACE DATA ──────────────────────────────────────────────────────
        public bool HasFaceData(int studentId)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT COUNT(*) FROM FaceData WHERE StudentID=@sid";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@sid", studentId);
            return (int)cmd.ExecuteScalar()! > 0;
        }

        private static FaceData MapRow(SqlDataReader r) => new()
        {
            FaceID          = r.GetInt32(0),
            StudentID       = r.GetInt32(1),
            StudentName     = r.GetString(2),
            FaceEncoding    = r.GetString(3),
            SampleImagePath = r.GetString(4),
            CapturedAt      = r.GetDateTime(5)
        };
    }
}
