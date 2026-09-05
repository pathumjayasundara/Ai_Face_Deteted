using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;
using FaceAttendanceSystem.Services;

namespace FaceAttendanceSystem.Forms
{
    public partial class UC_AdminPanel : UserControl
    {
        private readonly LecturerRepository _lecturerRepository = new();
        private readonly StudentRepository _studentRepository = new();
        private readonly AttendanceRepository _attendanceRepository = new();
        private readonly EmailSettingsRepository _emailSettingsRepository = new();

        public UC_AdminPanel()
        {
            InitializeComponent();
            LoadLecturers();
            LoadStudents();
            LoadAttendanceRecords();
            LoadEmailSettings();

            deleteLecturerButton.Click += DeleteLecturerButton_Click;
            deleteStudentButton.Click += DeleteStudentButton_Click;
            deleteAttendanceButton.Click += DeleteAttendanceButton_Click;
            saveEmailSettingsButton.Click += SaveEmailSettingsButton_Click;
            testEmailButton.Click += TestEmailButton_Click;
            refreshAllButton.Click += (s, e) =>
            {
                LoadLecturers();
                LoadStudents();
                LoadAttendanceRecords();
            };
        }

        private void LoadEmailSettings()
        {
            EmailSettings settings = _emailSettingsRepository.Load();
            smtpHostTextBox.Text = settings.SmtpHost;
            smtpPortNumeric.Value = settings.SmtpPort;
            useSslCheckBox.Checked = settings.UseSsl;
            senderEmailTextBox.Text = settings.SenderEmail;
            senderPasswordTextBox.Text = settings.SenderPassword;
            senderDisplayNameTextBox.Text = settings.SenderDisplayName;
        }

        private void SaveEmailSettingsButton_Click(object? sender, EventArgs e)
        {
            var settings = new EmailSettings
            {
                SmtpHost = smtpHostTextBox.Text.Trim(),
                SmtpPort = (int)smtpPortNumeric.Value,
                UseSsl = useSslCheckBox.Checked,
                SenderEmail = senderEmailTextBox.Text.Trim(),
                SenderPassword = senderPasswordTextBox.Text,
                SenderDisplayName = senderDisplayNameTextBox.Text.Trim()
            };

            _emailSettingsRepository.Save(settings);
            emailStatusLabel.ForeColor = Color.FromArgb(30, 130, 30);
            emailStatusLabel.Text = "✅ Email settings saved. Click 'Send Test Email' to verify they work.";
        }

        private async void TestEmailButton_Click(object? sender, EventArgs e)
        {
            string toAddress = testEmailToTextBox.Text.Trim();
            if (string.IsNullOrEmpty(toAddress))
            {
                emailStatusLabel.ForeColor = Color.Firebrick;
                emailStatusLabel.Text = "Please enter an email address to send the test to.";
                return;
            }

            // Save first, so the test actually uses whatever is currently in the fields.
            SaveEmailSettingsButton_Click(sender, e);

            testEmailButton.Enabled = false;
            emailStatusLabel.ForeColor = Color.FromArgb(141, 21, 58);
            emailStatusLabel.Text = "Sending test email... please wait.";

            bool success = await EmailService.SendAsync(
                toAddress,
                "Attendance System",
                "<div style='font-family:Segoe UI, Arial, sans-serif;'>" +
                "<h2 style='color:#8D1538;'>Test Email</h2>" +
                "<p>If you're reading this, email sending is configured correctly! ✅</p></div>");

            testEmailButton.Enabled = true;

            if (success)
            {
                emailStatusLabel.ForeColor = Color.FromArgb(30, 130, 30);
                emailStatusLabel.Text = $"✅ Test email sent successfully to {toAddress}! Check their inbox (and spam folder).";
            }
            else
            {
                emailStatusLabel.ForeColor = Color.Firebrick;
                emailStatusLabel.Text = "❌ Sending failed. Check 'XML Local Data Files\\email-log.txt' next to the .exe " +
                    "for the exact reason (wrong password, blocked port, etc.).";
            }
        }

        private void LoadLecturers()
        {
            List<Lecturer> lecturers = _lecturerRepository.LoadAll();
            lecturersGrid.DataSource = lecturers
                .Select(l => new { l.LecturerId, l.FullName, l.Position, l.Username, l.Email })
                .ToList();
            lecturerCountLabel.Text = $"{lecturers.Count} lecturer account(s)";
        }

        private void LoadStudents()
        {
            List<Student> students = _studentRepository.LoadAll();
            studentsGrid.DataSource = students
                .Select(s => new { s.RegNumber, s.FullName, s.Course, s.Email })
                .ToList();
            studentCountLabel.Text = $"{students.Count} registered student(s)";
        }

        private void LoadAttendanceRecords()
        {
            List<AttendanceRecord> records = _attendanceRepository.LoadAll()
                .OrderByDescending(r => r.TimeMarked)
                .ToList();

            attendanceGrid.DataSource = records
                .Select(r => new
                {
                    r.Id,
                    r.RegNumber,
                    r.FullName,
                    r.SubjectCode,
                    r.SubjectName,
                    Date = r.Date.ToString("dd MMM yyyy"),
                    Time = r.TimeMarked.ToString("hh:mm:ss tt"),
                    r.Status
                })
                .ToList();

            if (attendanceGrid.Columns["Id"] != null)
            {
                attendanceGrid.Columns["Id"].Visible = false;
            }

            attendanceCountLabel.Text = $"{records.Count} attendance record(s)";
        }

        private void DeleteLecturerButton_Click(object? sender, EventArgs e)
        {
            if (lecturersGrid.CurrentRow == null)
            {
                MessageBox.Show("Please select a lecturer to delete.", "Admin Panel",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string username = lecturersGrid.CurrentRow.Cells["Username"].Value?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(username))
            {
                return;
            }

            if (username.Equals(Lecturer.AdminUsername, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("The built-in Administrator account cannot be deleted.", "Admin Panel",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Delete lecturer account '{username}'? This cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _lecturerRepository.Delete(username);
                LoadLecturers();
            }
        }

        private void DeleteStudentButton_Click(object? sender, EventArgs e)
        {
            if (studentsGrid.CurrentRow == null)
            {
                MessageBox.Show("Please select a student to delete.", "Admin Panel",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string regNumber = studentsGrid.CurrentRow.Cells["RegNumber"].Value?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(regNumber))
            {
                return;
            }

            DialogResult confirm = MessageBox.Show($"Delete student '{regNumber}'? Their face data and photos will also be removed.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _studentRepository.Delete(regNumber);

                // Also clean up their stored photo and zipped training photos.
                try
                {
                    string photoPath = FilePaths.GetPhotoPath(regNumber);
                    if (File.Exists(photoPath))
                    {
                        File.Delete(photoPath);
                    }

                    string zipPath = FilePaths.GetTrainingPhotosZipPath(regNumber);
                    if (File.Exists(zipPath))
                    {
                        File.Delete(zipPath);
                    }
                }
                catch
                {
                    // Best-effort cleanup only - the student record itself is already deleted.
                }

                LoadStudents();
            }
        }

        private void DeleteAttendanceButton_Click(object? sender, EventArgs e)
        {
            if (attendanceGrid.CurrentRow == null)
            {
                MessageBox.Show("Please select an attendance record to delete.", "Admin Panel",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string id = attendanceGrid.CurrentRow.Cells["Id"].Value?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            DialogResult confirm = MessageBox.Show("Delete this attendance record? This cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                _attendanceRepository.Delete(id);
                LoadAttendanceRecords();
            }
        }
    }
}
