using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Forms
{
    public partial class UC_Timetable : UserControl
    {
        private readonly TimetableRepository _timetableRepository = new();
        private readonly SubjectRepository _subjectRepository = new();
        private List<Subject> _subjects = new();

        public UC_Timetable()
        {
            InitializeComponent();
            dayComboBox.SelectedIndex = 0;
            startTimePicker.Value = DateTime.Today.AddHours(8);
            endTimePicker.Value = DateTime.Today.AddHours(9);
            LoadSubjectsIntoComboBox();
            LoadTimetable();
            subjectComboBox.SelectedIndexChanged += SubjectComboBox_SelectedIndexChanged;
            importPhotoButton.Click += ImportPhotoButton_Click;
        }

        private void ImportPhotoButton_Click(object? sender, EventArgs e)
        {
            using var importForm = new TimetableImportForm();
            importForm.ShowDialog(this.FindForm());

            if (importForm.AnyEntriesImported)
            {
                LoadTimetable();
                statusLabel.Text = "Timetable updated from the imported photo/PDF.";
                statusLabel.ForeColor = Color.FromArgb(30, 130, 30);
            }
        }

        private class SubjectListItem
        {
            public string SubjectCode { get; set; } = string.Empty;
            public string SubjectName { get; set; } = string.Empty;
            public int Year { get; set; }
            public int Semester { get; set; }
            public string DisplayText => $"{SubjectCode} - {SubjectName}";
        }

        // As soon as a subject is picked, auto-select the Year/Semester it actually
        // belongs to (e.g. PST 21202 -> Year 2, Semester 1) instead of leaving the
        // lecturer to set them manually and risk a mismatch.
        private void SubjectComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (subjectComboBox.SelectedItem is not SubjectListItem selected)
            {
                return;
            }

            if (selected.Year is >= 1 and <= 4)
            {
                yearComboBox.SelectedIndex = selected.Year - 1;
            }

            if (selected.Semester is >= 1 and <= 2)
            {
                semesterComboBox.SelectedIndex = selected.Semester - 1;
            }
        }

        private void LoadSubjectsIntoComboBox()
        {
            _subjects = _subjectRepository.LoadAll();
            subjectComboBox.DataSource = null;
            subjectComboBox.DisplayMember = "DisplayText";
            subjectComboBox.DataSource = _subjects
                .Select(s => new SubjectListItem { SubjectCode = s.SubjectCode, SubjectName = s.SubjectName, Year = s.Year, Semester = s.Semester })
                .ToList();

            if (_subjects.Count == 0)
            {
                statusLabel.Text = "No subjects yet. Please add subjects first in 'Manage Subjects'.";
                statusLabel.ForeColor = Color.Firebrick;
                addButton.Enabled = false;
            }
        }

        private void LoadTimetable()
        {
            List<TimetableEntry> entries = _timetableRepository.LoadAll()
                .OrderBy(e => e.Day)
                .ThenBy(e => e.StartTime)
                .ToList();

            timetableGrid.DataSource = entries
                .Select(e => new
                {
                    e.Id,
                    Day = e.Day.ToString(),
                    Time = $"{e.StartTime} - {e.EndTime}",
                    e.Room,
                    e.SubjectCode,
                    e.SubjectName,
                    e.Year,
                    e.Semester
                })
                .ToList();

            if (timetableGrid.Columns["Id"] != null)
            {
                timetableGrid.Columns["Id"].Visible = false;
            }

            countLabel.Text = $"{entries.Count} scheduled class(es)";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (subjectComboBox.SelectedItem is not SubjectListItem selected)
            {
                statusLabel.Text = "Please select a subject.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            string subjectCode = selected.SubjectCode;
            string subjectName = selected.SubjectName;

            if (string.IsNullOrWhiteSpace(roomTextBox.Text))
            {
                statusLabel.Text = "Please enter the room / hall.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            _timetableRepository.Add(new TimetableEntry
            {
                Day = (DayOfWeek)dayComboBox.SelectedIndex,
                StartTime = startTimePicker.Value.ToString("HH:mm"),
                EndTime = endTimePicker.Value.ToString("HH:mm"),
                Room = roomTextBox.Text.Trim(),
                SubjectCode = subjectCode,
                SubjectName = subjectName,
                Year = int.Parse(yearComboBox.Text),
                Semester = int.Parse(semesterComboBox.Text)
            });

            statusLabel.Text = "Class added to timetable.";
            statusLabel.ForeColor = Color.FromArgb(30, 130, 30);
            roomTextBox.Clear();
            LoadTimetable();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (timetableGrid.CurrentRow == null)
            {
                statusLabel.Text = "Please select a class from the list first.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            string id = timetableGrid.CurrentRow.Cells["Id"].Value?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            _timetableRepository.Delete(id);
            statusLabel.Text = "Class removed from timetable.";
            statusLabel.ForeColor = Color.FromArgb(30, 130, 30);
            LoadTimetable();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            LoadSubjectsIntoComboBox();
            LoadTimetable();
        }
    }
}
