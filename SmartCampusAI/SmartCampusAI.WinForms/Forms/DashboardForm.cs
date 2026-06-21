using System;
using System.Drawing;
using System.Windows.Forms;
using SmartCampusAI.Data;
using SmartCampusAI.Styles;
using SmartCampusAI.Utils;

namespace SmartCampusAI.Forms
{
    public class DashboardForm : Form
    {
        private Panel _sidebar = new();
        private Panel _mainPanel = new();
        private Label _lblWelcome = new();
        private Label _lblRole = new();

        // Stat cards
        private Panel _cardStudents = new();
        private Panel _cardAttendance = new();
        private Panel _cardFace = new();

        // Nav buttons
        private Button _btnStudents = new();
        private Button _btnUsers = new();
        private Button _btnFaceRecog = new();
        private Button _btnAttendance = new();
        private Button _btnLogout = new();

        private readonly StudentRepository _studentRepo = new();
        private readonly AttendanceRepository _attendanceRepo = new();
        private readonly FaceRepository _faceRepo = new();

        public DashboardForm()
        {
            InitializeComponent();
            LoadStats();
        }

        private void InitializeComponent()
        {
            this.Text            = "SmartCampusAI – Dashboard";
            this.Size            = new Size(1000, 650);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.MinimumSize     = new Size(900, 600);
            this.BackColor       = ColorPalette.LightGray;

            // ── SIDEBAR ────────────────────────────────────────────────────────
            _sidebar.Dock      = DockStyle.Left;
            _sidebar.Width     = 220;
            _sidebar.BackColor = ColorPalette.NavyBlue;

            Label lblLogo = new()
            {
                Text      = "🎓 SmartCampusAI",
                Font      = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize  = true,
                Location  = new Point(15, 20)
            };

            Label lblDivider = new()
            {
                BorderStyle = BorderStyle.Fixed3D,
                Height      = 2,
                Location    = new Point(0, 65),
                Width       = 220
            };

            _lblWelcome.Text      = $"👤 {SessionManager.CurrentUser?.Username}";
            _lblWelcome.Font      = new Font("Segoe UI", 10, FontStyle.Bold);
            _lblWelcome.ForeColor = Color.White;
            _lblWelcome.AutoSize  = true;
            _lblWelcome.Location  = new Point(15, 80);

            _lblRole.Text      = $"Role: {SessionManager.CurrentUser?.Role}";
            _lblRole.Font      = new Font("Segoe UI", 8);
            _lblRole.ForeColor = Color.FromArgb(150, 200, 255);
            _lblRole.AutoSize  = true;
            _lblRole.Location  = new Point(15, 103);

            int btnY = 140;
            BuildNavButton(_btnStudents,   "📋  Manage Students",   btnY);        btnY += 52;
            BuildNavButton(_btnAttendance, "📅  View Attendance",   btnY);        btnY += 52;
            BuildNavButton(_btnFaceRecog,  "🤖  Face Recognition",  btnY);        btnY += 52;

            if (SessionManager.IsAdmin)
            {
                BuildNavButton(_btnUsers, "👥  Manage Users", btnY); btnY += 52;
            }

            btnY = 560;
            BuildNavButton(_btnLogout, "🚪  Logout", btnY);
            _btnLogout.BackColor = ColorPalette.Danger;

            _btnStudents.Click   += (s, e) => OpenForm(new StudentForm());
            _btnAttendance.Click += (s, e) => OpenForm(new AttendanceForm());
            _btnFaceRecog.Click  += (s, e) => OpenForm(new FaceRecognitionForm());
            _btnUsers.Click      += (s, e) =>
            {
                if (SessionManager.IsAdmin) OpenForm(new UserForm());
                else Helpers.ShowError("Admin access required.");
            };
            _btnLogout.Click += BtnLogout_Click;

            _sidebar.Controls.AddRange(new Control[]
            {
                lblLogo, lblDivider, _lblWelcome, _lblRole,
                _btnStudents, _btnAttendance, _btnFaceRecog,
                _btnUsers, _btnLogout
            });

            // ── MAIN AREA ──────────────────────────────────────────────────────
            _mainPanel.Dock      = DockStyle.Fill;
            _mainPanel.BackColor = ColorPalette.LightGray;
            _mainPanel.Padding   = new Padding(30);

            Label lblDash = new()
            {
                Text      = "📊 Dashboard Overview",
                Font      = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ColorPalette.NavyBlue,
                AutoSize  = true,
                Location  = new Point(30, 30)
            };

            Label lblDate = new()
            {
                Text      = DateTime.Now.ToString("dddd, MMMM dd yyyy"),
                Font      = new Font("Segoe UI", 10),
                ForeColor = ColorPalette.TextMuted,
                AutoSize  = true,
                Location  = new Point(30, 62)
            };

            // Stat cards
            _cardStudents  = BuildStatCard("👨‍🎓 Total Students",  "0", ColorPalette.MidBlue,  new Point(30, 110));
            _cardAttendance = BuildStatCard("✅ Today Present",    "0", ColorPalette.Success,  new Point(230, 110));
            _cardFace       = BuildStatCard("🤖 Face Profiles",    "0", ColorPalette.Warning,  new Point(430, 110));

            // Quick action buttons
            Label lblQuick = new()
            {
                Text      = "Quick Actions",
                Font      = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = ColorPalette.NavyBlue,
                AutoSize  = true,
                Location  = new Point(30, 250)
            };

            Button btnQuickFace = BuildQuickBtn("🤖 Start Face Recognition", ColorPalette.MidBlue,  new Point(30, 285));
            Button btnQuickStu  = BuildQuickBtn("➕ Add New Student",        ColorPalette.Success,   new Point(230, 285));
            Button btnQuickAtt  = BuildQuickBtn("📅 View Attendance Report", ColorPalette.Warning,   new Point(430, 285));

            btnQuickFace.Click += (s, e) => OpenForm(new FaceRecognitionForm());
            btnQuickStu.Click  += (s, e) => OpenForm(new StudentForm());
            btnQuickAtt.Click  += (s, e) => OpenForm(new AttendanceForm());

            _mainPanel.Controls.AddRange(new Control[]
            {
                lblDash, lblDate,
                _cardStudents, _cardAttendance, _cardFace,
                lblQuick, btnQuickFace, btnQuickStu, btnQuickAtt
            });

            this.Controls.Add(_mainPanel);
            this.Controls.Add(_sidebar);
        }

