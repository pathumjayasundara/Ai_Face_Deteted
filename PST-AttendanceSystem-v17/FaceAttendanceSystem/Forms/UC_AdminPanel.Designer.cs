namespace FaceAttendanceSystem.Forms
{
    partial class UC_AdminPanel
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
            mainLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            refreshAllButton = new Button();
            titleLabel = new Label();
            tabControl = new TabControl();
            lecturersTabPage = new TabPage();
            lecturersGrid = new DataGridView();
            lecturerToolPanel = new Panel();
            deleteLecturerButton = new Button();
            lecturerCountLabel = new Label();
            studentsTabPage = new TabPage();
            studentsGrid = new DataGridView();
            studentToolPanel = new Panel();
            deleteStudentButton = new Button();
            studentCountLabel = new Label();
            attendanceTabPage = new TabPage();
            attendanceGrid = new DataGridView();
            attendanceToolPanel = new Panel();
            deleteAttendanceButton = new Button();
            attendanceCountLabel = new Label();
            emailTabPage = new TabPage();
            smtpHostLabel = new Label();
            smtpHostTextBox = new TextBox();
            smtpPortLabel = new Label();
            smtpPortNumeric = new NumericUpDown();
            useSslCheckBox = new CheckBox();
            senderEmailLabel = new Label();
            senderEmailTextBox = new TextBox();
            senderPasswordLabel = new Label();
            senderPasswordTextBox = new TextBox();
            senderDisplayNameLabel = new Label();
            senderDisplayNameTextBox = new TextBox();
            saveEmailSettingsButton = new Button();
            testEmailButton = new Button();
            testEmailToTextBox = new TextBox();
            testEmailToLabel = new Label();
            emailStatusLabel = new Label();
            mainLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            tabControl.SuspendLayout();
            lecturersTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lecturersGrid).BeginInit();
            lecturerToolPanel.SuspendLayout();
            studentsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)studentsGrid).BeginInit();
            studentToolPanel.SuspendLayout();
            attendanceTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)attendanceGrid).BeginInit();
            attendanceToolPanel.SuspendLayout();
            emailTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)smtpPortNumeric).BeginInit();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(245, 245, 245);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(20);
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(tabControl, 0, 1);
            mainLayout.Name = "mainLayout";
            //
            // headerPanel
            //
            headerPanel.Controls.Add(refreshAllButton);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Name = "headerPanel";
            //
            // titleLabel
            //
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(141, 21, 58);
            titleLabel.Location = new Point(0, 12);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "🛡 Administrator Panel";
            //
            // refreshAllButton
            //
            refreshAllButton.BackColor = Color.FromArgb(212, 160, 23);
            refreshAllButton.Cursor = Cursors.Hand;
            refreshAllButton.FlatStyle = FlatStyle.Flat;
            refreshAllButton.FlatAppearance.BorderSize = 0;
            refreshAllButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            refreshAllButton.ForeColor = Color.White;
            refreshAllButton.Location = new Point(1000, 8);
            refreshAllButton.Name = "refreshAllButton";
            refreshAllButton.Size = new Size(120, 32);
            refreshAllButton.Text = "🔄 Refresh All";
            refreshAllButton.UseVisualStyleBackColor = false;
            //
            // tabControl
            //
            tabControl.Controls.Add(lecturersTabPage);
            tabControl.Controls.Add(studentsTabPage);
            tabControl.Controls.Add(attendanceTabPage);
            tabControl.Controls.Add(emailTabPage);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            //
            // lecturersTabPage
            //
            lecturersTabPage.Controls.Add(lecturersGrid);
            lecturersTabPage.Controls.Add(lecturerToolPanel);
            lecturersTabPage.Name = "lecturersTabPage";
            lecturersTabPage.Padding = new Padding(10);
            lecturersTabPage.Text = "Lecturer Records";
            lecturersTabPage.UseVisualStyleBackColor = true;
            //
            // lecturerToolPanel
            //
            lecturerToolPanel.Controls.Add(deleteLecturerButton);
            lecturerToolPanel.Controls.Add(lecturerCountLabel);
            lecturerToolPanel.Dock = DockStyle.Top;
            lecturerToolPanel.Height = 45;
            lecturerToolPanel.Name = "lecturerToolPanel";
            //
            // lecturerCountLabel
            //
            lecturerCountLabel.AutoSize = true;
            lecturerCountLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lecturerCountLabel.ForeColor = Color.Gray;
            lecturerCountLabel.Location = new Point(0, 14);
            lecturerCountLabel.Name = "lecturerCountLabel";
            lecturerCountLabel.Text = "0 lecturer account(s)";
            //
            // deleteLecturerButton
            //
            deleteLecturerButton.BackColor = Color.Firebrick;
            deleteLecturerButton.Cursor = Cursors.Hand;
            deleteLecturerButton.FlatStyle = FlatStyle.Flat;
            deleteLecturerButton.FlatAppearance.BorderSize = 0;
            deleteLecturerButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            deleteLecturerButton.ForeColor = Color.White;
            deleteLecturerButton.Location = new Point(950, 8);
            deleteLecturerButton.Name = "deleteLecturerButton";
            deleteLecturerButton.Size = new Size(150, 30);
            deleteLecturerButton.Text = "🗑 Delete Selected";
            deleteLecturerButton.UseVisualStyleBackColor = false;
            //
            // lecturersGrid
            //
            lecturersGrid.AllowUserToAddRows = false;
            lecturersGrid.AllowUserToDeleteRows = false;
            lecturersGrid.BackgroundColor = Color.White;
            lecturersGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(141, 21, 58);
            lecturersGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            lecturersGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lecturersGrid.Dock = DockStyle.Fill;
            lecturersGrid.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lecturersGrid.Name = "lecturersGrid";
            lecturersGrid.ReadOnly = true;
            lecturersGrid.RowHeadersVisible = false;
            lecturersGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lecturersGrid.MultiSelect = false;
            lecturersGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // studentsTabPage
            //
            studentsTabPage.Controls.Add(studentsGrid);
            studentsTabPage.Controls.Add(studentToolPanel);
            studentsTabPage.Name = "studentsTabPage";
            studentsTabPage.Padding = new Padding(10);
            studentsTabPage.Text = "Students";
            studentsTabPage.UseVisualStyleBackColor = true;
            //
            // studentToolPanel
            //
            studentToolPanel.Controls.Add(deleteStudentButton);
            studentToolPanel.Controls.Add(studentCountLabel);
            studentToolPanel.Dock = DockStyle.Top;
            studentToolPanel.Height = 45;
            studentToolPanel.Name = "studentToolPanel";
            //
            // studentCountLabel
            //
            studentCountLabel.AutoSize = true;
            studentCountLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            studentCountLabel.ForeColor = Color.Gray;
            studentCountLabel.Location = new Point(0, 14);
            studentCountLabel.Name = "studentCountLabel";
            studentCountLabel.Text = "0 registered student(s)";
            //
            // deleteStudentButton
            //
            deleteStudentButton.BackColor = Color.Firebrick;
            deleteStudentButton.Cursor = Cursors.Hand;
            deleteStudentButton.FlatStyle = FlatStyle.Flat;
            deleteStudentButton.FlatAppearance.BorderSize = 0;
            deleteStudentButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            deleteStudentButton.ForeColor = Color.White;
            deleteStudentButton.Location = new Point(950, 8);
            deleteStudentButton.Name = "deleteStudentButton";
            deleteStudentButton.Size = new Size(150, 30);
            deleteStudentButton.Text = "🗑 Delete Selected";
            deleteStudentButton.UseVisualStyleBackColor = false;
            //
            // studentsGrid
            //
            studentsGrid.AllowUserToAddRows = false;
            studentsGrid.AllowUserToDeleteRows = false;
            studentsGrid.BackgroundColor = Color.White;
            studentsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(141, 21, 58);
            studentsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            studentsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            studentsGrid.Dock = DockStyle.Fill;
            studentsGrid.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            studentsGrid.Name = "studentsGrid";
            studentsGrid.ReadOnly = true;
            studentsGrid.RowHeadersVisible = false;
            studentsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            studentsGrid.MultiSelect = false;
            studentsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // attendanceTabPage
            //
            attendanceTabPage.Controls.Add(attendanceGrid);
            attendanceTabPage.Controls.Add(attendanceToolPanel);
            attendanceTabPage.Name = "attendanceTabPage";
            attendanceTabPage.Padding = new Padding(10);
            attendanceTabPage.Text = "Attendance Records";
            attendanceTabPage.UseVisualStyleBackColor = true;
            //
            // attendanceToolPanel
            //
            attendanceToolPanel.Controls.Add(deleteAttendanceButton);
            attendanceToolPanel.Controls.Add(attendanceCountLabel);
            attendanceToolPanel.Dock = DockStyle.Top;
            attendanceToolPanel.Height = 45;
            attendanceToolPanel.Name = "attendanceToolPanel";
            //
            // attendanceCountLabel
            //
            attendanceCountLabel.AutoSize = true;
            attendanceCountLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            attendanceCountLabel.ForeColor = Color.Gray;
            attendanceCountLabel.Location = new Point(0, 14);
            attendanceCountLabel.Name = "attendanceCountLabel";
            attendanceCountLabel.Text = "0 attendance record(s)";
            //
            // deleteAttendanceButton
            //
            deleteAttendanceButton.BackColor = Color.Firebrick;
            deleteAttendanceButton.Cursor = Cursors.Hand;
            deleteAttendanceButton.FlatStyle = FlatStyle.Flat;
            deleteAttendanceButton.FlatAppearance.BorderSize = 0;
            deleteAttendanceButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            deleteAttendanceButton.ForeColor = Color.White;
            deleteAttendanceButton.Location = new Point(950, 8);
            deleteAttendanceButton.Name = "deleteAttendanceButton";
            deleteAttendanceButton.Size = new Size(150, 30);
            deleteAttendanceButton.Text = "🗑 Delete Selected";
            deleteAttendanceButton.UseVisualStyleBackColor = false;
            //
            // attendanceGrid
            //
            attendanceGrid.AllowUserToAddRows = false;
            attendanceGrid.AllowUserToDeleteRows = false;
            attendanceGrid.BackgroundColor = Color.White;
            attendanceGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(141, 21, 58);
            attendanceGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            attendanceGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            attendanceGrid.Dock = DockStyle.Fill;
            attendanceGrid.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            attendanceGrid.Name = "attendanceGrid";
            attendanceGrid.ReadOnly = true;
            attendanceGrid.RowHeadersVisible = false;
            attendanceGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            attendanceGrid.MultiSelect = false;
            attendanceGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // emailTabPage
            //
            emailTabPage.Controls.Add(emailStatusLabel);
            emailTabPage.Controls.Add(testEmailButton);
            emailTabPage.Controls.Add(testEmailToTextBox);
            emailTabPage.Controls.Add(testEmailToLabel);
            emailTabPage.Controls.Add(saveEmailSettingsButton);
            emailTabPage.Controls.Add(senderDisplayNameTextBox);
            emailTabPage.Controls.Add(senderDisplayNameLabel);
            emailTabPage.Controls.Add(senderPasswordTextBox);
            emailTabPage.Controls.Add(senderPasswordLabel);
            emailTabPage.Controls.Add(senderEmailTextBox);
            emailTabPage.Controls.Add(senderEmailLabel);
            emailTabPage.Controls.Add(useSslCheckBox);
            emailTabPage.Controls.Add(smtpPortNumeric);
            emailTabPage.Controls.Add(smtpPortLabel);
            emailTabPage.Controls.Add(smtpHostTextBox);
            emailTabPage.Controls.Add(smtpHostLabel);
            emailTabPage.Name = "emailTabPage";
            emailTabPage.Padding = new Padding(20);
            emailTabPage.Text = "Email Settings";
            emailTabPage.UseVisualStyleBackColor = true;
            //
            // smtpHostLabel
            //
            smtpHostLabel.AutoSize = true;
            smtpHostLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            smtpHostLabel.ForeColor = Color.FromArgb(141, 21, 58);
            smtpHostLabel.Location = new Point(3, 10);
            smtpHostLabel.Name = "smtpHostLabel";
            smtpHostLabel.Text = "SMTP Host";
            //
            // smtpHostTextBox
            //
            smtpHostTextBox.Font = new Font("Segoe UI", 10F);
            smtpHostTextBox.Location = new Point(3, 33);
            smtpHostTextBox.Name = "smtpHostTextBox";
            smtpHostTextBox.Size = new Size(260, 27);
            //
            // smtpPortLabel
            //
            smtpPortLabel.AutoSize = true;
            smtpPortLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            smtpPortLabel.ForeColor = Color.FromArgb(141, 21, 58);
            smtpPortLabel.Location = new Point(280, 10);
            smtpPortLabel.Name = "smtpPortLabel";
            smtpPortLabel.Text = "SMTP Port";
            //
            // smtpPortNumeric
            //
            smtpPortNumeric.Font = new Font("Segoe UI", 10F);
            smtpPortNumeric.Location = new Point(280, 33);
            smtpPortNumeric.Maximum = 65535;
            smtpPortNumeric.Minimum = 1;
            smtpPortNumeric.Name = "smtpPortNumeric";
            smtpPortNumeric.Size = new Size(100, 27);
            smtpPortNumeric.Value = 587;
            //
            // useSslCheckBox
            //
            useSslCheckBox.AutoSize = true;
            useSslCheckBox.Checked = true;
            useSslCheckBox.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            useSslCheckBox.ForeColor = Color.FromArgb(141, 21, 58);
            useSslCheckBox.Location = new Point(400, 36);
            useSslCheckBox.Name = "useSslCheckBox";
            useSslCheckBox.Text = "Use SSL/TLS";
            useSslCheckBox.UseVisualStyleBackColor = true;
            //
            // senderEmailLabel
            //
            senderEmailLabel.AutoSize = true;
            senderEmailLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            senderEmailLabel.ForeColor = Color.FromArgb(141, 21, 58);
            senderEmailLabel.Location = new Point(3, 75);
            senderEmailLabel.Name = "senderEmailLabel";
            senderEmailLabel.Text = "Sender Email (e.g. Gmail address)";
            //
            // senderEmailTextBox
            //
            senderEmailTextBox.Font = new Font("Segoe UI", 10F);
            senderEmailTextBox.Location = new Point(3, 98);
            senderEmailTextBox.Name = "senderEmailTextBox";
            senderEmailTextBox.Size = new Size(320, 27);
            //
            // senderPasswordLabel
            //
            senderPasswordLabel.AutoSize = true;
            senderPasswordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            senderPasswordLabel.ForeColor = Color.FromArgb(141, 21, 58);
            senderPasswordLabel.Location = new Point(340, 75);
            senderPasswordLabel.Name = "senderPasswordLabel";
            senderPasswordLabel.Text = "Sender Password (Gmail App Password)";
            //
            // senderPasswordTextBox
            //
            senderPasswordTextBox.Font = new Font("Segoe UI", 10F);
            senderPasswordTextBox.Location = new Point(340, 98);
            senderPasswordTextBox.Name = "senderPasswordTextBox";
            senderPasswordTextBox.PasswordChar = '●';
            senderPasswordTextBox.Size = new Size(320, 27);
            //
            // senderDisplayNameLabel
            //
            senderDisplayNameLabel.AutoSize = true;
            senderDisplayNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            senderDisplayNameLabel.ForeColor = Color.FromArgb(141, 21, 58);
            senderDisplayNameLabel.Location = new Point(3, 140);
            senderDisplayNameLabel.Name = "senderDisplayNameLabel";
            senderDisplayNameLabel.Text = "Sender Display Name";
            //
            // senderDisplayNameTextBox
            //
            senderDisplayNameTextBox.Font = new Font("Segoe UI", 10F);
            senderDisplayNameTextBox.Location = new Point(3, 163);
            senderDisplayNameTextBox.Name = "senderDisplayNameTextBox";
            senderDisplayNameTextBox.Size = new Size(320, 27);
            //
            // saveEmailSettingsButton
            //
            saveEmailSettingsButton.BackColor = Color.FromArgb(141, 21, 58);
            saveEmailSettingsButton.Cursor = Cursors.Hand;
            saveEmailSettingsButton.FlatStyle = FlatStyle.Flat;
            saveEmailSettingsButton.FlatAppearance.BorderSize = 0;
            saveEmailSettingsButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            saveEmailSettingsButton.ForeColor = Color.White;
            saveEmailSettingsButton.Location = new Point(3, 210);
            saveEmailSettingsButton.Name = "saveEmailSettingsButton";
            saveEmailSettingsButton.Size = new Size(200, 38);
            saveEmailSettingsButton.Text = "💾 Save Email Settings";
            saveEmailSettingsButton.UseVisualStyleBackColor = false;
            //
            // testEmailToLabel
            //
            testEmailToLabel.AutoSize = true;
            testEmailToLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            testEmailToLabel.ForeColor = Color.FromArgb(141, 21, 58);
            testEmailToLabel.Location = new Point(3, 270);
            testEmailToLabel.Name = "testEmailToLabel";
            testEmailToLabel.Text = "Send a test email to:";
            //
            // testEmailToTextBox
            //
            testEmailToTextBox.Font = new Font("Segoe UI", 10F);
            testEmailToTextBox.Location = new Point(3, 293);
            testEmailToTextBox.Name = "testEmailToTextBox";
            testEmailToTextBox.Size = new Size(320, 27);
            //
            // testEmailButton
            //
            testEmailButton.BackColor = Color.FromArgb(212, 160, 23);
            testEmailButton.Cursor = Cursors.Hand;
            testEmailButton.FlatStyle = FlatStyle.Flat;
            testEmailButton.FlatAppearance.BorderSize = 0;
            testEmailButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            testEmailButton.ForeColor = Color.White;
            testEmailButton.Location = new Point(340, 292);
            testEmailButton.Name = "testEmailButton";
            testEmailButton.Size = new Size(180, 30);
            testEmailButton.Text = "✉ Send Test Email";
            testEmailButton.UseVisualStyleBackColor = false;
            //
            // emailStatusLabel
            //
            emailStatusLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            emailStatusLabel.Location = new Point(3, 335);
            emailStatusLabel.MaximumSize = new Size(700, 0);
            emailStatusLabel.Name = "emailStatusLabel";
            emailStatusLabel.Size = new Size(700, 80);
            emailStatusLabel.Text = "";
            //
            // UC_AdminPanel
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainLayout);
            Name = "UC_AdminPanel";
            Size = new Size(1280, 675);
            mainLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            tabControl.ResumeLayout(false);
            lecturersTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lecturersGrid).EndInit();
            lecturerToolPanel.ResumeLayout(false);
            lecturerToolPanel.PerformLayout();
            studentsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)studentsGrid).EndInit();
            studentToolPanel.ResumeLayout(false);
            studentToolPanel.PerformLayout();
            attendanceTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)attendanceGrid).EndInit();
            attendanceToolPanel.ResumeLayout(false);
            attendanceToolPanel.PerformLayout();
            emailTabPage.ResumeLayout(false);
            emailTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)smtpPortNumeric).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private Panel headerPanel;
        private Label titleLabel;
        private Button refreshAllButton;
        private TabControl tabControl;
        private TabPage lecturersTabPage;
        private Panel lecturerToolPanel;
        private Label lecturerCountLabel;
        private Button deleteLecturerButton;
        private DataGridView lecturersGrid;
        private TabPage studentsTabPage;
        private Panel studentToolPanel;
        private Label studentCountLabel;
        private Button deleteStudentButton;
        private DataGridView studentsGrid;
        private TabPage attendanceTabPage;
        private Panel attendanceToolPanel;
        private Label attendanceCountLabel;
        private Button deleteAttendanceButton;
        private DataGridView attendanceGrid;
        private TabPage emailTabPage;
        private Label smtpHostLabel;
        private TextBox smtpHostTextBox;
        private Label smtpPortLabel;
        private NumericUpDown smtpPortNumeric;
        private CheckBox useSslCheckBox;
        private Label senderEmailLabel;
        private TextBox senderEmailTextBox;
        private Label senderPasswordLabel;
        private TextBox senderPasswordTextBox;
        private Label senderDisplayNameLabel;
        private TextBox senderDisplayNameTextBox;
        private Button saveEmailSettingsButton;
        private Label testEmailToLabel;
        private TextBox testEmailToTextBox;
        private Button testEmailButton;
        private Label emailStatusLabel;
    }
}
