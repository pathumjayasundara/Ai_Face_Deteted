using SmartCRUD.App.Utils;

namespace SmartCRUD.App.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        // Sidebar
        private Panel  pnlSidebar;
        private Label  lblLogo;
        private Label  lblLogoSub;
        private Panel  pnlDivider1;
        private Label  lblWelcome;
        private Label  lblRole;
        private Button btnNavDashboard;
        private Button btnNavStudents;
        private Button btnNavUsers;
        private Button btnLogout;

        // Top bar
        private Panel pnlTopBar;
        private Label lblPageTitle;
        private Label lblDate;

        // Content
        private Panel pnlContent;

        // Dashboard welcome card
        private Panel pnlDashHome;
        private Label lblDashGreet;
        private Label lblDashSub;

        // Stat cards
        private Panel pnlCardStudents;
        private Label lblStatStudentsTitle;
        private Label lblStatStudents;
        private Panel pnlCardDate;
        private Label lblStatDateTitle;
        private Label lblStatDate;

        // Quick actions
        private Label  lblQuickTitle;
        private Button btnQuickStudents;
        private Button btnQuickUsers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components       = new System.ComponentModel.Container();

            // Sidebar
            this.pnlSidebar       = new Panel();
            this.lblLogo          = new Label();
            this.lblLogoSub       = new Label();
            this.pnlDivider1      = new Panel();
            this.lblWelcome       = new Label();
            this.lblRole          = new Label();
            this.btnNavDashboard  = new Button();
            this.btnNavStudents   = new Button();
            this.btnNavUsers      = new Button();
            this.btnLogout        = new Button();

            // Top bar
            this.pnlTopBar        = new Panel();
            this.lblPageTitle     = new Label();
            this.lblDate          = new Label();

            // Content
            this.pnlContent       = new Panel();

            // Dashboard home
            this.pnlDashHome      = new Panel();
            this.lblDashGreet     = new Label();
            this.lblDashSub       = new Label();
            this.pnlCardStudents  = new Panel();
            this.lblStatStudentsTitle = new Label();
            this.lblStatStudents  = new Label();
            this.pnlCardDate      = new Panel();
            this.lblStatDateTitle = new Label();
            this.lblStatDate      = new Label();
            this.lblQuickTitle    = new Label();
            this.btnQuickStudents = new Button();
            this.btnQuickUsers    = new Button();

            // ── FORM ────────────────────────────────────────────────────────
            this.Text            = "SmartCRUD – Dashboard";
            this.Size            = new Size(1200, 720);
            this.MinimumSize     = new Size(1000, 600);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.BackColor       = AppColors.LightGray;
            this.Font            = new Font("Segoe UI", 9F);

            // ── SIDEBAR ─────────────────────────────────────────────────────
            this.pnlSidebar.Dock      = DockStyle.Left;
            this.pnlSidebar.Width     = 230;
            this.pnlSidebar.BackColor = AppColors.SidebarBg;

            this.lblLogo.Text      = "SmartCRUD";
            this.lblLogo.Font      = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.AutoSize  = true;
            this.lblLogo.Location  = new Point(20, 22);

            this.lblLogoSub.Text      = "Management System";
            this.lblLogoSub.Font      = new Font("Segoe UI", 8F);
            this.lblLogoSub.ForeColor = AppColors.SidebarText;
            this.lblLogoSub.AutoSize  = true;
            this.lblLogoSub.Location  = new Point(22, 58);

            this.pnlDivider1.Location  = new Point(0, 80);
            this.pnlDivider1.Size      = new Size(230, 1);
            this.pnlDivider1.BackColor = Color.FromArgb(30, 255, 255, 255);

            this.lblWelcome.Location  = new Point(15, 95);
            this.lblWelcome.Size      = new Size(200, 20);
            this.lblWelcome.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.White;
            this.lblWelcome.Text      = "Welcome!";

            this.lblRole.Location  = new Point(15, 116);
            this.lblRole.Size      = new Size(200, 18);
            this.lblRole.Font      = new Font("Segoe UI", 8F);
            this.lblRole.ForeColor = AppColors.SidebarText;
            this.lblRole.Text      = "Role: User";

            // Nav buttons helper
            BuildNavButton(this.btnNavDashboard, "  🏠  Dashboard",  145, true);
            BuildNavButton(this.btnNavStudents,  "  👥  Students",   195, false);
            BuildNavButton(this.btnNavUsers,     "  🔐  Users",      245, false);

            this.btnNavDashboard.Click += new EventHandler(this.btnNavDashboard_Click);
            this.btnNavStudents.Click  += new EventHandler(this.btnNavStudents_Click);
            this.btnNavUsers.Click     += new EventHandler(this.btnNavUsers_Click);

            // Logout button
            this.btnLogout.Text      = "  🚪  Logout";
            this.btnLogout.Location  = new Point(0, 620);
            this.btnLogout.Size      = new Size(230, 50);
            this.btnLogout.Font      = new Font("Segoe UI", 10F);
            this.btnLogout.ForeColor = Color.FromArgb(255, 150, 150);
            this.btnLogout.BackColor = AppColors.SidebarBg;
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            this.btnLogout.Cursor    = Cursors.Hand;
            this.btnLogout.Click    += new EventHandler(this.btnLogout_Click);

            this.pnlSidebar.Controls.Add(this.lblLogo);
            this.pnlSidebar.Controls.Add(this.lblLogoSub);
            this.pnlSidebar.Controls.Add(this.pnlDivider1);
            this.pnlSidebar.Controls.Add(this.lblWelcome);
            this.pnlSidebar.Controls.Add(this.lblRole);
            this.pnlSidebar.Controls.Add(this.btnNavDashboard);
            this.pnlSidebar.Controls.Add(this.btnNavStudents);
            this.pnlSidebar.Controls.Add(this.btnNavUsers);
            this.pnlSidebar.Controls.Add(this.btnLogout);

            // ── TOP BAR ─────────────────────────────────────────────────────
            this.pnlTopBar.Dock      = DockStyle.Top;
            this.pnlTopBar.Height    = 60;
            this.pnlTopBar.BackColor = AppColors.White;

            this.lblPageTitle.Text      = "Dashboard";
            this.lblPageTitle.Font      = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblPageTitle.ForeColor = AppColors.NavyBlue;
            this.lblPageTitle.AutoSize  = true;
            this.lblPageTitle.Location  = new Point(20, 14);

            this.lblDate.Text      = DateTime.Now.ToString("dddd, MMMM d, yyyy");
            this.lblDate.Font      = new Font("Segoe UI", 9F);
            this.lblDate.ForeColor = AppColors.TextMuted;
            this.lblDate.AutoSize  = true;
            this.lblDate.Location  = new Point(800, 20);

            this.pnlTopBar.Controls.Add(this.lblPageTitle);
            this.pnlTopBar.Controls.Add(this.lblDate);

            // ── CONTENT PANEL ────────────────────────────────────────────────
            this.pnlContent.Dock      = DockStyle.Fill;
            this.pnlContent.BackColor = AppColors.LightGray;
            this.pnlContent.Padding   = new Padding(20);

            // ── DASHBOARD HOME ───────────────────────────────────────────────
            this.pnlDashHome.Dock      = DockStyle.Fill;
            this.pnlDashHome.BackColor = AppColors.LightGray;

            this.lblDashGreet.Text      = "📊  Welcome to SmartCRUD!";
            this.lblDashGreet.Font      = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblDashGreet.ForeColor = AppColors.NavyBlue;
            this.lblDashGreet.AutoSize  = true;
            this.lblDashGreet.Location  = new Point(0, 10);

            this.lblDashSub.Text      = "Manage your data efficiently with real-time SQL Server integration.";
            this.lblDashSub.Font      = new Font("Segoe UI", 10F);
            this.lblDashSub.ForeColor = AppColors.TextMuted;
            this.lblDashSub.AutoSize  = true;
            this.lblDashSub.Location  = new Point(2, 55);

            // Stat card – Students
            BuildStatCard(
                this.pnlCardStudents,
                this.lblStatStudentsTitle, "Total Students",
                this.lblStatStudents, "0",
                AppColors.MidBlue,
                new Point(0, 100));

            // Stat card – Date
            BuildStatCard(
                this.pnlCardDate,
                this.lblStatDateTitle, "Today's Date",
                this.lblStatDate, DateTime.Now.ToString("MMM dd"),
                AppColors.Success,
                new Point(210, 100));

            // Quick actions
            this.lblQuickTitle.Text      = "Quick Actions";
            this.lblQuickTitle.Font      = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblQuickTitle.ForeColor = AppColors.NavyBlue;
            this.lblQuickTitle.AutoSize  = true;
            this.lblQuickTitle.Location  = new Point(0, 250);

            BuildQuickButton(this.btnQuickStudents, "👥  Manage Students", AppColors.MidBlue,  new Point(0,   290));
            BuildQuickButton(this.btnQuickUsers,    "🔐  Manage Users",    AppColors.Success,   new Point(220, 290));

            this.btnQuickStudents.Click += new EventHandler(this.btnQuickStudents_Click);
            this.btnQuickUsers.Click    += new EventHandler(this.btnQuickUsers_Click);

            this.pnlDashHome.Controls.Add(this.lblDashGreet);
            this.pnlDashHome.Controls.Add(this.lblDashSub);
            this.pnlDashHome.Controls.Add(this.pnlCardStudents);
            this.pnlDashHome.Controls.Add(this.pnlCardDate);
            this.pnlDashHome.Controls.Add(this.lblQuickTitle);
            this.pnlDashHome.Controls.Add(this.btnQuickStudents);
            this.pnlDashHome.Controls.Add(this.btnQuickUsers);

            this.pnlContent.Controls.Add(this.pnlDashHome);

            // Add in correct Z-order
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopBar);
            this.Controls.Add(this.pnlSidebar);
        }

        private static void BuildNavButton(Button btn, string text, int top, bool active)
        {
            btn.Text      = text;
            btn.Location  = new Point(0, top);
            btn.Size      = new Size(230, 46);
            btn.Font      = new Font("Segoe UI", 10F);
            btn.ForeColor = active ? Color.White : AppColors.SidebarText;
            btn.BackColor = active ? AppColors.SidebarHover : AppColors.SidebarBg;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize          = 0;
            btn.FlatAppearance.MouseOverBackColor  = AppColors.SidebarHover;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Cursor    = Cursors.Hand;
            btn.Tag       = "nav";
        }

        private static void BuildStatCard(Panel pnl, Label lblTitle, string title,
                                          Label lblValue, string value, Color accent, Point loc)
        {
            pnl.Location  = loc;
            pnl.Size      = new Size(190, 120);
            pnl.BackColor = Color.White;

            Panel accentBar = new()
            {
                Dock      = DockStyle.Top,
                Height    = 5,
                BackColor = accent
            };

            lblTitle.Text      = title;
            lblTitle.Font      = new Font("Segoe UI", 9F);
            lblTitle.ForeColor = AppColors.TextMuted;
            lblTitle.AutoSize  = true;
            lblTitle.Location  = new Point(14, 18);

            lblValue.Text      = value;
            lblValue.Font      = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblValue.ForeColor = accent;
            lblValue.AutoSize  = true;
            lblValue.Location  = new Point(14, 42);

            pnl.Controls.Add(accentBar);
            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblValue);
        }

        private static void BuildQuickButton(Button btn, string text, Color color, Point loc)
        {
            btn.Text      = text;
            btn.Location  = loc;
            btn.Size      = new Size(200, 50);
            btn.Font      = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor    = Cursors.Hand;
        }
    }
}
