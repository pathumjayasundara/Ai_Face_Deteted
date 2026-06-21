using SmartCRUD.App.Utils;

namespace SmartCRUD.App.Forms
{
    partial class StudentForm
    {
        private System.ComponentModel.IContainer components = null;

        // Top bar
        private Panel  pnlTopBar;
        private Label  lblTitle;

        // Search bar
        private Panel   pnlSearch;
        private TextBox txtSearch;
        private Button  btnSearch;
        private Button  btnClearSearch;
        private Label   lblRecordCount;

        // Left form panel
        private Panel   pnlFormOuter;
        private Label   lblFormTitle;
        private Label   lblStudentIdLbl;
        private TextBox txtStudentId;
        private Label   lblFullName;
        private TextBox txtFullName;
        private Label   lblAge;
        private TextBox txtAge;
        private Label   lblGender;
        private ComboBox cboGender;
        private Label   lblPhone;
        private TextBox txtPhone;
        private Label   lblEmail;
        private TextBox txtEmail;
        private Label   lblAddress;
        private TextBox txtAddress;
        private Button  btnAdd;
        private Button  btnUpdate;
        private Button  btnDelete;
        private Button  btnClear;
        private Label   lblStatus;

        // Right grid panel
        private Panel        pnlGrid;
        private DataGridView dgvStudents;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components    = new System.ComponentModel.Container();
            this.pnlTopBar     = new Panel();
            this.lblTitle      = new Label();
            this.pnlSearch     = new Panel();
            this.txtSearch     = new TextBox();
            this.btnSearch     = new Button();
            this.btnClearSearch = new Button();
            this.lblRecordCount = new Label();
            this.pnlFormOuter  = new Panel();
            this.lblFormTitle  = new Label();
            this.lblStudentIdLbl = new Label();
            this.txtStudentId  = new TextBox();
            this.lblFullName   = new Label();
            this.txtFullName   = new TextBox();
            this.lblAge        = new Label();
            this.txtAge        = new TextBox();
            this.lblGender     = new Label();
            this.cboGender     = new ComboBox();
            this.lblPhone      = new Label();
            this.txtPhone      = new TextBox();
            this.lblEmail      = new Label();
            this.txtEmail      = new TextBox();
            this.lblAddress    = new Label();
            this.txtAddress    = new TextBox();
            this.btnAdd        = new Button();
            this.btnUpdate     = new Button();
            this.btnDelete     = new Button();
            this.btnClear      = new Button();
            this.lblStatus     = new Label();
            this.pnlGrid       = new Panel();
            this.dgvStudents   = new DataGridView();

            // ── FORM ────────────────────────────────────────────────────────
            this.Text            = "Student Management";
            this.Size            = new Size(1150, 700);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.BackColor       = AppColors.LightGray;
            this.Font            = new Font("Segoe UI", 9F);

            // ── TOP BAR ─────────────────────────────────────────────────────
            this.pnlTopBar.Dock      = DockStyle.Top;
            this.pnlTopBar.Height    = 56;
            this.pnlTopBar.BackColor = AppColors.NavyBlue;

            this.lblTitle.Text      = "👥  Student Management — CRUD";
            this.lblTitle.Font      = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Location  = new Point(18, 14);

            this.pnlTopBar.Controls.Add(this.lblTitle);

            // ── SEARCH BAR ──────────────────────────────────────────────────
            this.pnlSearch.Dock      = DockStyle.Top;
            this.pnlSearch.Height    = 54;
            this.pnlSearch.BackColor = AppColors.White;
            this.pnlSearch.Padding   = new Padding(12, 10, 12, 0);

            this.txtSearch.Location        = new Point(12, 12);
            this.txtSearch.Size            = new Size(280, 30);
            this.txtSearch.Font            = new Font("Segoe UI", 10F);
            this.txtSearch.BorderStyle     = BorderStyle.FixedSingle;
            this.txtSearch.PlaceholderText = "Search by name, phone, email or ID…";
            this.txtSearch.KeyDown        += new KeyEventHandler(this.txtSearch_KeyDown);

            this.btnSearch.Text      = "🔍 Search";
            this.btnSearch.Location  = new Point(302, 11);
            this.btnSearch.Size      = new Size(100, 32);
            this.btnSearch.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSearch.BackColor = AppColors.MidBlue;
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.Cursor    = Cursors.Hand;
            this.btnSearch.Click    += new EventHandler(this.btnSearch_Click);

