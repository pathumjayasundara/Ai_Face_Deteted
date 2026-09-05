using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Forms;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem
{
    public partial class MainDashboard : Form
    {
        private readonly Lecturer _lecturer;

        public MainDashboard(Lecturer lecturer)
        {
            _lecturer = lecturer;
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            Text = $"Sabaragamuwa University Face Attendance System - {lecturer.FullName}";
            LoadLogo();
            logoutButton.Click += LogoutButton_Click;
            adminButton.Visible = lecturer.IsAdmin;
            ShowScreen(new UC_Home(_lecturer));
            SelectedButtonStyle(homeButton);
            AttachHoverEffect(homeButton);
            AttachHoverEffect(registerButton);
            AttachHoverEffect(subjectsButton);
            AttachHoverEffect(timetableButton);
            AttachHoverEffect(attendanceButton);
            AttachHoverEffect(recordsButton);
            AttachHoverEffect(chatbotButton);
            AttachHoverEffect(adminButton);
        }

        private void LoadLogo()
        {
            try
            {
                if (File.Exists(FilePaths.FacultyLogoPath))
                {
                    logoPictureBox.Image = Image.FromFile(FilePaths.FacultyLogoPath);
                }
            }
            catch
            {
                // Decorative only - the dashboard still works fine without it.
            }
        }

        // Set to true when the user clicks Logout, so Program.cs knows to show
        // the login screen again instead of exiting the whole application.
        public bool LoggedOut { get; private set; }

        private void LogoutButton_Click(object? sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to log out?",
                "Log Out",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            LoggedOut = true;
            Close();
        }

        private void ShowScreen(UserControl screen)
        {
            // IMPORTANT: Controls.Clear() only detaches controls from the panel -
            // it does NOT call Dispose() on them. Since the camera on Register
            // Student / Take Attendance only stops itself when its HandleDestroyed
            // event fires (which only happens on Dispose), leaving old screens
            // un-disposed meant the webcam stayed locked in the background after
            // navigating away, which is exactly what caused the camera issues.
            foreach (Control oldScreen in panelContainer.Controls)
            {
                oldScreen.Dispose();
            }

            panelContainer.Controls.Clear();
            screen.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(screen);
            screen.BringToFront();
        }

        private Button? _activeButton;

        private void SelectedButtonStyle(Button activeButton)
        {
            _activeButton = activeButton;
            foreach (Button button in new[] { homeButton, registerButton, subjectsButton, timetableButton, attendanceButton, recordsButton, chatbotButton, adminButton })
            {
                button.BackColor = button == activeButton ? Color.FromArgb(242, 217, 160) : Color.White;
            }
        }

        private void AttachHoverEffect(Button button)
        {
            button.MouseEnter += (s, e) => button.BackColor = Color.FromArgb(247, 231, 192);
            button.MouseLeave += (s, e) => button.BackColor = button == _activeButton
                ? Color.FromArgb(242, 217, 160)
                : Color.White;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_Home(_lecturer));
            SelectedButtonStyle(homeButton);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_RegisterStudent());
            SelectedButtonStyle(registerButton);
        }

        private void subjectsButton_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_ManageSubjects());
            SelectedButtonStyle(subjectsButton);
        }

        private void timetableButton_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_Timetable());
            SelectedButtonStyle(timetableButton);
        }

        private void attendanceButton_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_TakeAttendance(_lecturer));
            SelectedButtonStyle(attendanceButton);
        }

        private void recordsButton_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_AttendanceRecords());
            SelectedButtonStyle(recordsButton);
        }

        private void chatbotButton_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_Chatbot());
            SelectedButtonStyle(chatbotButton);
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            ShowScreen(new UC_AdminPanel());
            SelectedButtonStyle(adminButton);
        }
    }
}
