namespace FaceAttendanceSystem
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            backgroundPanel = new FaceAttendanceSystem.Controls.DoubleBufferedPanel();
            centeringLayout = new TableLayoutPanel();
            cardPanel = new Panel();
            mainLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            logoPictureBox = new PictureBox();
            uniNameLabel = new Label();
            facultyNameLabel = new Label();
            deptNameLabel = new Label();
            tabPanel = new Panel();
            loginTabButton = new Button();
            registerTabButton = new Button();
            contentPanel = new Panel();
            loginPanel = new Panel();
            loginUsernameLabel = new Label();
            loginUsernameTextBox = new TextBox();
            loginPasswordLabel = new Label();
            loginPasswordTextBox = new TextBox();
            loginButton = new Button();
            changePasswordLinkLabel = new LinkLabel();
            registerPanel = new Panel();
            regFullNameLabel = new Label();
            regFullNameTextBox = new TextBox();
            regPositionLabel = new Label();
            regPositionComboBox = new ComboBox();
            regLecturerIdLabel = new Label();
            regLecturerIdTextBox = new TextBox();
            regEmailLabel = new Label();
            regEmailTextBox = new TextBox();
            regUsernameLabel = new Label();
            regUsernameTextBox = new TextBox();
            regPasswordLabel = new Label();
            regPasswordTextBox = new TextBox();
            regConfirmPasswordLabel = new Label();
            regConfirmPasswordTextBox = new TextBox();
            registerButton = new Button();
            messageLabel = new Label();
            backgroundPanel.SuspendLayout();
            centeringLayout.SuspendLayout();
            cardPanel.SuspendLayout();
            mainLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            tabPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            loginPanel.SuspendLayout();
            registerPanel.SuspendLayout();
            SuspendLayout();
            //
            // backgroundPanel
            //
            backgroundPanel.BackColor = Color.FromArgb(90, 12, 38);
            backgroundPanel.Controls.Add(centeringLayout);
            backgroundPanel.Dock = DockStyle.Fill;
            backgroundPanel.Name = "backgroundPanel";
            //
            // centeringLayout
            //
            centeringLayout.BackColor = Color.Transparent;
            centeringLayout.ColumnCount = 1;
            centeringLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            centeringLayout.RowCount = 1;
            centeringLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            centeringLayout.Controls.Add(cardPanel, 0, 0);
            centeringLayout.Dock = DockStyle.Fill;
            centeringLayout.Name = "centeringLayout";
            //
            // cardPanel
            //
            cardPanel.Anchor = AnchorStyles.None;
            cardPanel.BackColor = Color.White;
            cardPanel.Controls.Add(mainLayout);
            cardPanel.Name = "cardPanel";
            cardPanel.Size = new Size(700, 640);
            //
            // mainLayout
            //
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 4;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(tabPanel, 0, 1);
            mainLayout.Controls.Add(contentPanel, 0, 2);
            mainLayout.Controls.Add(messageLabel, 0, 3);
            mainLayout.Name = "mainLayout";
            //
            // headerPanel
            //
            headerPanel.BackColor = Color.White;
            headerPanel.Controls.Add(deptNameLabel);
            headerPanel.Controls.Add(facultyNameLabel);
            headerPanel.Controls.Add(uniNameLabel);
            headerPanel.Controls.Add(logoPictureBox);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Name = "headerPanel";
            //
            // logoPictureBox
            //
            logoPictureBox.Location = new Point(30, 15);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(110, 120);
            logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoPictureBox.TabStop = false;
            //
            // uniNameLabel
            //
            uniNameLabel.AutoSize = true;
            uniNameLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            uniNameLabel.ForeColor = Color.FromArgb(141, 21, 58);
            uniNameLabel.Location = new Point(160, 20);
            uniNameLabel.MaximumSize = new Size(500, 0);
            uniNameLabel.Name = "uniNameLabel";
            uniNameLabel.Text = "Sabaragamuwa University of Sri Lanka";
            //
            // facultyNameLabel
            //
            facultyNameLabel.AutoSize = true;
            facultyNameLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            facultyNameLabel.ForeColor = Color.FromArgb(212, 160, 23);
            facultyNameLabel.Location = new Point(160, 65);
            facultyNameLabel.Name = "facultyNameLabel";
            facultyNameLabel.Text = "Faculty of Applied Sciences";
            //
            // deptNameLabel
            //
            deptNameLabel.AutoSize = true;
            deptNameLabel.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            deptNameLabel.ForeColor = Color.DimGray;
            deptNameLabel.Location = new Point(160, 95);
            deptNameLabel.MaximumSize = new Size(500, 0);
            deptNameLabel.Name = "deptNameLabel";
            deptNameLabel.Text = "Department of Physical Science and Technology\r\nAttendance System";
            //
            // tabPanel
            //
            tabPanel.Controls.Add(registerTabButton);
            tabPanel.Controls.Add(loginTabButton);
            tabPanel.Dock = DockStyle.Fill;
            tabPanel.Name = "tabPanel";
            tabPanel.Padding = new Padding(30, 5, 30, 5);
            //
            // loginTabButton
            //
            loginTabButton.Dock = DockStyle.Left;
            loginTabButton.FlatStyle = FlatStyle.Flat;
            loginTabButton.FlatAppearance.BorderSize = 0;
            loginTabButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            loginTabButton.Name = "loginTabButton";
            loginTabButton.Text = "Login";
            loginTabButton.UseVisualStyleBackColor = false;
            loginTabButton.Width = 200;
            //
            // registerTabButton
            //
            registerTabButton.Dock = DockStyle.Left;
            registerTabButton.FlatStyle = FlatStyle.Flat;
            registerTabButton.FlatAppearance.BorderSize = 0;
            registerTabButton.Name = "registerTabButton";
            registerTabButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            registerTabButton.Text = "Register";
            registerTabButton.UseVisualStyleBackColor = false;
            registerTabButton.Width = 200;
            //
            // contentPanel
            //
            contentPanel.Controls.Add(registerPanel);
            contentPanel.Controls.Add(loginPanel);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Name = "contentPanel";
            //
            // loginPanel
            //
            loginPanel.Controls.Add(loginButton);
            loginPanel.Controls.Add(changePasswordLinkLabel);
            loginPanel.Controls.Add(loginPasswordTextBox);
            loginPanel.Controls.Add(loginPasswordLabel);
            loginPanel.Controls.Add(loginUsernameTextBox);
            loginPanel.Controls.Add(loginUsernameLabel);
            loginPanel.Dock = DockStyle.Fill;
            loginPanel.Name = "loginPanel";
            //
            // loginUsernameLabel
            //
            loginUsernameLabel.AutoSize = true;
            loginUsernameLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            loginUsernameLabel.ForeColor = Color.FromArgb(141, 21, 58);
            loginUsernameLabel.Location = new Point(60, 40);
            loginUsernameLabel.Name = "loginUsernameLabel";
            loginUsernameLabel.Text = "Username";
            //
            // loginUsernameTextBox
            //
            loginUsernameTextBox.Font = new Font("Segoe UI", 12F);
            loginUsernameTextBox.Location = new Point(60, 65);
            loginUsernameTextBox.Name = "loginUsernameTextBox";
            loginUsernameTextBox.Size = new Size(340, 30);
            //
            // loginPasswordLabel
            //
            loginPasswordLabel.AutoSize = true;
            loginPasswordLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            loginPasswordLabel.ForeColor = Color.FromArgb(141, 21, 58);
            loginPasswordLabel.Location = new Point(60, 115);
            loginPasswordLabel.Name = "loginPasswordLabel";
            loginPasswordLabel.Text = "Password";
            //
            // loginPasswordTextBox
            //
            loginPasswordTextBox.Font = new Font("Segoe UI", 12F);
            loginPasswordTextBox.Location = new Point(60, 140);
            loginPasswordTextBox.Name = "loginPasswordTextBox";
            loginPasswordTextBox.PasswordChar = '●';
            loginPasswordTextBox.Size = new Size(340, 30);
            //
            // loginButton
            //
            loginButton.BackColor = Color.FromArgb(141, 21, 58);
            loginButton.Cursor = Cursors.Hand;
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.FlatAppearance.BorderSize = 0;
            loginButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            loginButton.ForeColor = Color.White;
            loginButton.Location = new Point(60, 195);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(340, 44);
            loginButton.Text = "🔓 Login";
            loginButton.UseVisualStyleBackColor = false;
            //
            // changePasswordLinkLabel
            //
            changePasswordLinkLabel.AutoSize = true;
            changePasswordLinkLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            changePasswordLinkLabel.LinkColor = Color.FromArgb(141, 21, 58);
            changePasswordLinkLabel.Location = new Point(60, 250);
            changePasswordLinkLabel.Name = "changePasswordLinkLabel";
            changePasswordLinkLabel.Size = new Size(120, 17);
            changePasswordLinkLabel.TabStop = true;
            changePasswordLinkLabel.Text = "Change Password?";
            //
            // registerPanel
            //
            registerPanel.AutoScroll = true;
            registerPanel.Controls.Add(registerButton);
            registerPanel.Controls.Add(regConfirmPasswordTextBox);
            registerPanel.Controls.Add(regConfirmPasswordLabel);
            registerPanel.Controls.Add(regPasswordTextBox);
            registerPanel.Controls.Add(regPasswordLabel);
            registerPanel.Controls.Add(regUsernameTextBox);
            registerPanel.Controls.Add(regUsernameLabel);
            registerPanel.Controls.Add(regEmailTextBox);
            registerPanel.Controls.Add(regEmailLabel);
            registerPanel.Controls.Add(regLecturerIdTextBox);
            registerPanel.Controls.Add(regLecturerIdLabel);
            registerPanel.Controls.Add(regPositionComboBox);
            registerPanel.Controls.Add(regPositionLabel);
            registerPanel.Controls.Add(regFullNameTextBox);
            registerPanel.Controls.Add(regFullNameLabel);
            registerPanel.Dock = DockStyle.Fill;
            registerPanel.Name = "registerPanel";
            //
            // regFullNameLabel
            //
            regFullNameLabel.AutoSize = true;
            regFullNameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            regFullNameLabel.ForeColor = Color.FromArgb(141, 21, 58);
            regFullNameLabel.Location = new Point(60, 15);
            regFullNameLabel.Name = "regFullNameLabel";
            regFullNameLabel.Text = "Lecturer Name";
            //
            // regFullNameTextBox
            //
            regFullNameTextBox.Font = new Font("Segoe UI", 10F);
            regFullNameTextBox.Location = new Point(60, 38);
            regFullNameTextBox.Name = "regFullNameTextBox";
            regFullNameTextBox.Size = new Size(330, 27);
            //
            // regPositionLabel
            //
            regPositionLabel.AutoSize = true;
            regPositionLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            regPositionLabel.ForeColor = Color.FromArgb(141, 21, 58);
            regPositionLabel.Location = new Point(410, 15);
            regPositionLabel.Name = "regPositionLabel";
            regPositionLabel.Text = "Lecturer Position";
            //
            // regPositionTextBox -> regPositionComboBox
            //
            regPositionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            regPositionComboBox.Font = new Font("Segoe UI", 10F);
            regPositionComboBox.Items.AddRange(new object[] {
                "Temporary Assistant Lecturer",
                "Lecturer (Probationary)",
                "Lecturer",
                "Lecturer (Visiting)",
                "Senior Lecturer Grade II",
                "Senior Lecturer Grade I",
                "Associate Professor",
                "Professor",
                "Senior Professor",
                "Chair Professor"
            });
            regPositionComboBox.Location = new Point(410, 38);
            regPositionComboBox.Name = "regPositionComboBox";
            regPositionComboBox.Size = new Size(230, 29);
            //
            // regLecturerIdLabel
            //
            regLecturerIdLabel.AutoSize = true;
            regLecturerIdLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            regLecturerIdLabel.ForeColor = Color.FromArgb(141, 21, 58);
            regLecturerIdLabel.Location = new Point(60, 75);
            regLecturerIdLabel.Name = "regLecturerIdLabel";
            regLecturerIdLabel.Text = "Lecturer ID Number";
            //
            // regLecturerIdTextBox
            //
            regLecturerIdTextBox.Font = new Font("Segoe UI", 10F);
            regLecturerIdTextBox.Location = new Point(60, 98);
            regLecturerIdTextBox.Name = "regLecturerIdTextBox";
            regLecturerIdTextBox.Size = new Size(330, 27);
            //
            // regEmailLabel
            //
            regEmailLabel.AutoSize = true;
            regEmailLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            regEmailLabel.ForeColor = Color.FromArgb(141, 21, 58);
            regEmailLabel.Location = new Point(410, 75);
            regEmailLabel.Name = "regEmailLabel";
            regEmailLabel.Text = "Official Email (@appsc.sab.ac.lk)";
            //
            // regEmailTextBox
            //
            regEmailTextBox.Font = new Font("Segoe UI", 10F);
            regEmailTextBox.Location = new Point(410, 98);
            regEmailTextBox.Name = "regEmailTextBox";
            regEmailTextBox.PlaceholderText = "e.g. kamal@appsc.sab.ac.lk";
            regEmailTextBox.Size = new Size(230, 27);
            //
            // regUsernameLabel
            //
            regUsernameLabel.AutoSize = true;
            regUsernameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            regUsernameLabel.ForeColor = Color.FromArgb(141, 21, 58);
            regUsernameLabel.Location = new Point(60, 135);
            regUsernameLabel.Name = "regUsernameLabel";
            regUsernameLabel.Text = "Username (simple, e.g. your name)";
            //
            // regUsernameTextBox
            //
            regUsernameTextBox.Font = new Font("Segoe UI", 10F);
            regUsernameTextBox.Location = new Point(60, 158);
            regUsernameTextBox.Name = "regUsernameTextBox";
            regUsernameTextBox.Size = new Size(330, 27);
            //
            // regPasswordLabel
            //
            regPasswordLabel.AutoSize = true;
            regPasswordLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            regPasswordLabel.ForeColor = Color.FromArgb(141, 21, 58);
            regPasswordLabel.Location = new Point(410, 135);
            regPasswordLabel.Name = "regPasswordLabel";
            regPasswordLabel.Text = "Password";
            //
            // regPasswordTextBox
            //
            regPasswordTextBox.Font = new Font("Segoe UI", 10F);
            regPasswordTextBox.Location = new Point(410, 158);
            regPasswordTextBox.Name = "regPasswordTextBox";
            regPasswordTextBox.PasswordChar = '●';
            regPasswordTextBox.Size = new Size(230, 27);
            //
            // regConfirmPasswordLabel
            //
            regConfirmPasswordLabel.AutoSize = true;
            regConfirmPasswordLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            regConfirmPasswordLabel.ForeColor = Color.FromArgb(141, 21, 58);
            regConfirmPasswordLabel.Location = new Point(60, 195);
            regConfirmPasswordLabel.Name = "regConfirmPasswordLabel";
            regConfirmPasswordLabel.Text = "Confirm Password";
            //
            // regConfirmPasswordTextBox
            //
            regConfirmPasswordTextBox.Font = new Font("Segoe UI", 10F);
            regConfirmPasswordTextBox.Location = new Point(60, 218);
            regConfirmPasswordTextBox.Name = "regConfirmPasswordTextBox";
            regConfirmPasswordTextBox.PasswordChar = '●';
            regConfirmPasswordTextBox.Size = new Size(330, 27);
            //
            // registerButton
            //
            registerButton.BackColor = Color.FromArgb(212, 160, 23);
            registerButton.Cursor = Cursors.Hand;
            registerButton.FlatStyle = FlatStyle.Flat;
            registerButton.FlatAppearance.BorderSize = 0;
            registerButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            registerButton.ForeColor = Color.White;
            registerButton.Location = new Point(410, 218);
            registerButton.Name = "registerButton";
            registerButton.Size = new Size(230, 42);
            registerButton.Text = "📝 Create Account";
            registerButton.UseVisualStyleBackColor = false;
            //
            // messageLabel
            //
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            messageLabel.Name = "messageLabel";
            messageLabel.Text = "";
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // LoginForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 750);
            Controls.Add(backgroundPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Attendance System - Login";
            backgroundPanel.ResumeLayout(false);
            centeringLayout.ResumeLayout(false);
            cardPanel.ResumeLayout(false);
            mainLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            tabPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            loginPanel.ResumeLayout(false);
            loginPanel.PerformLayout();
            registerPanel.ResumeLayout(false);
            registerPanel.PerformLayout();
            ResumeLayout(false);
        }

        private FaceAttendanceSystem.Controls.DoubleBufferedPanel backgroundPanel;
        private TableLayoutPanel centeringLayout;
        private Panel cardPanel;
        private TableLayoutPanel mainLayout;
        private Panel headerPanel;
        private PictureBox logoPictureBox;
        private Label uniNameLabel;
        private Label facultyNameLabel;
        private Label deptNameLabel;
        private Panel tabPanel;
        private Button loginTabButton;
        private Button registerTabButton;
        private Panel contentPanel;
        private Panel loginPanel;
        private Label loginUsernameLabel;
        private TextBox loginUsernameTextBox;
        private Label loginPasswordLabel;
        private TextBox loginPasswordTextBox;
        private Button loginButton;
        private LinkLabel changePasswordLinkLabel;
        private Panel registerPanel;
        private Label regFullNameLabel;
        private TextBox regFullNameTextBox;
        private Label regPositionLabel;
        private ComboBox regPositionComboBox;
        private Label regLecturerIdLabel;
        private TextBox regLecturerIdTextBox;
        private Label regEmailLabel;
        private TextBox regEmailTextBox;
        private Label regUsernameLabel;
        private TextBox regUsernameTextBox;
        private Label regPasswordLabel;
        private TextBox regPasswordTextBox;
        private Label regConfirmPasswordLabel;
        private TextBox regConfirmPasswordTextBox;
        private Button registerButton;
        private Label messageLabel;
    }
}