            this.btnClearSearch.Text      = "✖ Clear";
            this.btnClearSearch.Location  = new Point(412, 11);
            this.btnClearSearch.Size      = new Size(80, 32);
            this.btnClearSearch.Font      = new Font("Segoe UI", 9F);
            this.btnClearSearch.BackColor = AppColors.Warning;
            this.btnClearSearch.ForeColor = Color.White;
            this.btnClearSearch.FlatStyle = FlatStyle.Flat;
            this.btnClearSearch.FlatAppearance.BorderSize = 0;
            this.btnClearSearch.Cursor    = Cursors.Hand;
            this.btnClearSearch.Click    += new EventHandler(this.btnClearSearch_Click);

            this.lblRecordCount.Text      = "0 records";
            this.lblRecordCount.Font      = new Font("Segoe UI", 9F);
            this.lblRecordCount.ForeColor = AppColors.TextMuted;
            this.lblRecordCount.AutoSize  = true;
            this.lblRecordCount.Location  = new Point(510, 17);

            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.btnClearSearch);
            this.pnlSearch.Controls.Add(this.lblRecordCount);

            // ── FORM PANEL (left) ───────────────────────────────────────────
            this.pnlFormOuter.Dock      = DockStyle.Left;
            this.pnlFormOuter.Width     = 330;
            this.pnlFormOuter.BackColor = AppColors.White;
            this.pnlFormOuter.AutoScroll = true;

            this.lblFormTitle.Text      = "Student Details";
            this.lblFormTitle.Font      = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblFormTitle.ForeColor = AppColors.NavyBlue;
            this.lblFormTitle.AutoSize  = true;
            this.lblFormTitle.Location  = new Point(16, 16);

            int y = 50;
            AddFormField(this.lblStudentIdLbl, "Student ID (auto)", this.txtStudentId, ref y);
            this.txtStudentId.ReadOnly  = true;
            this.txtStudentId.BackColor = AppColors.LightGray;

            AddFormField(this.lblFullName, "Full Name *", this.txtFullName, ref y);
            AddFormField(this.lblAge,      "Age *",       this.txtAge,      ref y);

            // Gender
            this.lblGender.Text      = "Gender *";
            this.lblGender.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblGender.ForeColor = AppColors.TextDark;
            this.lblGender.AutoSize  = true;
            this.lblGender.Location  = new Point(16, y);
            y += 22;

            this.cboGender.Location      = new Point(16, y);
            this.cboGender.Size          = new Size(290, 30);
            this.cboGender.Font          = new Font("Segoe UI", 10F);
            this.cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            this.cboGender.FlatStyle     = FlatStyle.Flat;
            y += 38;

            AddFormField(this.lblPhone,   "Phone *",   this.txtPhone,   ref y);
            AddFormField(this.lblEmail,   "Email",     this.txtEmail,   ref y);
            AddFormField(this.lblAddress, "Address *", this.txtAddress, ref y);
            this.txtAddress.Multiline = true;
            this.txtAddress.Height    = 64;
            y += 32;

            // Buttons
            this.btnAdd.Text      = "➕  Add Student";
            this.btnAdd.Location  = new Point(16, y);
            this.btnAdd.Size      = new Size(138, 40);
            StyleBtn(this.btnAdd, AppColors.Success);
            this.btnAdd.Click    += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text   = "✏  Update";
            this.btnUpdate.Location = new Point(162, y);
            this.btnUpdate.Size   = new Size(138, 40);
            StyleBtn(this.btnUpdate, AppColors.MidBlue);
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            y += 50;

            this.btnDelete.Text   = "🗑  Delete";
            this.btnDelete.Location = new Point(16, y);
            this.btnDelete.Size   = new Size(138, 40);
            StyleBtn(this.btnDelete, AppColors.Danger);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClear.Text    = "🔄  Clear";
            this.btnClear.Location = new Point(162, y);
            this.btnClear.Size    = new Size(138, 40);
            StyleBtn(this.btnClear, AppColors.Warning);
            this.btnClear.Click  += new EventHandler(this.btnClear_Click);
            y += 56;

