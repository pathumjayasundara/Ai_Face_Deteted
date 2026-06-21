using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartCampusAI.Data;
using SmartCampusAI.Models;
using SmartCampusAI.Styles;
using SmartCampusAI.Utils;

namespace SmartCampusAI.Forms
{
    public class UserForm : Form
    {
        private DataGridView _dgv = new();
        private TextBox _txtId       = new();
        private TextBox _txtUsername = new();
        private TextBox _txtPassword = new();
        private ComboBox _cboRole    = new();
        private Button _btnAdd       = new();
        private Button _btnUpdate    = new();
        private Button _btnDelete    = new();
        private Button _btnClear     = new();
        private Label _lblStatus     = new();
        private int _selectedId      = 0;

        private readonly UserRepository _repo = new();

        public UserForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text          = "User Management";
            this.Size          = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = ColorPalette.LightGray;

            // Top bar
            Panel topBar = new() { Dock = DockStyle.Top, Height = 50, BackColor = ColorPalette.NavyBlue };
            Label lbl = new() { Text = "👥 User Management", Font = new Font("Segoe UI", 13, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(15, 12) };
            topBar.Controls.Add(lbl);

            // Form panel
            Panel fp = new() { Width = 300, Dock = DockStyle.Left, BackColor = Color.White, Padding = new Padding(20) };

            int y = 20;
            Label lblTitle = new() { Text = "User Details", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = ColorPalette.NavyBlue, AutoSize = true, Location = new Point(20, y) }; y += 38;

            Label l1 = MakeLabel("User ID (auto)", new Point(20, y)); y += 22;
            _txtId.Location = new Point(20, y); _txtId.Size = new Size(255, 28); _txtId.Font = Theme.InputFont; _txtId.BorderStyle = BorderStyle.FixedSingle; _txtId.ReadOnly = true; _txtId.BackColor = ColorPalette.LightGray; y += 38;

            Label l2 = MakeLabel("Username *", new Point(20, y)); y += 22;
            _txtUsername.Location = new Point(20, y); _txtUsername.Size = new Size(255, 28); _txtUsername.Font = Theme.InputFont; _txtUsername.BorderStyle = BorderStyle.FixedSingle; y += 38;

            Label l3 = MakeLabel("Password *", new Point(20, y)); y += 22;
            _txtPassword.Location = new Point(20, y); _txtPassword.Size = new Size(255, 28); _txtPassword.Font = Theme.InputFont; _txtPassword.BorderStyle = BorderStyle.FixedSingle; _txtPassword.PasswordChar = '●'; y += 38;

            Label l4 = MakeLabel("Role *", new Point(20, y)); y += 22;
            _cboRole.Location = new Point(20, y); _cboRole.Size = new Size(255, 28); _cboRole.Font = Theme.InputFont; _cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboRole.Items.AddRange(new object[] { "Admin", "User" }); _cboRole.SelectedIndex = 1; y += 42;

            _btnAdd.Text = "➕ Add"; _btnAdd.Location = new Point(20, y); _btnAdd.Size = new Size(118, 36); Theme.StyleSuccessButton(_btnAdd); _btnAdd.Click += BtnAdd_Click;
            _btnUpdate.Text = "✏ Update"; _btnUpdate.Location = new Point(148, y); _btnUpdate.Size = new Size(118, 36); Theme.StylePrimaryButton(_btnUpdate); _btnUpdate.Click += BtnUpdate_Click; y += 46;

            _btnDelete.Text = "🗑 Delete"; _btnDelete.Location = new Point(20, y); _btnDelete.Size = new Size(118, 36); Theme.StyleDangerButton(_btnDelete); _btnDelete.Click += BtnDelete_Click;
            _btnClear.Text = "🔄 Clear"; _btnClear.Location = new Point(148, y); _btnClear.Size = new Size(118, 36); Theme.StyleWarningButton(_btnClear); _btnClear.Click += (s, e) => ClearForm(); y += 50;

            _lblStatus.Location = new Point(20, y); _lblStatus.Size = new Size(255, 36); _lblStatus.Font = new Font("Segoe UI", 9); _lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            fp.Controls.AddRange(new Control[] { lblTitle, l1, _txtId, l2, _txtUsername, l3, _txtPassword, l4, _cboRole, _btnAdd, _btnUpdate, _btnDelete, _btnClear, _lblStatus });

            // Grid
            Panel gp = new() { Dock = DockStyle.Fill, Padding = new Padding(8), BackColor = ColorPalette.LightGray };
            _dgv.Dock = DockStyle.Fill;
            Theme.StyleDataGrid(_dgv);
            _dgv.SelectionChanged += DgvSelectionChanged;
            gp.Controls.Add(_dgv);

            this.Controls.Add(gp);
            this.Controls.Add(fp);
            this.Controls.Add(topBar);
        }