        private void LoadStats()
        {
            try
            {
                int students   = _studentRepo.Count();
                int todayAtt   = _attendanceRepo.TodayCount();
                int faceProfil = 0;

                // Count distinct students with face data
                var faceList = _faceRepo.GetAllFaceData();
                var ids = new System.Collections.Generic.HashSet<int>();
                foreach (var f in faceList) ids.Add(f.StudentID);
                faceProfil = ids.Count;

                UpdateStatCard(_cardStudents,   students.ToString());
                UpdateStatCard(_cardAttendance, todayAtt.ToString());
                UpdateStatCard(_cardFace,       faceProfil.ToString());
            }
            catch { /* DB not ready yet */ }
        }

        private static Panel BuildStatCard(string title, string value, Color accent, Point loc)
        {
            Panel card = new()
            {
                Size      = new Size(175, 110),
                Location  = loc,
                BackColor = Color.White
            };

            Panel topBar = new()
            {
                Dock      = DockStyle.Top,
                Height    = 5,
                BackColor = accent
            };

            Label lblTitle = new()
            {
                Text      = title,
                Font      = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextMuted,
                AutoSize  = true,
                Location  = new Point(12, 18)
            };

            Label lblValue = new()
            {
                Name      = "valueLabel",
                Text      = value,
                Font      = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = accent,
                AutoSize  = true,
                Location  = new Point(12, 42)
            };

            card.Controls.AddRange(new Control[] { topBar, lblTitle, lblValue });
            return card;
        }

        private static void UpdateStatCard(Panel card, string value)
        {
            foreach (Control c in card.Controls)
            {
                if (c.Name == "valueLabel")
                {
                    c.Text = value;
                    break;
                }
            }
        }

        private static Button BuildQuickBtn(string text, Color color, Point loc)
        {
            Button btn = new()
            {
                Text      = text,
                Location  = loc,
                Size      = new Size(175, 50),
                BackColor = color,
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private static void BuildNavButton(Button btn, string text, int y)
        {
            btn.Text      = text;
            btn.Location  = new Point(0, y);
            btn.Size      = new Size(220, 45);
            btn.BackColor = ColorPalette.NavyBlue;
            btn.ForeColor = Color.White;
            btn.Font      = new Font("Segoe UI", 10);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize  = 0;
            btn.FlatAppearance.MouseOverBackColor = ColorPalette.MidBlue;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding   = new Padding(15, 0, 0, 0);
            btn.Cursor    = Cursors.Hand;
        }

        private void OpenForm(Form form)
        {
            form.MdiParent = null;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            LoadStats(); // refresh stats after child form closes
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            if (Helpers.Confirm("Are you sure you want to logout?"))
            {
                SessionManager.Logout();
                new LoginForm().Show();
                this.Close();
            }
        }
    }
}
