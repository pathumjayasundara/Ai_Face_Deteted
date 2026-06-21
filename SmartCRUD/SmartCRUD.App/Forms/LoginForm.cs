using SmartCRUD.App.Data;
using SmartCRUD.App.Utils;

namespace SmartCRUD.App.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository _userRepo = new();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                PerformLogin();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPassword.Focus();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●';
        }

        private void PerformLogin()
        {
            lblError.Text    = string.Empty;
            lblError.Visible = false;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowError("Please enter your username.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("Please enter your password.");
                txtPassword.Focus();
                return;
            }

            try
            {
                SetLoading(true);

                var user = _userRepo.GetByCredentials(username, password);
                if (user == null)
                {
                    ShowError("Invalid username or password. Please try again.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                SessionManager.Login(user);

                var dashboard = new DashboardForm();
                dashboard.Show();
                this.Hide();
                dashboard.FormClosed += (s, args) => this.Close();
            }
            catch (Exception ex)
            {
                ShowError($"Connection error: {ex.Message}\n\nCheck your SQL Server connection string in DatabaseConnection.cs");
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void ShowError(string message)
        {
            lblError.Text    = message;
            lblError.Visible = true;
        }

        private void SetLoading(bool loading)
        {
            btnLogin.Enabled  = !loading;
            btnLogin.Text     = loading ? "Authenticating…" : "LOGIN";
            txtUsername.Enabled = !loading;
            txtPassword.Enabled = !loading;
            Application.DoEvents();
        }
    }
}
