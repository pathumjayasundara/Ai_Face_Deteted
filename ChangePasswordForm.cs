using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;
using FaceAttendanceSystem.Services;

namespace FaceAttendanceSystem
{
    public partial class ChangePasswordForm : Form
    {
        private readonly LecturerRepository _lecturerRepository = new();
        private readonly AdminAccountRepository _adminAccountRepository = new();

        public ChangePasswordForm()
        {
            InitializeComponent();
            changeButton.Click += ChangeButton_Click;
            cancelButton.Click += (s, e) => Close();
        }

        private void ChangeButton_Click(object? sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string oldPassword = oldPasswordTextBox.Text;
            string newPassword = newPasswordTextBox.Text;
            string confirmPassword = confirmPasswordTextBox.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                ShowMessage("Please fill in all fields.", false);
                return;
            }

            if (newPassword != confirmPassword)
            {
                ShowMessage("New passwords do not match.", false);
                return;
            }

            if (newPassword.Length < 4)
            {
                ShowMessage("New password should be at least 4 characters.", false);
                return;
            }

            // The built-in Administrator account.
            if (username.Equals(Lecturer.AdminUsername, StringComparison.OrdinalIgnoreCase))
            {
                string currentHash = _adminAccountRepository.GetPasswordHash();
                if (!PasswordHelper.Verify(oldPassword, currentHash))
                {
                    ShowMessage("Old password is incorrect.", false);
                    return;
                }

                _adminAccountRepository.SetPasswordHash(PasswordHelper.Hash(newPassword));
                ShowMessage("Password changed successfully! You can now log in.", true);
                return;
            }

            // A regular lecturer account.
            Lecturer? lecturer = _lecturerRepository.FindByUsername(username);
            if (lecturer == null || !PasswordHelper.Verify(oldPassword, lecturer.PasswordHash))
            {
                ShowMessage("Username or old password is incorrect.", false);
                return;
            }

            lecturer.PasswordHash = PasswordHelper.Hash(newPassword);
            _lecturerRepository.UpdatePassword(lecturer.Username, lecturer.PasswordHash);
            ShowMessage("Password changed successfully! You can now log in.", true);
        }

        private void ShowMessage(string message, bool success)
        {
            messageLabel.ForeColor = success ? Color.FromArgb(30, 130, 30) : Color.Firebrick;
            messageLabel.Text = message;
        }
    }
}
