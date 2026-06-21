using SmartCRUD.App.Data;
using SmartCRUD.App.Models;
using SmartCRUD.App.Utils;

namespace SmartCRUD.App.Forms
{
    public partial class UserForm : Form
    {
        private readonly UserRepository _repo = new();
        private int _selectedUserId = 0;

        public UserForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = _repo.GetAll();
                dgvUsers.DataSource = list;
                ConfigureGrid();
                lblRecordCount.Text = $"{list.Count} record{(list.Count == 1 ? "" : "s")} found";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            if (dgvUsers.Columns.Count == 0) return;
            if (dgvUsers.Columns.Contains("IsActive"))
                dgvUsers.Columns["IsActive"]!.Visible = false;
            if (dgvUsers.Columns.Contains("Password"))
                dgvUsers.Columns["Password"]!.Visible = false;
            if (dgvUsers.Columns.Contains("UserID"))
                dgvUsers.Columns["UserID"]!.Width = 60;
            if (dgvUsers.Columns.Contains("Username"))
                dgvUsers.Columns["Username"]!.Width = 160;
            if (dgvUsers.Columns.Contains("Role"))
                dgvUsers.Columns["Role"]!.Width = 100;
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;
            var row = dgvUsers.SelectedRows[0];
            _selectedUserId      = Convert.ToInt32(row.Cells["UserID"].Value);
            txtUserId.Text       = _selectedUserId.ToString();
            txtUsername.Text     = row.Cells["Username"].Value?.ToString() ?? "";
            txtPassword.Text     = "";
            cboRole.Text         = row.Cells["Role"].Value?.ToString() ?? "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(out UserModel user)) return;
            if (_repo.UsernameExists(user.Username))
            { ShowStatus("⚠ Username already exists.", true); return; }
            try
            {
                int id = _repo.Insert(user);
                ShowStatus($"✅ User added (ID: {id}).", false);
                ClearForm();
                LoadData();
            }
            catch (Exception ex) { ShowStatus($"❌ {ex.Message}", true); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0) { ShowStatus("⚠ Select a user first.", true); return; }
            if (!ValidateForm(out UserModel user)) return;
            if (_repo.UsernameExists(user.Username, _selectedUserId))
            { ShowStatus("⚠ Username already taken.", true); return; }
            user.UserID = _selectedUserId;
            try
            {
                _repo.Update(user);
                ShowStatus("✅ User updated successfully.", false);
                LoadData();
            }
            catch (Exception ex) { ShowStatus($"❌ {ex.Message}", true); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0) { ShowStatus("⚠ Select a user first.", true); return; }
            if (_selectedUserId == SessionManager.CurrentUser?.UserID)
            { ShowStatus("⚠ Cannot delete your own account.", true); return; }
            var r = MessageBox.Show("Delete this user?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r != DialogResult.Yes) return;
            try
            {
                _repo.Delete(_selectedUserId);
                ShowStatus("✅ User deleted.", false);
                ClearForm();
                LoadData();
            }
            catch (Exception ex) { ShowStatus($"❌ {ex.Message}", true); }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPass.Checked ? '\0' : '●';
        }

        private bool ValidateForm(out UserModel user)
        {
            user = new UserModel();
            if (!Validator.IsNotEmpty(txtUsername.Text))
            { ShowStatus("⚠ Username is required.", true); return false; }
            if (!Validator.IsValidUsername(txtUsername.Text))
            { ShowStatus("⚠ Username: 3-50 chars, letters/digits/_ only.", true); return false; }
            if (!Validator.IsNotEmpty(txtPassword.Text))
            { ShowStatus("⚠ Password is required.", true); return false; }
            if (string.IsNullOrWhiteSpace(cboRole.Text))
            { ShowStatus("⚠ Select a role.", true); return false; }

            user.Username = txtUsername.Text.Trim();
            user.Password = txtPassword.Text;
            user.Role     = cboRole.Text;
            return true;
        }

        private void ClearForm()
        {
            _selectedUserId = 0;
            txtUserId.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cboRole.SelectedIndex = -1;
            lblStatus.Text = string.Empty;
            dgvUsers.ClearSelection();
        }

        private void ShowStatus(string msg, bool isError)
        {
            lblStatus.Text      = msg;
            lblStatus.ForeColor = isError ? AppColors.Danger : AppColors.Success;
        }
    }
}
