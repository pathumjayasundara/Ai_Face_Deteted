using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;
using FaceAttendanceSystem.Services;

namespace FaceAttendanceSystem
{
    public partial class LoginForm : Form
    {
        private readonly LecturerRepository _lecturerRepository = new();

        public Lecturer? LoggedInLecturer { get; private set; }

        private readonly System.Windows.Forms.Timer _backgroundAnimationTimer = new() { Interval = 40 };
        private float _animationPhase;

        public LoginForm()
        {
            InitializeComponent();
            Text = "Attendance System - Login";
            LoadIcon();
            LoadLogos();
            ShowLoginPanel();
            StartBackgroundAnimation();

            loginTabButton.Click += (s, e) => ShowLoginPanel();
            registerTabButton.Click += (s, e) => ShowRegisterPanel();
            loginButton.Click += LoginButton_Click;
            registerButton.Click += RegisterButton_Click;
            changePasswordLinkLabel.Click += ChangePasswordLink_Click;
            FormClosed += (s, e) =>
            {
                _backgroundAnimationTimer.Stop();
                _cachedBackgroundBrush?.Dispose();
            };
        }

        private void LoadIcon()
        {
            try
            {
                if (File.Exists(FilePaths.AppIconPath))
                {
                    Icon = new Icon(FilePaths.AppIconPath);
                }
            }
            catch
            {
                // Decorative only - the form still works fine without a custom icon.
            }
        }

        // A clean, professional "white light streaks on a red background" animation
        // drifting across the band behind the login card - purely decorative.
        // Double buffering (see DoubleBufferedPanel) plus keeping this Paint handler
        // very cheap (no GDI+ objects allocated per frame) is what stops it from
        // stuttering/flickering.
        private System.Drawing.Drawing2D.LinearGradientBrush? _cachedBackgroundBrush;
        private Size _cachedBrushSize;

        private void StartBackgroundAnimation()
        {
            _backgroundAnimationTimer.Tick += (s, e) =>
            {
                _animationPhase += 0.01f;
                if (_animationPhase > 1000f)
                {
                    _animationPhase = 0;
                }
                backgroundPanel.Invalidate();
            };
            backgroundPanel.Paint += BackgroundPanel_Paint;
            backgroundPanel.Resize += (s, e) => _cachedBackgroundBrush = null; // force a rebuild at the new size
            _backgroundAnimationTimer.Start();
        }

        private void BackgroundPanel_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = backgroundPanel.Width;
            int height = backgroundPanel.Height;

            if (width <= 0 || height <= 0)
            {
                return;
            }

