using SmartCRUD.App.Utils;

namespace SmartCRUD.App.Forms
{
    partial class UserForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel        pnlTopBar;
        private Label        lblTitle;
        private Panel        pnlSearch;
        private Label        lblRecordCount;
        private Panel        pnlFormOuter;
        private Label        lblFormTitle;
        private Label        lblUserIdLbl;
        private TextBox      txtUserId;
        private Label        lblUsername;
        private TextBox      txtUsername;
        private Label        lblPassword;
        private TextBox      txtPassword;
        private CheckBox     chkShowPass;
        private Label        lblRole;
        private ComboBox     cboRole;
        private Button       btnAdd;
        private Button       btnUpdate;
        private Button       btnDelete;
        private Button       btnClear;
        private Label        lblStatus;
        private Panel        pnlGrid;
        private DataGridView dgvUsers;

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
            this.lblRecordCount = new Label();
            this.pnlFormOuter  = new Panel();
            this.lblFormTitle  = new Label();
            this.lblUserIdLbl  = new Label();
            this.txtUserId     = new TextBox();
            this.lblUsername   = new Label();
            this.txtUsername   = new TextBox();
            this.lblPassword   = new Label();
            this.txtPassword   = new TextBox();
            this.chkShowPass   = new CheckBox();
            this.lblRole       = new Label();
            this.cboRole       = new ComboBox();
            this.btnAdd        = new Button();
            this.btnUpdate     = new Button();
            this.btnDelete     = new Button();
            this.btnClear      = new Button();
            this.lblStatus     = new Label();
            this.pnlGrid       = new Panel();
            this.dgvUsers      = new DataGridView();

