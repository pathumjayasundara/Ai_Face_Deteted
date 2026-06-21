using System;
using System.Drawing;
using System.Windows.Forms;
using SmartCampusAI.Data;
using SmartCampusAI.Models;
using SmartCampusAI.Styles;
using SmartCampusAI.Utils;

namespace SmartCampusAI.Forms
{
    public class LoginForm : Form
    {
        private Panel _headerPanel = new();
        private Label _lblTitle = new();
        private Label _lblSubtitle = new();
        private Panel _cardPanel = new();
        private Label _lblUsername = new();
        private TextBox _txtUsername = new();
        private Label _lblPassword = new();
        private TextBox _txtPassword = new();
        private CheckBox _chkShowPass = new();
        private Button _btnLogin = new();
        private Label _lblStatus = new();
        private Label _lblVersion = new();

        private readonly UserRepository _userRepo = new();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text            = "SmartCampusAI – Login";
            this.Size            = new Size(440, 560);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.BackColor       = ColorPalette.LightGray;
            this.Font            = Theme.LabelFont;

            // ── HEADER ──────────────────────────────────────────────────────────
            _headerPanel.Dock      = DockStyle.Top;
            _headerPanel.Height    = 140;
            _headerPanel.BackColor = ColorPalette.NavyBlue;

            _lblTitle.Text      = "🎓 SmartCampusAI";
            _lblTitle.Font      = new Font("Segoe UI", 20, FontStyle.Bold);
            _lblTitle.ForeColor = Color.White;
            _lblTitle.AutoSize  = true;
            _lblTitle.Location  = new Point(80, 35);

            _lblSubtitle.Text      = "AI-Powered Attendance Management";
            _lblSubtitle.Font      = new Font("Segoe UI", 10);
            _lblSubtitle.ForeColor = Color.FromArgb(180, 210, 255);
            _lblSubtitle.AutoSize  = true;
            _lblSubtitle.Location  = new Point(80, 80);

            _headerPanel.Controls.Add(_lblTitle);
            _headerPanel.Controls.Add(_lblSubtitle);

            // ── CARD ─────────────────────────────────────────────────────────────
            _cardPanel.BackColor  = Color.White;
            _cardPanel.Size       = new Size(360, 320);
            _cardPanel.Location   = new Point(40, 160);

            // Username
            _lblUsername.Text     = "Username";
            _lblUsername.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
            _lblUsername.Location = new Point(20, 20);
            _lblUsername.AutoSize = true;

            _txtUsername.Location    = new Point(20, 42);
            _txtUsername.Size        = new Size(320, 30);
            _txtUsername.Font        = Theme.InputFont;
            _txtUsername.BorderStyle = BorderStyle.FixedSingle;
            _txtUsername.Text        = "admin";

            // Password
            _lblPassword.Text     = "Password";
            _lblPassword.Font     = new Font("Segoe UI", 9, FontStyle.Bold);
            _lblPassword.Location = new Point(20, 90);
            _lblPassword.AutoSize = true;

            _txtPassword.Location     = new Point(20, 112);
            _txtPassword.Size         = new Size(320, 30);
            _txtPassword.Font         = Theme.InputFont;
            _txtPassword.BorderStyle  = BorderStyle.FixedSingle;
            _txtPassword.PasswordChar = '●';
            _txtPassword.Text         = "Admin@123";

            // Show password
            _chkShowPass.Text      = "Show Password";
            _chkShowPass.Location  = new Point(20, 152);
            _chkShowPass.AutoSize  = true;
            _chkShowPass.Font      = Theme.SmallFont;
            _chkShowPass.CheckedChanged += (s, e) =>
                _txtPassword.PasswordChar = _chkShowPass.Checked ? '\0' : '●';

            // Login button
            _btnLogin.Text     = "LOGIN";
            _btnLogin.Location = new Point(20, 185);
            _btnLogin.Size     = new Size(320, 42);
            Theme.StylePrimaryButton(_btnLogin);
            _btnLogin.Click   += BtnLogin_Click;

            // Status label
            _lblStatus.Location  = new Point(20, 240);
            _lblStatus.Size      = new Size(320, 50);
            _lblStatus.Font      = new Font("Segoe UI", 9);
            _lblStatus.ForeColor = ColorPalette.Danger;
            _lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            _cardPanel.Controls.AddRange(new Control[]
            {
                _lblUsername, _txtUsername,
                _lblPassword, _txtPassword,
                _chkShowPass, _btnLogin, _lblStatus
            });

            // Version label
            _lblVersion.Text      = "v1.0 | Powered by OpenCvSharp + LBPH";
            _lblVersion.Font      = new Font("Segoe UI", 8);
            _lblVersion.ForeColor = ColorPalette.TextMuted;
            _lblVersion.AutoSize  = true;
            _lblVersion.Location  = new Point(100, 500);

            this.Controls.AddRange(new Control[] { _headerPanel, _cardPanel, _lblVersion });

            // Enter key triggers login
            this.AcceptButton = _btnLogin;

            // Keyboard shortcut
            _txtPassword.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) BtnLogin_Click(s, e);
            };
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            _lblStatus.Text = string.Empty;
            string username = _txtUsername.Text.Trim();
            string password = _txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                _lblStatus.Text = "Please enter username and password.";
                return;
            }

            try
            {
                _btnLogin.Enabled = false;
                _btnLogin.Text    = "Authenticating…";

                User? user = _userRepo.GetByCredentials(username, password);
                if (user == null)
                {
                    _lblStatus.Text    = "❌ Invalid username or password.";
                    _btnLogin.Enabled  = true;
                    _btnLogin.Text     = "LOGIN";
                    return;
                }

                SessionManager.Login(user);
                DashboardForm dashboard = new();
                dashboard.Show();
                this.Hide();
                dashboard.FormClosed += (s2, e2) => this.Close();
            }
            catch (Exception ex)
            {
                _lblStatus.Text   = $"Connection error: {ex.Message}";
                _btnLogin.Enabled = true;
                _btnLogin.Text    = "LOGIN";
            }
        }
    }
}