            // Rebuild the gradient brush only when the panel size actually changes,
            // instead of allocating new GDI+ objects on every single animation frame.
            if (_cachedBackgroundBrush == null || _cachedBrushSize.Width != width || _cachedBrushSize.Height != height)
            {
                _cachedBackgroundBrush?.Dispose();
                _cachedBackgroundBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, width, height),
                    Color.FromArgb(150, 22, 60),
                    Color.FromArgb(90, 12, 38),
                    45f);
                _cachedBrushSize = new Size(width, height);
            }

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRectangle(_cachedBackgroundBrush, 0, 0, width, height);

            // A handful of thin white diagonal streaks, each drifting at its own
            // steady speed - a light, reused Pen only (no per-frame allocations).
            using var streakPen = new Pen(Color.FromArgb(50, 255, 255, 255), 2.2f);
            int streakCount = 6;
            float travel = width + height + 300f;

            for (int i = 0; i < streakCount; i++)
            {
                float speed = 55f + (i * 14f);
                float startOffset = i * (travel / streakCount);
                float progress = (_animationPhase * speed + startOffset) % travel;

                float sx = progress - 200f;
                float sy = -100f;
                float ex = sx - height - 200f;
                float ey = height + 100f;

                g.DrawLine(streakPen, sx, sy, ex, ey);
            }
        }

        private void LoadLogos()
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
                // If the logo can't be loaded for any reason, the form still works fine without it.
            }
        }

        private void ShowLoginPanel()
        {
            loginPanel.Visible = true;
            registerPanel.Visible = false;
            loginTabButton.BackColor = Color.FromArgb(141, 21, 58);
            loginTabButton.ForeColor = Color.White;
            registerTabButton.BackColor = Color.White;
            registerTabButton.ForeColor = Color.FromArgb(141, 21, 58);
            messageLabel.Text = string.Empty;
        }

        private void ShowRegisterPanel()
        {
            loginPanel.Visible = false;
            registerPanel.Visible = true;
            registerTabButton.BackColor = Color.FromArgb(141, 21, 58);
            registerTabButton.ForeColor = Color.White;
            loginTabButton.BackColor = Color.White;
            loginTabButton.ForeColor = Color.FromArgb(141, 21, 58);
            messageLabel.Text = string.Empty;
        }

        private void LoginButton_Click(object? sender, EventArgs e)
        {
            string username = loginUsernameTextBox.Text.Trim();
            string password = loginPasswordTextBox.Text;

            // The built-in Administrator account (password is changeable via "Change Password").
            if (username.Equals(Lecturer.AdminUsername, StringComparison.OrdinalIgnoreCase))
            {
                string adminHash = new AdminAccountRepository().GetPasswordHash();
                if (!PasswordHelper.Verify(password, adminHash))
                {
                    messageLabel.ForeColor = Color.Firebrick;
                    messageLabel.Text = "Incorrect username or password.";
                    return;
                }

                LoggedInLecturer = Lecturer.CreateAdminAccount();
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            Lecturer? lecturer = _lecturerRepository.FindByUsername(username);

            if (lecturer == null || !PasswordHelper.Verify(password, lecturer.PasswordHash))
            {
                messageLabel.ForeColor = Color.Firebrick;
                messageLabel.Text = "Incorrect username or password.";
                return;
            }

            LoggedInLecturer = lecturer;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ChangePasswordLink_Click(object? sender, EventArgs e)
        {
            using var changePasswordForm = new ChangePasswordForm();
            changePasswordForm.ShowDialog(this);
        }

        private async void RegisterButton_Click(object? sender, EventArgs e)
        {
            string fullName = regFullNameTextBox.Text.Trim();
            string position = regPositionComboBox.Text;
            string lecturerId = regLecturerIdTextBox.Text.Trim();
            string email = regEmailTextBox.Text.Trim();
            string username = regUsernameTextBox.Text.Trim();
            string password = regPasswordTextBox.Text;
            string confirmPassword = regConfirmPasswordTextBox.Text;

            // Nothing is optional - every field must be filled in before registration is allowed.
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(position) ||
                string.IsNullOrEmpty(lecturerId) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                messageLabel.ForeColor = Color.Firebrick;
                messageLabel.Text = "Please fill in every field - name, position, lecturer ID, email, username and password are all required.";
                return;
            }

            if (!ValidationHelper.IsValidLecturerEmail(email))
            {
                messageLabel.ForeColor = Color.Firebrick;
                messageLabel.Text = $"Please use your official university email ({ValidationHelper.LecturerEmailExample}).";
                return;
            }

            if (password != confirmPassword)
            {
                messageLabel.ForeColor = Color.Firebrick;
                messageLabel.Text = "Passwords do not match.";
                return;
            }

            if (_lecturerRepository.UsernameExists(username))
            {
                messageLabel.ForeColor = Color.Firebrick;
                messageLabel.Text = $"The username '{username}' is already taken - usernames must be unique. Please choose another.";
                return;
            }

            var newLecturer = new Lecturer
            {
                FullName = fullName,
                Position = position,
                LecturerId = lecturerId,
                Email = email,
                Username = username,
                PasswordHash = PasswordHelper.Hash(password)
            };
            _lecturerRepository.Add(newLecturer);

            regFullNameTextBox.Clear();
            regPositionComboBox.SelectedIndex = -1;
            regLecturerIdTextBox.Clear();
            regEmailTextBox.Clear();
            regUsernameTextBox.Clear();
            regPasswordTextBox.Clear();
            regConfirmPasswordTextBox.Clear();

            loginUsernameTextBox.Text = username;
            ShowLoginPanel();
            messageLabel.ForeColor = Color.FromArgb(30, 130, 30);
            messageLabel.Text = "Account created! You can now log in.";

            // Fire-and-forget: a slow/misconfigured mail server should never
            // block the lecturer from continuing to the login screen.
            _ = EmailService.SendLecturerRegistrationEmailAsync(newLecturer);
        }
    }
}