            this.Text          = "User Management";
            this.Size          = new Size(1050, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = AppColors.LightGray;
            this.Font          = new Font("Segoe UI", 9F);

            // Top bar
            this.pnlTopBar.Dock      = DockStyle.Top;
            this.pnlTopBar.Height    = 56;
            this.pnlTopBar.BackColor = AppColors.NavyBlue;
            this.lblTitle.Text       = "🔐  User Management";
            this.lblTitle.Font       = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblTitle.ForeColor  = Color.White;
            this.lblTitle.AutoSize   = true;
            this.lblTitle.Location   = new Point(18, 14);
            this.pnlTopBar.Controls.Add(this.lblTitle);

            // Search bar (info bar)
            this.pnlSearch.Dock      = DockStyle.Top;
            this.pnlSearch.Height    = 44;
            this.pnlSearch.BackColor = AppColors.White;
            this.lblRecordCount.Text      = "0 records";
            this.lblRecordCount.Font      = new Font("Segoe UI", 9F);
            this.lblRecordCount.ForeColor = AppColors.TextMuted;
            this.lblRecordCount.AutoSize  = true;
            this.lblRecordCount.Location  = new Point(16, 13);
            this.pnlSearch.Controls.Add(this.lblRecordCount);

            // Form panel
            this.pnlFormOuter.Dock       = DockStyle.Left;
            this.pnlFormOuter.Width      = 310;
            this.pnlFormOuter.BackColor  = AppColors.White;
            this.pnlFormOuter.AutoScroll = true;

            this.lblFormTitle.Text      = "User Details";
            this.lblFormTitle.Font      = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblFormTitle.ForeColor = AppColors.NavyBlue;
            this.lblFormTitle.AutoSize  = true;
            this.lblFormTitle.Location  = new Point(16, 16);

            int y = 52;
            AddField(this.lblUserIdLbl, "User ID (auto)", this.txtUserId, ref y);
            this.txtUserId.ReadOnly  = true;
            this.txtUserId.BackColor = AppColors.LightGray;

            AddField(this.lblUsername, "Username *", this.txtUsername, ref y);

            AddField(this.lblPassword, "Password *", this.txtPassword, ref y);
            this.txtPassword.PasswordChar = '●';

            this.chkShowPass.Text      = "Show password";
            this.chkShowPass.Font      = new Font("Segoe UI", 8.5F);
            this.chkShowPass.ForeColor = AppColors.TextMuted;
            this.chkShowPass.AutoSize  = true;
            this.chkShowPass.Location  = new Point(16, y);
            this.chkShowPass.CheckedChanged += new EventHandler(this.chkShowPass_CheckedChanged);
            y += 30;

            this.lblRole.Text      = "Role *";
            this.lblRole.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRole.ForeColor = AppColors.TextDark;
            this.lblRole.AutoSize  = true;
            this.lblRole.Location  = new Point(16, y);
            y += 22;

            this.cboRole.Location      = new Point(16, y);
            this.cboRole.Size          = new Size(270, 30);
            this.cboRole.Font          = new Font("Segoe UI", 10F);
            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboRole.Items.AddRange(new object[] { "Admin", "Manager", "User" });
            this.cboRole.FlatStyle     = FlatStyle.Flat;
            y += 44;

            this.btnAdd.Text      = "➕  Add";
            this.btnAdd.Location  = new Point(16, y);
            this.btnAdd.Size      = new Size(125, 40);
            StyleBtn(this.btnAdd, AppColors.Success);
            this.btnAdd.Click    += new EventHandler(this.btnAdd_Click);

            this.btnUpdate.Text   = "✏  Update";
            this.btnUpdate.Location = new Point(148, y);
            this.btnUpdate.Size   = new Size(125, 40);
            StyleBtn(this.btnUpdate, AppColors.MidBlue);
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            y += 50;

            this.btnDelete.Text   = "🗑  Delete";
            this.btnDelete.Location = new Point(16, y);
            this.btnDelete.Size   = new Size(125, 40);
            StyleBtn(this.btnDelete, AppColors.Danger);
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            this.btnClear.Text    = "🔄  Clear";
            this.btnClear.Location = new Point(148, y);
            this.btnClear.Size    = new Size(125, 40);
            StyleBtn(this.btnClear, AppColors.Warning);
            this.btnClear.Click  += new EventHandler(this.btnClear_Click);
            y += 56;

            this.lblStatus.Location  = new Point(16, y);
            this.lblStatus.Size      = new Size(275, 52);
            this.lblStatus.Font      = new Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = AppColors.Success;

            this.pnlFormOuter.Controls.Add(this.lblFormTitle);
            this.pnlFormOuter.Controls.Add(this.lblUserIdLbl);
            this.pnlFormOuter.Controls.Add(this.txtUserId);
            this.pnlFormOuter.Controls.Add(this.lblUsername);
            this.pnlFormOuter.Controls.Add(this.txtUsername);
            this.pnlFormOuter.Controls.Add(this.lblPassword);
            this.pnlFormOuter.Controls.Add(this.txtPassword);
            this.pnlFormOuter.Controls.Add(this.chkShowPass);
            this.pnlFormOuter.Controls.Add(this.lblRole);
            this.pnlFormOuter.Controls.Add(this.cboRole);
            this.pnlFormOuter.Controls.Add(this.btnAdd);
            this.pnlFormOuter.Controls.Add(this.btnUpdate);
            this.pnlFormOuter.Controls.Add(this.btnDelete);
            this.pnlFormOuter.Controls.Add(this.btnClear);
            this.pnlFormOuter.Controls.Add(this.lblStatus);

            // Grid panel
            this.pnlGrid.Dock      = DockStyle.Fill;
            this.pnlGrid.BackColor = AppColors.LightGray;
            this.pnlGrid.Padding   = new Padding(10);

            this.dgvUsers.Dock                  = DockStyle.Fill;
            this.dgvUsers.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.ReadOnly              = true;
            this.dgvUsers.AllowUserToAddRows    = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.MultiSelect           = false;
            this.dgvUsers.BackgroundColor       = Color.White;
            this.dgvUsers.BorderStyle           = BorderStyle.None;
            this.dgvUsers.RowHeadersVisible     = false;
            this.dgvUsers.EnableHeadersVisualStyles = false;
            this.dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = AppColors.NavyBlue;
            this.dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvUsers.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dgvUsers.ColumnHeadersHeight   = 38;
            this.dgvUsers.RowTemplate.Height    = 32;
            this.dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);
            this.dgvUsers.DefaultCellStyle.Font                    = new Font("Segoe UI", 9F);
            this.dgvUsers.DefaultCellStyle.SelectionBackColor      = AppColors.MidBlue;
            this.dgvUsers.DefaultCellStyle.SelectionForeColor      = Color.White;
            this.dgvUsers.DefaultCellStyle.Padding                 = new Padding(6, 0, 0, 0);
            this.dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUsers.GridColor       = Color.FromArgb(220, 225, 235);
            this.dgvUsers.SelectionChanged += new EventHandler(this.dgvUsers_SelectionChanged);

            this.pnlGrid.Controls.Add(this.dgvUsers);

            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlFormOuter);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlTopBar);
        }

        private static void AddField(Label lbl, string text, TextBox tb, ref int y)
        {
            lbl.Text      = text;
            lbl.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl.ForeColor = AppColors.TextDark;
            lbl.AutoSize  = true;
            lbl.Location  = new Point(16, y);
            y += 22;
            tb.Location    = new Point(16, y);
            tb.Size        = new Size(270, 30);
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
