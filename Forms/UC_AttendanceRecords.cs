using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Forms
{
    public partial class UC_AttendanceRecords : UserControl
    {
        private readonly AttendanceRepository _attendanceRepository = new();
        private readonly SubjectRepository _subjectRepository = new();

        private static readonly string[] Courses =
        {
            "All Courses",
            "BSc Physical Sciences",
            "BSc Hons in Chemical Technology",
            "BSc Hons in Applied Physics",
            "BSc Hons in Computer Science and Technology"
        };

        public UC_AttendanceRecords()
        {
            InitializeComponent();
            datePicker.Value = DateTime.Today;
            LoadCourseFilter();
            LoadSubjectFilter();

            datePicker.ValueChanged += (s, e) => LoadRecords();
            courseFilterComboBox.SelectedIndexChanged += (s, e) => LoadRecords();
            subjectFilterComboBox.SelectedIndexChanged += (s, e) => LoadRecords();

            LoadRecords();
            LoadSummary();
        }

        // "Subject Wise Show a Wise Count of All Students Joined Students" - for every
        // subject, how many distinct students have ever attended it (all dates combined).
        private void LoadSummary()
        {
            List<AttendanceRecord> allRecords = _attendanceRepository.LoadAll();

            var summaryRows = allRecords
                .GroupBy(r => new { r.SubjectCode, r.SubjectName })
                .Select(g => new
                {
                    g.Key.SubjectCode,
                    g.Key.SubjectName,
                    StudentsJoined = g.Select(r => r.RegNumber).Distinct().Count(),
                    TotalAttendances = g.Count()
                })
                .OrderBy(r => r.SubjectCode)
                .ToList();

            summaryGrid.DataSource = summaryRows;
        }

        private void LoadCourseFilter()
        {
            courseFilterComboBox.Items.Clear();
            courseFilterComboBox.Items.AddRange(Courses);
            courseFilterComboBox.SelectedIndex = 0;
        }

        private void LoadSubjectFilter()
        {
            List<Subject> subjects = _subjectRepository.LoadAll()
                .OrderBy(s => s.Year).ThenBy(s => s.Semester).ThenBy(s => s.SubjectCode)
                .ToList();

            subjectFilterComboBox.Items.Clear();
            subjectFilterComboBox.Items.Add("All Subjects");
            foreach (Subject subject in subjects)
            {
                subjectFilterComboBox.Items.Add($"{subject.SubjectCode} - {subject.SubjectName}");
            }
            subjectFilterComboBox.SelectedIndex = 0;
        }

        private void LoadRecords()
        {
            IEnumerable<AttendanceRecord> records = _attendanceRepository.GetByDate(datePicker.Value);

            if (courseFilterComboBox.SelectedIndex > 0)
            {
                string course = courseFilterComboBox.Text;
                records = records.Where(r => r.Course.Equals(course, StringComparison.OrdinalIgnoreCase));
            }

            if (subjectFilterComboBox.SelectedIndex > 0)
            {
                string subjectCode = subjectFilterComboBox.Text.Split(" - ")[0];
                records = records.Where(r => r.SubjectCode.Equals(subjectCode, StringComparison.OrdinalIgnoreCase));
            }

            List<object> rows = records
                .OrderBy(r => r.TimeMarked)
                .Select(r => new
                {
                    r.RegNumber,
                    r.FullName,
                    r.Course,
                    r.SubjectCode,
                    r.SubjectName,
                    Time = r.TimeMarked.ToString("hh:mm:ss tt"),
                    r.Status
                })
                .Cast<object>()
                .ToList();

            recordsGrid.DataSource = rows;
            countLabel.Text = $"{rows.Count} student(s) marked present on {datePicker.Value:dd MMM yyyy}";
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadSubjectFilter();
            LoadRecords();
            LoadSummary();
        }
    }
}
