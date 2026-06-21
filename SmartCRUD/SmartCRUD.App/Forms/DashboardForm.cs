using SmartCRUD.App.Data;
using SmartCRUD.App.Utils;

namespace SmartCRUD.App.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly StudentRepository _studentRepo = new();

        public DashboardForm()
        {
            InitializeComponent();
            ApplyUserInfo();
            LoadDashboardStats();
        }

        private void ApplyUserInfo()
        {
            lblWelcome.Text = $"Welcome, {SessionManager.CurrentUser?.Username}!";
            lblRole.Text    = $"Role: {SessionManager.CurrentUser?.Role}";
            lblDate.Text    = DateTime.Now.ToString("dddd, MMMM d, yyyy");

            // Hide Users nav if not admin
            btnNavUsers.Visible = SessionManager.IsAdmin;
        }

        private void LoadDashboardStats()
        {
            try
            {
                int count = _studentRepo.Count();
                lblStatStudents.Text = count.ToString("N0");
                lblStatDate.Text     = DateTime.Now.ToString("MMM dd");
            }
            catch
            {
                lblStatStudents.Text = "--";
            }
        }

        private void btnNavStudents_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentForm());
            SetActiveNav(btnNavStudents);
        }

        private void btnNavUsers_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsAdmin)
            {
                MessageBox.Show("Administrator access required.", "Access Denied",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new UserForm());
            SetActiveNav(btnNavUsers);
        }

        private void btnNavDashboard_Click(object sender, EventArgs e)
        {
            CloseChildForm();
            SetActiveNav(btnNavDashboard);
            LoadDashboardStats();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SessionManager.Logout();
                new LoginForm().Show();
                this.Close();
            }
        }

        private void btnQuickStudents_Click(object sender, EventArgs e)
        {
            OpenChildForm(new StudentForm());
            SetActiveNav(btnNavStudents);
        }

        private void btnQuickUsers_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsAdmin)
            {
                OpenChildForm(new UserForm());
                SetActiveNav(btnNavUsers);
            }
        }

        private Form? _currentChild;

        private void OpenChildForm(Form child)
        {
            CloseChildForm();
            _currentChild = child;
            child.TopLevel   = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock       = DockStyle.Fill;
            child.BackColor  = AppColors.LightGray;
            pnlContent.Controls.Add(child);
            child.Show();
            child.FormClosed += (s, e) =>
            {
                _currentChild = null;
                LoadDashboardStats();
            };
        }

        private void CloseChildForm()
        {
            if (_currentChild != null && !_currentChild.IsDisposed)
            {
                _currentChild.Close();
                _currentChild = null;
            }
            pnlContent.Controls.Clear();
        }

        private void SetActiveNav(Button activeBtn)
        {
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button btn && btn.Tag?.ToString() == "nav")
                {
                    btn.BackColor = AppColors.SidebarBg;
                    btn.ForeColor = AppColors.SidebarText;
                }
            }
            activeBtn.BackColor = AppColors.SidebarHover;
            activeBtn.ForeColor = Color.White;
        }
    }
}