            this.lblStatus.Location  = new Point(16, y);
            this.lblStatus.Size      = new Size(295, 52);
            this.lblStatus.Font      = new Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = AppColors.Success;
            this.lblStatus.TextAlign = ContentAlignment.TopLeft;

            this.pnlFormOuter.Controls.Add(this.lblFormTitle);
            this.pnlFormOuter.Controls.Add(this.lblStudentIdLbl);
            this.pnlFormOuter.Controls.Add(this.txtStudentId);
            this.pnlFormOuter.Controls.Add(this.lblFullName);
            this.pnlFormOuter.Controls.Add(this.txtFullName);
            this.pnlFormOuter.Controls.Add(this.lblAge);
            this.pnlFormOuter.Controls.Add(this.txtAge);
            this.pnlFormOuter.Controls.Add(this.lblGender);
            this.pnlFormOuter.Controls.Add(this.cboGender);
            this.pnlFormOuter.Controls.Add(this.lblPhone);
            this.pnlFormOuter.Controls.Add(this.txtPhone);
            this.pnlFormOuter.Controls.Add(this.lblEmail);
            this.pnlFormOuter.Controls.Add(this.txtEmail);
            this.pnlFormOuter.Controls.Add(this.lblAddress);
            this.pnlFormOuter.Controls.Add(this.txtAddress);
            this.pnlFormOuter.Controls.Add(this.btnAdd);
            this.pnlFormOuter.Controls.Add(this.btnUpdate);
            this.pnlFormOuter.Controls.Add(this.btnDelete);
            this.pnlFormOuter.Controls.Add(this.btnClear);
            this.pnlFormOuter.Controls.Add(this.lblStatus);

            // ── GRID PANEL (right) ──────────────────────────────────────────
            this.pnlGrid.Dock      = DockStyle.Fill;
            this.pnlGrid.BackColor = AppColors.LightGray;
            this.pnlGrid.Padding   = new Padding(10);

            this.dgvStudents.Dock                  = DockStyle.Fill;
            this.dgvStudents.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudents.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.ReadOnly              = true;
            this.dgvStudents.AllowUserToAddRows    = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.MultiSelect           = false;
            this.dgvStudents.BackgroundColor       = Color.White;
            this.dgvStudents.BorderStyle           = BorderStyle.None;
            this.dgvStudents.RowHeadersVisible     = false;
            this.dgvStudents.EnableHeadersVisualStyles = false;
            this.dgvStudents.ColumnHeadersDefaultCellStyle.BackColor  = AppColors.NavyBlue;
            this.dgvStudents.ColumnHeadersDefaultCellStyle.ForeColor  = Color.White;
            this.dgvStudents.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dgvStudents.ColumnHeadersDefaultCellStyle.Padding    = new Padding(8, 0, 0, 0);
            this.dgvStudents.ColumnHeadersHeight   = 38;
            this.dgvStudents.RowTemplate.Height    = 32;
            this.dgvStudents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);
            this.dgvStudents.DefaultCellStyle.Font                    = new Font("Segoe UI", 9F);
            this.dgvStudents.DefaultCellStyle.SelectionBackColor      = AppColors.MidBlue;
            this.dgvStudents.DefaultCellStyle.SelectionForeColor      = Color.White;
            this.dgvStudents.DefaultCellStyle.Padding                 = new Padding(6, 0, 0, 0);
            this.dgvStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStudents.GridColor       = Color.FromArgb(220, 225, 235);
            this.dgvStudents.SelectionChanged += new EventHandler(this.dgvStudents_SelectionChanged);

            this.pnlGrid.Controls.Add(this.dgvStudents);

            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlFormOuter);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlTopBar);
        }

        private static void AddFormField(Label lbl, string labelText, TextBox tb, ref int y)
        {
            lbl.Text      = labelText;
            lbl.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl.ForeColor = AppColors.TextDark;
            lbl.AutoSize  = true;
            lbl.Location  = new Point(16, y);
            y += 22;
            tb.Location    = new Point(16, y);
            tb.Size        = new Size(290, 30);
            tb.Font        = new Font("Segoe UI", 10F);
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.BackColor   = Color.White;
            y += 38;
        }

        private static void StyleBtn(Button btn, Color color)
        {
            btn.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor    = Cursors.Hand;
        }
    }
}