        private static Label MakeLabel(string text, Point loc) =>
            new() { Text = text, Font = Theme.LabelFont, AutoSize = true, Location = loc };

        private void LoadData()
        {
            try { _dgv.DataSource = _repo.GetAll(); HideSystemCols(); }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void HideSystemCols()
        {
            if (_dgv.Columns.Contains("CreatedAt")) _dgv.Columns["CreatedAt"]!.Visible = false;
            if (_dgv.Columns.Contains("Password"))  _dgv.Columns["Password"]!.Visible  = false;
        }

        private void DgvSelectionChanged(object? sender, EventArgs e)
        {
            if (_dgv.SelectedRows.Count == 0) return;
            var row       = _dgv.SelectedRows[0];
            _selectedId       = Convert.ToInt32(row.Cells["UserID"].Value);
            _txtId.Text       = _selectedId.ToString();
            _txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
            _txtPassword.Text = "";
            _cboRole.Text     = row.Cells["Role"].Value?.ToString() ?? "";
        }

        private bool ValidateForm(out User u)
        {
            u = new User();
            if (!Validators.IsNotEmpty(_txtUsername.Text)) { ShowStatus("Username required.", true); return false; }
            if (!Validators.IsValidUsername(_txtUsername.Text)) { ShowStatus("Username: letters, digits, _ or . only (3-50 chars).", true); return false; }
            if (!Validators.IsNotEmpty(_txtPassword.Text)) { ShowStatus("Password required.", true); return false; }
            u.UserID   = _selectedId;
            u.Username = _txtUsername.Text.Trim();
            u.Password = _txtPassword.Text;
            u.Role     = _cboRole.Text;
            return true;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateForm(out User u)) return;
            if (_repo.UsernameExists(u.Username)) { ShowStatus("Username already taken.", true); return; }
            try { _repo.Insert(u); ShowStatus("✅ User added.", false); ClearForm(); LoadData(); }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (_selectedId == 0) { ShowStatus("Select a user first.", true); return; }
            if (!ValidateForm(out User u)) return;
            if (_repo.UsernameExists(u.Username, _selectedId)) { ShowStatus("Username already taken.", true); return; }
            try { _repo.Update(u); ShowStatus("✅ User updated.", false); LoadData(); }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedId == 0) { ShowStatus("Select a user first.", true); return; }
            if (SessionManager.CurrentUser?.UserID == _selectedId) { ShowStatus("Cannot delete your own account.", true); return; }
            if (!Helpers.Confirm("Delete this user?")) return;
            try { _repo.Delete(_selectedId); ShowStatus("✅ User deleted.", false); ClearForm(); LoadData(); }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void ClearForm()
        {
            _selectedId = 0;
            _txtId.Clear(); _txtUsername.Clear(); _txtPassword.Clear();
            _cboRole.SelectedIndex = 1;
            _lblStatus.Text = string.Empty;
            _dgv.ClearSelection();
        }

        private void ShowStatus(string msg, bool err)
        {
            _lblStatus.Text      = msg;
            _lblStatus.ForeColor = err ? ColorPalette.Danger : ColorPalette.Success;
        }
    }
}
