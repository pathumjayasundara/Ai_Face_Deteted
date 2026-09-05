using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Forms
{
    public partial class UC_ManageSubjects : UserControl
    {
        private readonly SubjectRepository _subjectRepository = new();

        public UC_ManageSubjects()
        {
            InitializeComponent();
            yearComboBox.SelectedIndex = 0;
            semesterComboBox.SelectedIndex = 0;
            creditsNumeric.Value = 3;

            filterYearComboBox.SelectedIndex = 0;
            filterSemesterComboBox.SelectedIndex = 0;
            filterYearComboBox.SelectedIndexChanged += (s, e) => LoadSubjects();
            filterSemesterComboBox.SelectedIndexChanged += (s, e) => LoadSubjects();
            searchTextBox.TextChanged += (s, e) => LoadSubjects();
            subjectCodeTextBox.TextChanged += SubjectCodeTextBox_TextChanged;

            LoadSubjects();
        }

        // PST subject codes encode Year + Semester as the first two digits after
        // the "PST" prefix, e.g. "PST 41693" -> digit '4' = Year 4, digit '1' =
        // Semester 1. As soon as those two digits are typed, auto-select them.
        private void SubjectCodeTextBox_TextChanged(object? sender, EventArgs e)
        {
            string digitsOnly = new string(subjectCodeTextBox.Text.Where(char.IsDigit).ToArray());

            if (digitsOnly.Length < 2)
            {
                return;
            }

            int yearDigit = digitsOnly[0] - '0';
            int semesterDigit = digitsOnly[1] - '0';

            if (yearDigit is >= 1 and <= 4)
            {
                yearComboBox.SelectedIndex = yearDigit - 1;
            }

            if (semesterDigit is >= 1 and <= 2)
            {
                semesterComboBox.SelectedIndex = semesterDigit - 1;
            }
        }

        private void LoadSubjects()
        {
            IEnumerable<Subject> subjects = _subjectRepository.LoadAll();

            if (filterYearComboBox.SelectedIndex > 0)
            {
                int year = filterYearComboBox.SelectedIndex; // index 1-4 maps to Year 1-4
                subjects = subjects.Where(s => s.Year == year);
            }

            if (filterSemesterComboBox.SelectedIndex > 0)
            {
                int semester = filterSemesterComboBox.SelectedIndex; // index 1-2 maps to Semester 1-2
                subjects = subjects.Where(s => s.Semester == semester);
            }

            string search = searchTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(search))
            {
                subjects = subjects.Where(s =>
                    s.SubjectCode.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    s.SubjectName.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            List<Subject> results = subjects
                .OrderBy(s => s.Year).ThenBy(s => s.Semester).ThenBy(s => s.SubjectCode)
                .ToList();

            subjectsGrid.DataSource = results
                .Select(s => new
                {
                    s.SubjectCode,
                    s.SubjectName,
                    s.Year,
                    s.Semester,
                    s.Credits
                })
                .ToList();

            countLabel.Text = $"{results.Count} subject(s)";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string code = subjectCodeTextBox.Text.Trim();
            string name = subjectNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
            {
                statusLabel.Text = "Subject code and name are required.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            if (_subjectRepository.SubjectCodeExists(code))
            {
                statusLabel.Text = "A subject with this code already exists.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            _subjectRepository.Add(new Subject
            {
                SubjectCode = code,
                SubjectName = name,
                Year = int.Parse(yearComboBox.Text),
                Semester = int.Parse(semesterComboBox.Text),
                Credits = (int)creditsNumeric.Value
            });

            statusLabel.Text = $"Subject '{name}' added.";
            statusLabel.ForeColor = Color.FromArgb(30, 130, 30);

            subjectCodeTextBox.Clear();
            subjectNameTextBox.Clear();
            creditsNumeric.Value = 3;
            LoadSubjects();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (subjectsGrid.CurrentRow == null)
            {
                statusLabel.Text = "Please select a subject from the list first.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            string code = subjectsGrid.CurrentRow.Cells["SubjectCode"].Value?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(code))
            {
                return;
            }

            _subjectRepository.Delete(code);
            statusLabel.Text = $"Subject '{code}' deleted.";
            statusLabel.ForeColor = Color.FromArgb(30, 130, 30);
            LoadSubjects();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadSubjects();
        }
    }
}
