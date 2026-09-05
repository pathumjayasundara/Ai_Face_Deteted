using System.Text.RegularExpressions;
using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Services
{
    // A local, rule-based assistant (no external AI service/API key involved -
    // everything it knows comes from this app's own student and attendance
    // records). It understands a range of natural phrasings for the same
    // intent (looking up a student, listing everyone, checking a subject's
    // attendance) so it feels more conversational than a plain search box.
    public static class ChatbotService
    {
        public static string GetResponse(string userMessage)
        {
            string message = userMessage.Trim().TrimEnd('?', '!', '.');
            if (message.Length == 0)
            {
                return "Please type a student's index number or name, or ask me a question. 🙂";
            }

            string lower = message.ToLowerInvariant();

            if (IsGreeting(lower))
            {
                return "Hello! 👋 I'm the PST Attendance Assistant.\n\n" +
                       "You can ask me things like:\n" +
                       "• \"22APP1234\" or a student's name\n" +
                       "• \"list all students\"\n" +
                       "• \"how many students attended PST 21202 today\"\n" +
                       "• \"has Kamal attended today\"";
            }

            if (lower.Contains("thank"))
            {
                return "You're welcome! 😊 Let me know if you need anything else.";
            }

            if (IsHelpRequest(lower))
            {
                return "Here's what I can help with:\n\n" +
                       "👤 Student lookup - type an index number or name (even a partial name)\n" +
                       "📋 \"list all students\" - shows every registered student\n" +
                       "📚 \"attendance for PST 21202\" - shows how many students have attended a subject\n" +
                       "✅ \"has [name] attended today\" - quick yes/no with details";
            }

            if (IsListAllStudentsRequest(lower))
            {
                return BuildAllStudentsAnswer();
            }

            string? subjectCode = TryExtractSubjectCode(message);
            if (subjectCode != null && LooksLikeSubjectQuestion(lower))
            {
                return BuildSubjectAnswer(subjectCode);
            }

            List<Student> allStudents = new StudentRepository().LoadAll();
            List<Student> matches = FindMatchingStudents(message, allStudents);

            if (matches.Count == 0)
            {
                return $"I couldn't find any registered student matching \"{message}\". " +
                       "Please check the spelling, or try their index number instead. " +
                       "You can also type \"list all students\" to see everyone registered.";
            }

            if (matches.Count > 1)
            {
                string names = string.Join("\n", matches.Take(8).Select(s => $"• {s.RegNumber} - {s.FullName}"));
                string more = matches.Count > 8 ? $"\n...and {matches.Count - 8} more." : string.Empty;
                return $"I found {matches.Count} students matching \"{message}\":\n{names}{more}\n\n" +
                       "Please type their exact index number to see their details.";
            }

            return BuildStudentAnswer(matches[0]);
        }

        private static bool IsGreeting(string lower)
        {
            string[] greetings = { "hi", "hello", "hey", "ayubowan", "howdy" };
            return greetings.Any(g => lower == g || lower.StartsWith(g + " ") || lower.StartsWith(g + "!"));
        }

        private static bool IsHelpRequest(string lower)
        {
            return lower.Contains("help") || lower.Contains("what can you do") || lower == "?";
        }

        private static bool IsListAllStudentsRequest(string lower)
        {
            return (lower.Contains("list") || lower.Contains("show") || lower.Contains("all")) &&
                   lower.Contains("student");
        }

        private static bool LooksLikeSubjectQuestion(string lower)
        {
            return lower.Contains("attend") || lower.Contains("present") || lower.Contains("joined") ||
                   lower.Contains("how many") || lower.Contains("class");
        }

        // Recognizes a subject code typed in a sentence, e.g. "how many attended PST 21202 today".
        private static string? TryExtractSubjectCode(string text)
        {
            Match match = Regex.Match(text, @"PST\s?\d{5}", RegexOptions.IgnoreCase);
            return match.Success ? match.Value.ToUpperInvariant().Replace(" ", "") : null;
        }

        private static List<Student> FindMatchingStudents(string query, List<Student> allStudents)
        {
            string trimmed = query.Trim();

            // Exact index-number match wins outright, even if the name-matching
            // below would also have matched something else.
            Student? exactIdMatch = allStudents.FirstOrDefault(s =>
                s.RegNumber.Equals(trimmed, StringComparison.OrdinalIgnoreCase));
            if (exactIdMatch != null)
            {
                return new List<Student> { exactIdMatch };
            }

            // Otherwise, match on index number OR name containing the typed text -
            // pull out any name-like words from a full sentence question so
            // "has kamal attended today" still finds "kamal".
            string[] stopWords = { "has", "did", "is", "attended", "attend", "present", "today", "the",
                "gone", "went", "to", "lecture", "lectures", "class", "classes", "he", "she", "they" };
            string[] words = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Where(w => !stopWords.Contains(w.ToLowerInvariant()))
                .ToArray();
            string candidateName = words.Length > 0 ? string.Join(" ", words) : trimmed;

            List<Student> matches = allStudents
                .Where(s =>
                    s.RegNumber.Contains(trimmed, StringComparison.OrdinalIgnoreCase) ||
                    s.FullName.Contains(candidateName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // If stripping stop-words found nothing, fall back to matching the
            // original raw text as a last resort.
            if (matches.Count == 0 && !candidateName.Equals(trimmed, StringComparison.OrdinalIgnoreCase))
            {
                matches = allStudents
                    .Where(s => s.FullName.Contains(trimmed, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return matches;
        }

        private static string BuildAllStudentsAnswer()
        {
            List<Student> students = new StudentRepository().LoadAll()
                .OrderBy(s => s.FullName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (students.Count == 0)
            {
                return "No students are registered yet.";
            }

            var response = new System.Text.StringBuilder();
            response.AppendLine($"📋 {students.Count} registered student(s):");
            foreach (Student student in students.Take(20))
            {
                response.AppendLine($"• {student.RegNumber} - {student.FullName} ({student.Course})");
            }
            if (students.Count > 20)
            {
                response.AppendLine($"...and {students.Count - 20} more.");
            }

            return response.ToString().TrimEnd();
        }

        private static string BuildSubjectAnswer(string subjectCode)
        {
            Subject? subject = new SubjectRepository().LoadAll()
                .FirstOrDefault(s => s.SubjectCode.Replace(" ", "").Equals(subjectCode, StringComparison.OrdinalIgnoreCase));

            List<AttendanceRecord> records = new AttendanceRepository().LoadAll()
                .Where(r => r.SubjectCode.Replace(" ", "").Equals(subjectCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            string displayName = subject != null
                ? $"{subject.SubjectCode} - {subject.SubjectName}"
                : subjectCode;

            if (records.Count == 0)
            {
                return $"📚 {displayName}\n\nNo attendance has been recorded for this subject yet.";
            }

            int distinctStudents = records.Select(r => r.RegNumber).Distinct().Count();
            List<AttendanceRecord> todayRecords = records.Where(r => r.Date.Date == DateTime.Today).ToList();

            var response = new System.Text.StringBuilder();
            response.AppendLine($"📚 {displayName}");
            response.AppendLine();
            response.AppendLine($"👥 {distinctStudents} distinct student(s) have attended (all time)");
            response.AppendLine($"📅 {todayRecords.Count} student(s) attended today");

            if (todayRecords.Count > 0)
            {
                response.AppendLine();
                response.AppendLine("Today's attendees:");
                foreach (AttendanceRecord record in todayRecords.OrderBy(r => r.TimeMarked).Take(15))
                {
                    response.AppendLine($"• {record.FullName} ({record.RegNumber}) at {record.TimeMarked:hh:mm tt}");
                }
            }

            return response.ToString().TrimEnd();
        }

        private static string BuildStudentAnswer(Student student)
        {
            var attendanceRepository = new AttendanceRepository();
            List<AttendanceRecord> allRecords = attendanceRepository.LoadAll()
                .Where(r => r.RegNumber.Equals(student.RegNumber, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(r => r.TimeMarked)
                .ToList();

            bool attendedToday = allRecords.Any(r => r.Date.Date == DateTime.Today);

            var response = new System.Text.StringBuilder();
            response.AppendLine($"👤 {student.FullName} ({student.RegNumber})");
            response.AppendLine($"🎓 Course: {student.Course}");

            if (!string.IsNullOrWhiteSpace(student.Email))
            {
                response.AppendLine($"✉ Email: {student.Email}");
            }

            response.AppendLine();

            if (attendedToday)
            {
                List<AttendanceRecord> todayRecords = allRecords.Where(r => r.Date.Date == DateTime.Today).ToList();
                string subjectsToday = string.Join(", ", todayRecords.Select(r => r.SubjectName).Distinct());
                response.AppendLine($"✅ Yes - {student.FullName.Split(' ')[0]} has attended today: {subjectsToday}.");
            }
            else
            {
                response.AppendLine($"❌ {student.FullName.Split(' ')[0]} has not attended any lecture today yet.");
            }

            response.AppendLine();
            response.AppendLine($"📊 Total lectures attended (all time): {allRecords.Count}");

            if (allRecords.Count > 0)
            {
                response.AppendLine();
                response.AppendLine("🕒 Recent attendance:");
                foreach (AttendanceRecord record in allRecords.Take(5))
                {
                    response.AppendLine($"  • {record.SubjectName} - {record.Date:dd MMM yyyy} at {record.TimeMarked:hh:mm tt}");
                }
            }

            return response.ToString().TrimEnd();
        }
    }
}
