namespace FaceAttendanceSystem
{
    partial class MainDashboard
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
            headerPanel = new Panel();
            logoPictureBox = new PictureBox();
            titleLabel = new Label();
            subTitleLabel = new Label();
            logoutButton = new Button();
            navPanel = new TableLayoutPanel();
            homeButton = new Button();
            registerButton = new Button();
            attendanceButton = new Button();
            recordsButton = new Button();
            adminButton = new Button();
            chatbotButton = new Button();
            subjectsButton = new Button();
            timetableButton = new Button();
            panelContainer = new Panel();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            navPanel.SuspendLayout();
            SuspendLayout();
            //
            // headerPanel
            //
            headerPanel.BackColor = Color.FromArgb(141, 21, 58);
            headerPanel.Controls.Add(logoutButton);
            headerPanel.Controls.Add(subTitleLabel);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Controls.Add(logoPictureBox);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 70;
            headerPanel.Name = "headerPanel";
            //
            // logoPictureBox
            //
            logoPictureBox.Location = new Point(14, 8);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(54, 54);
            logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoPictureBox.TabStop = false;
            //
            // titleLabel
            //
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(80, 12);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "Sabaragamuwa University Face Attendance System";
            //
            // subTitleLabel
            //
            subTitleLabel.AutoSize = true;
            subTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            subTitleLabel.ForeColor = Color.FromArgb(230, 230, 230);
            subTitleLabel.Location = new Point(80, 40);
            subTitleLabel.Name = "subTitleLabel";
            subTitleLabel.Text = "Department of Physical Science and Technology";
            //
            // logoutButton
            //
            logoutButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            logoutButton.Cursor = Cursors.Hand;
            logoutButton.FlatStyle = FlatStyle.Flat;
            logoutButton.FlatAppearance.BorderColor = Color.FromArgb(212, 160, 23);
            logoutButton.FlatAppearance.BorderSize = 1;
            logoutButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            logoutButton.ForeColor = Color.White;
            logoutButton.Location = new Point(1150, 18);
            logoutButton.Name = "logoutButton";
            logoutButton.Size = new Size(110, 34);
            logoutButton.Text = "⏻ Logout";
            logoutButton.UseVisualStyleBackColor = false;
            //
            // navPanel
            //
            navPanel.BackColor = Color.White;
            navPanel.ColumnCount = 8;
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            navPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
            navPanel.RowCount = 1;
            navPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            navPanel.Controls.Add(homeButton, 0, 0);
            navPanel.Controls.Add(registerButton, 1, 0);
            navPanel.Controls.Add(subjectsButton, 2, 0);
            navPanel.Controls.Add(timetableButton, 3, 0);
            navPanel.Controls.Add(attendanceButton, 4, 0);
            navPanel.Controls.Add(recordsButton, 5, 0);
            navPanel.Controls.Add(chatbotButton, 6, 0);
            navPanel.Controls.Add(adminButton, 7, 0);
            navPanel.Dock = DockStyle.Top;
            navPanel.Height = 55;
            navPanel.Name = "navPanel";
            //
            // homeButton
            //
            homeButton.Cursor = Cursors.Hand;
            homeButton.Dock = DockStyle.Fill;
            homeButton.FlatStyle = FlatStyle.Flat;
            homeButton.FlatAppearance.BorderSize = 0;
            homeButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            homeButton.ForeColor = Color.FromArgb(141, 21, 58);
            homeButton.Name = "homeButton";
            homeButton.Text = "Home";
            homeButton.UseVisualStyleBackColor = true;
            homeButton.Click += homeButton_Click;
            //
            // registerButton
            //
            registerButton.Cursor = Cursors.Hand;
            registerButton.Dock = DockStyle.Fill;
            registerButton.FlatStyle = FlatStyle.Flat;
            registerButton.FlatAppearance.BorderSize = 0;
            registerButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            registerButton.ForeColor = Color.FromArgb(141, 21, 58);
            registerButton.Name = "registerButton";
            registerButton.Text = "Register Student";
            registerButton.UseVisualStyleBackColor = true;
            registerButton.Click += registerButton_Click;
            //
            // subjectsButton
            //
            subjectsButton.Cursor = Cursors.Hand;
            subjectsButton.Dock = DockStyle.Fill;
            subjectsButton.FlatStyle = FlatStyle.Flat;
            subjectsButton.FlatAppearance.BorderSize = 0;
            subjectsButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            subjectsButton.ForeColor = Color.FromArgb(141, 21, 58);
            subjectsButton.Name = "subjectsButton";
            subjectsButton.Text = "Manage Subjects";
            subjectsButton.UseVisualStyleBackColor = true;
            subjectsButton.Click += subjectsButton_Click;
            //
            // timetableButton
            //
            timetableButton.Cursor = Cursors.Hand;
            timetableButton.Dock = DockStyle.Fill;
            timetableButton.FlatStyle = FlatStyle.Flat;
            timetableButton.FlatAppearance.BorderSize = 0;
            timetableButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            timetableButton.ForeColor = Color.FromArgb(141, 21, 58);
            timetableButton.Name = "timetableButton";
            timetableButton.Text = "Timetable";
            timetableButton.UseVisualStyleBackColor = true;
            timetableButton.Click += timetableButton_Click;
            //
            // attendanceButton
            //
            attendanceButton.Cursor = Cursors.Hand;
            attendanceButton.Dock = DockStyle.Fill;
            attendanceButton.FlatStyle = FlatStyle.Flat;
            attendanceButton.FlatAppearance.BorderSize = 0;
            attendanceButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            attendanceButton.ForeColor = Color.FromArgb(141, 21, 58);
            attendanceButton.Name = "attendanceButton";
            attendanceButton.Text = "Take Attendance";
            attendanceButton.UseVisualStyleBackColor = true;
            attendanceButton.Click += attendanceButton_Click;
            //
            // recordsButton
            //
            recordsButton.Cursor = Cursors.Hand;
            recordsButton.Dock = DockStyle.Fill;
            recordsButton.FlatStyle = FlatStyle.Flat;
            recordsButton.FlatAppearance.BorderSize = 0;
            recordsButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            recordsButton.ForeColor = Color.FromArgb(141, 21, 58);
            recordsButton.Name = "recordsButton";
            recordsButton.Text = "Attendance Records";
            recordsButton.UseVisualStyleBackColor = true;
            recordsButton.Click += recordsButton_Click;
            //
            // chatbotButton
            //
            chatbotButton.Cursor = Cursors.Hand;
            chatbotButton.Dock = DockStyle.Fill;
            chatbotButton.FlatStyle = FlatStyle.Flat;
            chatbotButton.FlatAppearance.BorderSize = 0;
            chatbotButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            chatbotButton.ForeColor = Color.FromArgb(141, 21, 58);
            chatbotButton.Name = "chatbotButton";
            chatbotButton.Text = "🤖 AI Assistant";
            chatbotButton.UseVisualStyleBackColor = true;
            chatbotButton.Click += chatbotButton_Click;
            //
            // adminButton
            //
            adminButton.Cursor = Cursors.Hand;
            adminButton.Dock = DockStyle.Fill;
            adminButton.FlatStyle = FlatStyle.Flat;
            adminButton.FlatAppearance.BorderSize = 0;
            adminButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            adminButton.ForeColor = Color.FromArgb(141, 21, 58);
            adminButton.Name = "adminButton";
            adminButton.Text = "🛡 Admin Panel";
            adminButton.UseVisualStyleBackColor = true;
            adminButton.Click += adminButton_Click;
            //
            // panelContainer
            //
            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Name = "panelContainer";
            //
            // MainDashboard
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 800);
            MinimumSize = new Size(1024, 700);
            Controls.Add(panelContainer);
            Controls.Add(navPanel);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.Sizable;
            Name = "MainDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sabaragamuwa University Face Attendance System";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            navPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel headerPanel;
        private PictureBox logoPictureBox;
        private Label titleLabel;
        private Label subTitleLabel;
        private Button logoutButton;
        private TableLayoutPanel navPanel;
        private Button homeButton;
        private Button registerButton;
        private Button attendanceButton;
        private Button recordsButton;
        private Button adminButton;
        private Button chatbotButton;
        private Button subjectsButton;
        private Button timetableButton;
        private Panel panelContainer;
    }
}
