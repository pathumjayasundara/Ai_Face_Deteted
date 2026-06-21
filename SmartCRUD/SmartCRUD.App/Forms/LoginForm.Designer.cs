namespace SmartCRUD.App.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private Panel     pnlHeader;
        private Label     lblAppTitle;
        private Label     lblAppSubtitle;
        private Panel     pnlCard;
        private Label     lblCardTitle;
        private Label     lblUsername;
        private TextBox   txtUsername;
        private Label     lblPassword;
        private TextBox   txtPassword;
        private CheckBox  chkShowPassword;
        private Button    btnLogin;
        private Label     lblError;
        private Label     lblVersion;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components     = new System.ComponentModel.Container();
            this.pnlHeader      = new Panel();
            this.lblAppTitle    = new Label();
            this.lblAppSubtitle = new Label();
            this.pnlCard        = new Panel();
            this.lblCardTitle   = new Label();
            this.lblUsername    = new Label();
            this.txtUsername    = new TextBox();
            this.lblPassword    = new Label();
            this.txtPassword    = new TextBox();
            this.chkShowPassword = new CheckBox();
            this.btnLogin       = new Button();
            this.lblError       = new Label();
            this.lblVersion     = new Label();

            // ── FORM ────────────────────────────────────────────────────────
            this.Text            = "SmartCRUD – Login";
            this.Size            = new Size(480, 580);
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.MinimizeBox     = true;
            this.BackColor       = Color.FromArgb(245, 246, 250);
            this.Font            = new Font("Segoe UI", 9F);

            // ── HEADER PANEL ────────────────────────────────────────────────
            this.pnlHeader.Dock      = DockStyle.Top;
            this.pnlHeader.Height    = 150;
            this.pnlHeader.BackColor = Color.FromArgb(15, 52, 96);
            this.pnlHeader.Padding   = new Padding(0);

            this.lblAppTitle.Text      = "SmartCRUD";
            this.lblAppTitle.Font      = new Font("Segoe UI", 26F, FontStyle.Bold);
            this.lblAppTitle.ForeColor = Color.White;
            this.lblAppTitle.AutoSize  = true;
            this.lblAppTitle.Location  = new Point(50, 35);

            this.lblAppSubtitle.Text      = "Smart Management System";
            this.lblAppSubtitle.Font      = new Font("Segoe UI", 11F);
            this.lblAppSubtitle.ForeColor = Color.FromArgb(189, 215, 238);
            this.lblAppSubtitle.AutoSize  = true;
            this.lblAppSubtitle.Location  = new Point(52, 90);

            this.pnlHeader.Controls.Add(this.lblAppTitle);
            this.pnlHeader.Controls.Add(this.lblAppSubtitle);

            // ── CARD PANEL ───────────────────────────────────────────────────
            this.pnlCard.BackColor  = Color.White;
            this.pnlCard.Size       = new Size(380, 340);
            this.pnlCard.Location   = new Point(50, 170);

            // Card title
            this.lblCardTitle.Text      = "Sign In to Your Account";
            this.lblCardTitle.Font      = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblCardTitle.ForeColor = Color.FromArgb(15, 52, 96);
            this.lblCardTitle.AutoSize  = true;
            this.lblCardTitle.Location  = new Point(20, 20);

            // Username label
            this.lblUsername.Text      = "Username";
            this.lblUsername.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblUsername.AutoSize  = true;
            this.lblUsername.Location  = new Point(20, 65);

            // Username textbox
            this.txtUsername.Location    = new Point(20, 85);
            this.txtUsername.Size        = new Size(340, 32);
            this.txtUsername.Font        = new Font("Segoe UI", 10F);
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;
            this.txtUsername.BackColor   = Color.FromArgb(245, 246, 250);
            this.txtUsername.Text        = "admin";
            this.txtUsername.KeyDown    += new KeyEventHandler(this.txtUsername_KeyDown);

            // Password label
            this.lblPassword.Text      = "Password";
            this.lblPassword.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblPassword.ForeColor = Color.FromArgb(44, 62, 80);
            this.lblPassword.AutoSize  = true;
            this.lblPassword.Location  = new Point(20, 130);

            // Password textbox
            this.txtPassword.Location     = new Point(20, 150);
            this.txtPassword.Size         = new Size(340, 32);
            this.txtPassword.Font         = new Font("Segoe UI", 10F);
            this.txtPassword.BorderStyle  = BorderStyle.FixedSingle;
            this.txtPassword.BackColor    = Color.FromArgb(245, 246, 250);
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Text         = "Admin@123";
            this.txtPassword.KeyDown     += new KeyEventHandler(this.txtPassword_KeyDown);

            // Show password checkbox
            this.chkShowPassword.Text      = "Show password";
            this.chkShowPassword.Location  = new Point(20, 192);
            this.chkShowPassword.AutoSize  = true;
            this.chkShowPassword.Font      = new Font("Segoe UI", 8.5F);
            this.chkShowPassword.ForeColor = Color.FromArgb(127, 140, 141);
            this.chkShowPassword.CheckedChanged += new EventHandler(this.chkShowPassword_CheckedChanged);

            // Login button
            this.btnLogin.Text      = "LOGIN";
            this.btnLogin.Location  = new Point(20, 225);
            this.btnLogin.Size      = new Size(340, 44);
            this.btnLogin.Font      = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnLogin.BackColor = Color.FromArgb(21, 96, 189);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Cursor    = Cursors.Hand;
            this.btnLogin.Click    += new EventHandler(this.btnLogin_Click);

            // Error label
            this.lblError.Location  = new Point(20, 280);
            this.lblError.Size      = new Size(340, 50);
            this.lblError.Font      = new Font("Segoe UI", 8.5F);
            this.lblError.ForeColor = Color.FromArgb(192, 57, 43);
            this.lblError.TextAlign = ContentAlignment.TopCenter;
            this.lblError.Visible   = false;

            this.pnlCard.Controls.Add(this.lblCardTitle);
            this.pnlCard.Controls.Add(this.lblUsername);
            this.pnlCard.Controls.Add(this.txtUsername);
            this.pnlCard.Controls.Add(this.lblPassword);
            this.pnlCard.Controls.Add(this.txtPassword);
            this.pnlCard.Controls.Add(this.chkShowPassword);
            this.pnlCard.Controls.Add(this.btnLogin);
            this.pnlCard.Controls.Add(this.lblError);

            // Version label
            this.lblVersion.Text      = "SmartCRUD v1.0  ·  .NET 8  ·  SQL Server";
            this.lblVersion.Font      = new Font("Segoe UI", 8F);
            this.lblVersion.ForeColor = Color.FromArgb(127, 140, 141);
            this.lblVersion.AutoSize  = true;
            this.lblVersion.Location  = new Point(118, 530);

            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlCard);
            this.Controls.Add(this.lblVersion);

            this.AcceptButton = this.btnLogin;
        }
    }
}
