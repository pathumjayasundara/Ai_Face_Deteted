using SmartCRUD.App.Data;
using SmartCRUD.App.Models;
using SmartCRUD.App.Utils;

namespace SmartCRUD.App.Forms
{
    public partial class StudentForm : Form
    {
        private readonly StudentRepository _repo = new();
        private int _selectedStudentId = 0;

        public StudentForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dgvStudents.DataSource = _repo.GetAll();
                ConfigureGrid();
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            if (dgvStudents.Columns.Count == 0) return;
            string[] hide = { "IsActive", "UpdatedAt" };
            foreach (var col in hide)
                if (dgvStudents.Columns.Contains(col))
                    dgvStudents.Columns[col]!.Visible = false;

            if (dgvStudents.Columns.Contains("StudentID"))
                dgvStudents.Columns["StudentID"]!.Width = 70;
            if (dgvStudents.Columns.Contains("FullName"))
                dgvStudents.Columns["FullName"]!.Width = 160;
            if (dgvStudents.Columns.Contains("Age"))
                dgvStudents.Columns["Age"]!.Width = 50;
            if (dgvStudents.Columns.Contains("Gender"))
                dgvStudents.Columns["Gender"]!.Width = 70;
            if (dgvStudents.Columns.Contains("Phone"))
                dgvStudents.Columns["Phone"]!.Width = 110;
            if (dgvStudents.Columns.Contains("Email"))
                dgvStudents.Columns["Email"]!.Width = 160;
        }

        private void UpdateRecordCount()
        {
            int count = dgvStudents.Rows.Count;
            lblRecordCount.Text = $"{count} record{(count == 1 ? "" : "s")} found";
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count == 0) return;
            var row = dgvStudents.SelectedRows[0];

            _selectedStudentId   = Convert.ToInt32(row.Cells["StudentID"].Value);
            txtFullName.Text     = row.Cells["FullName"].Value?.ToString() ?? "";
            txtAge.Text          = row.Cells["Age"].Value?.ToString() ?? "";
            cboGender.Text       = row.Cells["Gender"].Value?.ToString() ?? "";
            txtPhone.Text        = row.Cells["Phone"].Value?.ToString() ?? "";
            txtEmail.Text        = row.Cells["Email"].Value?.ToString() ?? "";
            txtAddress.Text      = row.Cells["Address"].Value?.ToString() ?? "";
            txtStudentId.Text    = _selectedStudentId.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(out StudentModel student)) return;
            try
            {
                int newId = _repo.Insert(student);
                ShowStatus($"✅ Student added successfully (ID: {newId})", false);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                ShowStatus($"❌ Error: {ex.Message}", true);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedStudentId == 0)
            {
                ShowStatus("⚠ Please select a student from the grid first.", true);
                return;
            }
            if (!ValidateForm(out StudentModel student)) return;
            student.StudentID = _selectedStudentId;
            try
            {
                _repo.Update(student);
                ShowStatus("✅ Student updated successfully.", false);
                LoadData();
            }
            catch (Exception ex)
            {
                ShowStatus($"❌ Error: {ex.Message}", true);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedStudentId == 0)
            {
                ShowStatus("⚠ Please select a student to delete.", true);
                return;
            }
            var confirm = MessageBox.Show(
                "Are you sure you want to delete this student?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;
            try
            {
                _repo.Delete(_selectedStudentId);
                ShowStatus("✅ Student deleted successfully.", false);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                ShowStatus($"❌ Error: {ex.Message}", true);
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void btnSearch_Click(object sender, EventArgs e) => DoSearch();

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) DoSearch();
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
            txtSearch.Focus();
        }

        private void DoSearch()
        {
            string kw = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(kw)) { LoadData(); return; }
            try
            {
                dgvStudents.DataSource = _repo.Search(kw);
                ConfigureGrid();
                UpdateRecordCount();
            }
            catch (Exception ex)
            {
                ShowStatus($"❌ Search error: {ex.Message}", true);
            }
        }

        private bool ValidateForm(out StudentModel student)
        {
            student = new StudentModel();
            lblStatus.Text = string.Empty;

            if (!Validator.IsNotEmpty(txtFullName.Text))
            { ShowStatus("⚠ Full Name is required.", true); txtFullName.Focus(); return false; }

            if (!Validator.IsValidAge(txtAge.Text, out int age))
            { ShowStatus("⚠ Age must be a valid number (1-120).", true); txtAge.Focus(); return false; }

            if (string.IsNullOrWhiteSpace(cboGender.Text))
            { ShowStatus("⚠ Please select a Gender.", true); cboGender.Focus(); return false; }

            if (!Validator.IsNotEmpty(txtPhone.Text))
            { ShowStatus("⚠ Phone is required.", true); txtPhone.Focus(); return false; }

            if (!Validator.IsValidPhone(txtPhone.Text))
            { ShowStatus("⚠ Phone format is invalid.", true); txtPhone.Focus(); return false; }

            if (!Validator.IsValidEmail(txtEmail.Text))
            { ShowStatus("⚠ Email format is invalid.", true); txtEmail.Focus(); return false; }

            if (!Validator.IsNotEmpty(txtAddress.Text))
            { ShowStatus("⚠ Address is required.", true); txtAddress.Focus(); return false; }

            student.FullName = txtFullName.Text.Trim();
            student.Age      = age;
            student.Gender   = cboGender.Text.Trim();
            student.Phone    = txtPhone.Text.Trim();
            student.Email    = txtEmail.Text.Trim();
            student.Address  = txtAddress.Text.Trim();
            return true;
        }

        private void ClearForm()
        {
            _selectedStudentId = 0;
            txtStudentId.Clear();
            txtFullName.Clear();
            txtAge.Clear();
            cboGender.SelectedIndex = -1;
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            lblStatus.Text = string.Empty;
            dgvStudents.ClearSelection();
            txtFullName.Focus();
        }

        private void ShowStatus(string msg, bool isError)
        {
            lblStatus.Text      = msg;
            lblStatus.ForeColor = isError ? AppColors.Danger : AppColors.Success;
        }
    }
}
