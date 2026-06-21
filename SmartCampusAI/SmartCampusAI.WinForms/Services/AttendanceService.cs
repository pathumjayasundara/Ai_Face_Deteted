using System;
using SmartCampusAI.Data;
using SmartCampusAI.Models;

namespace SmartCampusAI.Services
{
    public class AttendanceService
    {
        private readonly AttendanceRepository _repo = new();
        private readonly StudentRepository _studentRepo = new();

        public (bool success, string message) MarkByFace(int studentId, double confidence)
        {
            Student? student = _studentRepo.GetById(studentId);
            if (student == null)
                return (false, "Student not found.");

            if (_repo.IsMarkedToday(studentId))
                return (false, $"{student.FullName} already marked present today.");

            bool result = _repo.MarkAttendance(studentId, "Present", confidence, "FaceRecognition");
            return result
                ? (true,  $"✅ Attendance marked for {student.FullName} ({confidence:F1}% confidence)")
                : (false, "Failed to mark attendance.");
        }

        public (bool success, string message) ManualMark(int studentId, DateTime date, string status)
        {
            Student? student = _studentRepo.GetById(studentId);
            if (student == null)
                return (false, "Student not found.");

            bool result = _repo.ManualMark(studentId, date, status);
            return result
                ? (true,  $"Attendance updated for {student.FullName}.")
                : (false, "Failed to update attendance.");
        }
    }
}
