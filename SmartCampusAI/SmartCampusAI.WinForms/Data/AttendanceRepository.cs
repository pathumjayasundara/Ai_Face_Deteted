using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SmartCampusAI.Models;

namespace SmartCampusAI.Data
{
    public class AttendanceRepository
    {
        // ── MARK ATTENDANCE (prevents duplicates via UNIQUE constraint) ─────────
        public bool MarkAttendance(int studentId, string status, double confidence, string markedBy)
        {
            try
            {
                using SqlConnection conn = DBConnection.GetConnection();
                string sql = @"IF NOT EXISTS (
                                    SELECT 1 FROM Attendance
                                    WHERE StudentID=@sid AND AttendanceDate=CAST(GETDATE() AS DATE))
                               INSERT INTO Attendance (StudentID,AttendanceDate,AttendanceTime,Status,MarkedBy,ConfidenceScore)
                               VALUES (@sid, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME),
                                       @status, @markedBy, @confidence)";
                using SqlCommand cmd = new(sql, conn);
                cmd.Parameters.AddWithValue("@sid",        studentId);
                cmd.Parameters.AddWithValue("@status",     status);
                cmd.Parameters.AddWithValue("@markedBy",   markedBy);
                cmd.Parameters.AddWithValue("@confidence", confidence);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false; // duplicate attendance for same day is silently ignored
            }
        }

        // ── CHECK IF ALREADY MARKED TODAY ──────────────────────────────────────
        public bool IsMarkedToday(int studentId)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"SELECT COUNT(*) FROM Attendance
                           WHERE StudentID=@sid AND AttendanceDate=CAST(GETDATE() AS DATE)";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@sid", studentId);
            return (int)cmd.ExecuteScalar()! > 0;
        }

        // ── GET TODAY'S ATTENDANCE ─────────────────────────────────────────────
        public List<Attendance> GetToday()
        {
            List<Attendance> list = new();
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"SELECT a.AttendanceID, a.StudentID, s.FullName,
                                  a.AttendanceDate, a.AttendanceTime,
                                  a.Status, a.MarkedBy, ISNULL(a.ConfidenceScore,0)
                           FROM Attendance a
                           INNER JOIN Students s ON a.StudentID=s.StudentID
                           WHERE a.AttendanceDate=CAST(GETDATE() AS DATE)
                           ORDER BY a.AttendanceTime DESC";
            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
                list.Add(MapRow(r));
            return list;
        }

        // ── GET BY DATE RANGE ──────────────────────────────────────────────────
        public List<Attendance> GetByDateRange(DateTime start, DateTime end)
        {
            List<Attendance> list = new();
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"SELECT a.AttendanceID, a.StudentID, s.FullName,
                                  a.AttendanceDate, a.AttendanceTime,
                                  a.Status, a.MarkedBy, ISNULL(a.ConfidenceScore,0)
                           FROM Attendance a
                           INNER JOIN Students s ON a.StudentID=s.StudentID
                           WHERE a.AttendanceDate BETWEEN @start AND @end
                           ORDER BY a.AttendanceDate DESC, a.AttendanceTime DESC";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@start", start.Date);
            cmd.Parameters.AddWithValue("@end",   end.Date);
            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
                list.Add(MapRow(r));
            return list;
        }

        // ── GET BY STUDENT ─────────────────────────────────────────────────────
        public List<Attendance> GetByStudent(int studentId)
        {
            List<Attendance> list = new();
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"SELECT a.AttendanceID, a.StudentID, s.FullName,
                                  a.AttendanceDate, a.AttendanceTime,
                                  a.Status, a.MarkedBy, ISNULL(a.ConfidenceScore,0)
                           FROM Attendance a
                           INNER JOIN Students s ON a.StudentID=s.StudentID
                           WHERE a.StudentID=@sid
                           ORDER BY a.AttendanceDate DESC";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@sid", studentId);
            using SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
                list.Add(MapRow(r));
            return list;
        }

        // ── MANUAL MARK ───────────────────────────────────────────────────────
        public bool ManualMark(int studentId, DateTime date, string status)
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = @"IF EXISTS (SELECT 1 FROM Attendance WHERE StudentID=@sid AND AttendanceDate=@date)
                               UPDATE Attendance SET Status=@status WHERE StudentID=@sid AND AttendanceDate=@date
                           ELSE
                               INSERT INTO Attendance (StudentID,AttendanceDate,AttendanceTime,Status,MarkedBy,ConfidenceScore)
                               VALUES (@sid,@date,CAST(GETDATE() AS TIME),@status,'Manual',0)";
            using SqlCommand cmd = new(sql, conn);
            cmd.Parameters.AddWithValue("@sid",    studentId);
            cmd.Parameters.AddWithValue("@date",   date.Date);
            cmd.Parameters.AddWithValue("@status", status);
            return cmd.ExecuteNonQuery() > 0;
        }

        // ── TODAY COUNT ───────────────────────────────────────────────────────
        public int TodayCount()
        {
            using SqlConnection conn = DBConnection.GetConnection();
            string sql = "SELECT COUNT(*) FROM Attendance WHERE AttendanceDate=CAST(GETDATE() AS DATE)";
            using SqlCommand cmd = new(sql, conn);
            return (int)cmd.ExecuteScalar()!;
        }

        private static Attendance MapRow(SqlDataReader r) => new()
        {
            AttendanceID    = r.GetInt32(0),
            StudentID       = r.GetInt32(1),
            StudentName     = r.GetString(2),
            AttendanceDate  = r.GetDateTime(3),
            AttendanceTime  = r.GetTimeSpan(4),
            Status          = r.GetString(5),
            MarkedBy        = r.GetString(6),
            ConfidenceScore = r.GetDouble(7)
        };
    }
}
