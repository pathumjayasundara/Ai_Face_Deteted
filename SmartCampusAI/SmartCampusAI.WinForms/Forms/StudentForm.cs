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
    public class StudentForm : Form
    {
        private DataGridView _dgv = new();
        private TextBox _txtSearch = new();
        private Button _btnSearch = new();
        private Button _btnClear = new();

        private TextBox _txtId = new();
        private TextBox _txtName = new();
        private TextBox _txtAge = new();
        private ComboBox _cboGender = new();
        private TextBox _txtPhone = new();
        private TextBox _txtAddress = new();

        private Button _btnAdd = new();
        private Button _btnUpdate = new();
        private Button _btnDelete = new();
        private Button _btnNewForm = new();

        private Label _lblStatus = new();

        private readonly StudentRepository _repo = new();
        private int _selectedId = 0;

        public StudentForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text            = "Manage Students";
            this.Size            = new Size(1100, 680);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.BackColor       = ColorPalette.LightGray;

            // ── TOP BAR ────────────────────────────────────────────────────────
            Panel topBar = new()
            {
                Dock      = DockStyle.Top,
                Height    = 55,
                BackColor = ColorPalette.NavyBlue,
                Padding   = new Padding(10, 10, 10, 0)
            };

            Label lblTitle = new()
            {
                Text      = "📋 Student Management",
                Font      = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize  = true,
                Location  = new Point(15, 14)
            };

            _txtSearch.Location    = new Point(350, 14);
            _txtSearch.Size        = new Size(220, 28);
            _txtSearch.Font        = Theme.InputFont;
            _txtSearch.BorderStyle = BorderStyle.FixedSingle;
            _txtSearch.PlaceholderText = "Search by name or ID…";

            _btnSearch.Text     = "🔍 Search";
            _btnSearch.Location = new Point(580, 13);
            _btnSearch.Size     = new Size(90, 30);
            Theme.StylePrimaryButton(_btnSearch);
            _btnSearch.Click   += (s, e) => SearchData();

            _btnClear.Text     = "✖ Clear";
            _btnClear.Location = new Point(678, 13);
            _btnClear.Size     = new Size(75, 30);
            Theme.StyleWarningButton(_btnClear);
            _btnClear.Click   += (s, e) => { _txtSearch.Clear(); LoadData(); };

            topBar.Controls.AddRange(new Control[] { lblTitle, _txtSearch, _btnSearch, _btnClear });

            // ── LEFT: FORM PANEL ───────────────────────────────────────────────
            Panel formPanel = new()
            {
                Width     = 320,
                Dock      = DockStyle.Left,
                BackColor = Color.White,
                Padding   = new Padding(20)
            };

            int fy = 20;
            Label lblForm = new()
            {
                Text      = "Student Details",
                Font      = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.NavyBlue,
                AutoSize  = true,
                Location  = new Point(20, fy)
            };
            fy += 40;

            // ID (read-only)
            AddFormRow(formPanel, "Student ID", _txtId, ref fy);
            _txtId.ReadOnly  = true;
            _txtId.BackColor = ColorPalette.LightGray;

            AddFormRow(formPanel, "Full Name *", _txtName, ref fy);

            AddFormRow(formPanel, "Age *", _txtAge, ref fy);

            // Gender
            Label lblGender = new() { Text = "Gender *", Font = Theme.LabelFont, AutoSize = true, Location = new Point(20, fy) };
            fy += 22;
            _cboGender.Location  = new Point(20, fy);
            _cboGender.Size      = new Size(270, 28);
            _cboGender.Font      = Theme.InputFont;
            _cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            _cboGender.SelectedIndex = 0;
            fy += 38;

            AddFormRow(formPanel, "Phone *", _txtPhone, ref fy);
            AddFormRow(formPanel, "Address *", _txtAddress, ref fy);
            _txtAddress.Multiline = true;
            _txtAddress.Height    = 60;

            fy += 30;

            _btnAdd.Text     = "➕ Add Student";
            _btnAdd.Location = new Point(20, fy);
            _btnAdd.Size     = new Size(128, 38);
            Theme.StyleSuccessButton(_btnAdd);
            _btnAdd.Click   += BtnAdd_Click;

            _btnUpdate.Text     = "✏ Update";
            _btnUpdate.Location = new Point(158, fy);
            _btnUpdate.Size     = new Size(128, 38);
            Theme.StylePrimaryButton(_btnUpdate);
            _btnUpdate.Click   += BtnUpdate_Click;
            fy += 50;

            _btnDelete.Text     = "🗑 Delete";
            _btnDelete.Location = new Point(20, fy);
            _btnDelete.Size     = new Size(128, 38);
            Theme.StyleDangerButton(_btnDelete);
            _btnDelete.Click   += BtnDelete_Click;

            _btnNewForm.Text     = "🔄 Clear Form";
            _btnNewForm.Location = new Point(158, fy);
            _btnNewForm.Size     = new Size(128, 38);
            Theme.StyleWarningButton(_btnNewForm);
            _btnNewForm.Click   += (s, e) => ClearForm();
            fy += 60;

            _lblStatus.Location  = new Point(20, fy);
            _lblStatus.Size      = new Size(270, 40);
            _lblStatus.Font      = new Font("Segoe UI", 9);
            _lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            formPanel.Controls.AddRange(new Control[]
            {
                lblForm, lblGender, _cboGender,
                _btnAdd, _btnUpdate, _btnDelete, _btnNewForm, _lblStatus
            });

            // ── RIGHT: DATA GRID ───────────────────────────────────────────────
            Panel gridPanel = new()
            {
                Dock      = DockStyle.Fill,
                Padding   = new Padding(10),
                BackColor = ColorPalette.LightGray
            };

            _dgv.Dock = DockStyle.Fill;
            Theme.StyleDataGrid(_dgv);
            _dgv.SelectionChanged += DgvSelectionChanged;

            gridPanel.Controls.Add(_dgv);

            this.Controls.Add(gridPanel);
            this.Controls.Add(formPanel);
            this.Controls.Add(topBar);
        }

        private static void AddFormRow(Panel parent, string label, TextBox tb, ref int y)
        {
            Label lbl = new() { Text = label, Font = Theme.LabelFont, AutoSize = true, Location = new Point(20, y) };
            y += 22;
            tb.Location    = new Point(20, y);
            tb.Size        = new Size(270, 28);
            tb.Font        = Theme.InputFont;
            tb.BorderStyle = BorderStyle.FixedSingle;
            y += 38;
            parent.Controls.Add(lbl);
            parent.Controls.Add(tb);
        }

        private void LoadData()
        {
            try
            {
                List<Student> list = _repo.GetAll();
                _dgv.DataSource = list;
                HideSystemColumns();
            }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void SearchData()
        {
            string kw = _txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(kw)) { LoadData(); return; }
            try
            {
                _dgv.DataSource = _repo.Search(kw);
                HideSystemColumns();
            }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void HideSystemColumns()
        {
            if (_dgv.Columns.Contains("CreatedAt"))
                _dgv.Columns["CreatedAt"]!.Visible = false;
        }

        private void DgvSelectionChanged(object? sender, EventArgs e)
        {
            if (_dgv.SelectedRows.Count == 0) return;
            DataGridViewRow row = _dgv.SelectedRows[0];
            _selectedId = Convert.ToInt32(row.Cells["StudentID"].Value);
            _txtId.Text      = _selectedId.ToString();
            _txtName.Text    = row.Cells["FullName"].Value?.ToString() ?? "";
            _txtAge.Text     = row.Cells["Age"].Value?.ToString() ?? "";
            _cboGender.Text  = row.Cells["Gender"].Value?.ToString() ?? "";
            _txtPhone.Text   = row.Cells["Phone"].Value?.ToString() ?? "";
            _txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
        }

        private bool ValidateForm(out Student s)
        {
            s = new Student();
            if (!Validators.IsNotEmpty(_txtName.Text))   { ShowStatus("Full Name is required.", true); return false; }
            if (!Validators.IsValidAge(_txtAge.Text, out int age)) { ShowStatus("Age must be a valid number.", true); return false; }
            if (!Validators.IsNotEmpty(_txtPhone.Text))  { ShowStatus("Phone is required.", true); return false; }
            if (!Validators.IsValidPhone(_txtPhone.Text)){ ShowStatus("Invalid phone format.", true); return false; }
            if (!Validators.IsNotEmpty(_txtAddress.Text)){ ShowStatus("Address is required.", true); return false; }

            s.StudentID = _selectedId;
            s.FullName  = _txtName.Text.Trim();
            s.Age       = age;
            s.Gender    = _cboGender.Text;
            s.Phone     = _txtPhone.Text.Trim();
            s.Address   = _txtAddress.Text.Trim();
            return true;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateForm(out Student s)) return;
            try
            {
                int newId = _repo.Insert(s);
                ShowStatus($"✅ Student added (ID: {newId})", false);
                ClearForm();
                LoadData();
            }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            if (_selectedId == 0) { ShowStatus("Select a student first.", true); return; }
            if (!ValidateForm(out Student s)) return;
            try
            {
                _repo.Update(s);
                ShowStatus("✅ Student updated.", false);
                LoadData();
            }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedId == 0) { ShowStatus("Select a student first.", true); return; }
            if (!Helpers.Confirm("Delete this student and all related data?")) return;
            try
            {
                _repo.Delete(_selectedId);
                ShowStatus("✅ Student deleted.", false);
                ClearForm();
                LoadData();
            }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void ClearForm()
        {
            _selectedId       = 0;
            _txtId.Clear();
            _txtName.Clear();
            _txtAge.Clear();
            _cboGender.SelectedIndex = 0;
            _txtPhone.Clear();
            _txtAddress.Clear();
            _lblStatus.Text = string.Empty;
            _dgv.ClearSelection();
        }

        private void ShowStatus(string msg, bool isError)
        {
            _lblStatus.Text      = msg;
            _lblStatus.ForeColor = isError ? ColorPalette.Danger : ColorPalette.Success;
        }
    }
}
