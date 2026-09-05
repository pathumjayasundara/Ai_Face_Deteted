namespace FaceAttendanceSystem.Forms
{
    partial class UC_RegisterStudent
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
            cameraGroupBox = new GroupBox();
            cameraPictureBox = new PictureBox();
            progressLabel = new Label();
            captureButton = new Button();
            formPanel = new Panel();
            titleLabel = new Label();
            regNumberLabel = new Label();
            regNumberTextBox = new TextBox();
            fullNameLabel = new Label();
            fullNameTextBox = new TextBox();
            courseLabel = new Label();
            courseComboBox = new ComboBox();
            studentEmailLabel = new Label();
            studentEmailTextBox = new TextBox();
            capturedPreviewLabel = new Label();
            capturedPreviewBox = new PictureBox();
            resetPhotosButton = new Button();
            saveButton = new Button();
            statusLabel = new Label();
            mainLayout.SuspendLayout();
            cameraGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cameraPictureBox).BeginInit();
            formPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)capturedPreviewBox).BeginInit();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(245, 245, 245);
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            mainLayout.RowCount = 1;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(30);
            mainLayout.Controls.Add(cameraGroupBox, 0, 0);
            mainLayout.Controls.Add(formPanel, 1, 0);
            mainLayout.Name = "mainLayout";
            //
            // cameraGroupBox
            //
            cameraGroupBox.Controls.Add(captureButton);
            cameraGroupBox.Controls.Add(progressLabel);
            cameraGroupBox.Controls.Add(cameraPictureBox);
            cameraGroupBox.Dock = DockStyle.Fill;
            cameraGroupBox.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cameraGroupBox.ForeColor = Color.FromArgb(141, 21, 58);
            cameraGroupBox.Margin = new Padding(10);
            cameraGroupBox.Name = "cameraGroupBox";
            cameraGroupBox.Text = "Live Camera - capture 5 photos for accurate recognition";
            //
            // cameraPictureBox
            //
            cameraPictureBox.BackColor = Color.Black;
            cameraPictureBox.Dock = DockStyle.Fill;
            cameraPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            cameraPictureBox.Margin = new Padding(15, 35, 15, 90);
            cameraPictureBox.Name = "cameraPictureBox";
            cameraPictureBox.TabStop = false;
            //
            // progressLabel
            //
            progressLabel.Dock = DockStyle.Bottom;
            progressLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            progressLabel.ForeColor = Color.FromArgb(141, 21, 58);
            progressLabel.Height = 30;
            progressLabel.Name = "progressLabel";
            progressLabel.Text = "Photos captured: 0 / 5";
            progressLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // captureButton
            //
            captureButton.BackColor = Color.FromArgb(141, 21, 58);
            captureButton.Cursor = Cursors.Hand;
            captureButton.Dock = DockStyle.Bottom;
            captureButton.FlatStyle = FlatStyle.Flat;
            captureButton.FlatAppearance.BorderSize = 0;
            captureButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            captureButton.ForeColor = Color.White;
            captureButton.Height = 48;
            captureButton.Margin = new Padding(15);
            captureButton.Name = "captureButton";
            captureButton.Text = "📸 Capture Photo 1";
            captureButton.UseVisualStyleBackColor = false;
            captureButton.Click += captureButton_Click;
            //
            // formPanel
            //
            formPanel.AutoScroll = true;
            formPanel.Controls.Add(statusLabel);
            formPanel.Controls.Add(saveButton);
            formPanel.Controls.Add(resetPhotosButton);
            formPanel.Controls.Add(capturedPreviewBox);
            formPanel.Controls.Add(capturedPreviewLabel);
            formPanel.Controls.Add(studentEmailTextBox);
            formPanel.Controls.Add(studentEmailLabel);
            formPanel.Controls.Add(courseComboBox);
            formPanel.Controls.Add(courseLabel);
            formPanel.Controls.Add(fullNameTextBox);
            formPanel.Controls.Add(fullNameLabel);
            formPanel.Controls.Add(regNumberTextBox);
            formPanel.Controls.Add(regNumberLabel);
            formPanel.Controls.Add(titleLabel);
            formPanel.Dock = DockStyle.Fill;
            formPanel.Margin = new Padding(10);
            formPanel.Name = "formPanel";
            //
            // titleLabel
            //
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(141, 21, 58);
            titleLabel.Location = new Point(20, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Text = "Register New Student";
            //
            // regNumberLabel
            //
            regNumberLabel.AutoSize = true;
            regNumberLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            regNumberLabel.ForeColor = Color.FromArgb(141, 21, 58);
            regNumberLabel.Location = new Point(20, 65);
            regNumberLabel.Name = "regNumberLabel";
            regNumberLabel.Text = "Student ID";
            //
            // regNumberTextBox
            //
            regNumberTextBox.Font = new Font("Segoe UI", 11F);
            regNumberTextBox.Location = new Point(20, 90);
            regNumberTextBox.Name = "regNumberTextBox";
            regNumberTextBox.PlaceholderText = "e.g. 22APP1234";
            regNumberTextBox.Size = new Size(320, 29);
            //
            // fullNameLabel
            //
            fullNameLabel.AutoSize = true;
            fullNameLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            fullNameLabel.ForeColor = Color.FromArgb(141, 21, 58);
            fullNameLabel.Location = new Point(20, 135);
            fullNameLabel.Name = "fullNameLabel";
            fullNameLabel.Text = "Full Name";
            //
            // fullNameTextBox
            //
            fullNameTextBox.Font = new Font("Segoe UI", 11F);
            fullNameTextBox.Location = new Point(20, 160);
            fullNameTextBox.Name = "fullNameTextBox";
            fullNameTextBox.Size = new Size(320, 29);
            //
            // courseLabel
            //
            courseLabel.AutoSize = true;
            courseLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            courseLabel.ForeColor = Color.FromArgb(141, 21, 58);
            courseLabel.Location = new Point(20, 205);
            courseLabel.Name = "courseLabel";
            courseLabel.Text = "Course";
            //
            // courseComboBox
            //
            courseComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            courseComboBox.Font = new Font("Segoe UI", 10F);
            courseComboBox.Items.AddRange(new object[] {
                "BSc Physical Sciences",
                "BSc Hons in Chemical Technology",
                "BSc Hons in Applied Physics",
                "BSc Hons in Computer Science and Technology"
            });
            courseComboBox.Location = new Point(20, 230);
            courseComboBox.Name = "courseComboBox";
            courseComboBox.Size = new Size(320, 29);
            //
            // studentEmailLabel
            //
            studentEmailLabel.AutoSize = true;
            studentEmailLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            studentEmailLabel.ForeColor = Color.FromArgb(141, 21, 58);
            studentEmailLabel.Location = new Point(20, 275);
            studentEmailLabel.Name = "studentEmailLabel";
            studentEmailLabel.Text = "Official Student Email (@ms.sab.ac.lk) - required";
            //
            // studentEmailTextBox
            //
            studentEmailTextBox.Font = new Font("Segoe UI", 10F);
            studentEmailTextBox.Location = new Point(20, 300);
            studentEmailTextBox.Name = "studentEmailTextBox";
            studentEmailTextBox.PlaceholderText = "e.g. 22app6134@ms.sab.ac.lk";
            studentEmailTextBox.Size = new Size(320, 27);
            //
            // capturedPreviewLabel
            //
            capturedPreviewLabel.AutoSize = true;
            capturedPreviewLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            capturedPreviewLabel.ForeColor = Color.FromArgb(141, 21, 58);
            capturedPreviewLabel.Location = new Point(20, 340);
            capturedPreviewLabel.Name = "capturedPreviewLabel";
            capturedPreviewLabel.Text = "Last Captured Photo";
            //
            // capturedPreviewBox
            //
            capturedPreviewBox.BackColor = Color.Gainsboro;
            capturedPreviewBox.BorderStyle = BorderStyle.FixedSingle;
            capturedPreviewBox.Location = new Point(20, 365);
            capturedPreviewBox.Name = "capturedPreviewBox";
            capturedPreviewBox.Size = new Size(180, 180);
            capturedPreviewBox.SizeMode = PictureBoxSizeMode.Zoom;
            capturedPreviewBox.TabStop = false;
            //
            // resetPhotosButton
            //
            resetPhotosButton.BackColor = Color.Gray;
            resetPhotosButton.Cursor = Cursors.Hand;
            resetPhotosButton.FlatStyle = FlatStyle.Flat;
            resetPhotosButton.FlatAppearance.BorderSize = 0;
            resetPhotosButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            resetPhotosButton.ForeColor = Color.White;
            resetPhotosButton.Location = new Point(220, 365);
            resetPhotosButton.Name = "resetPhotosButton";
            resetPhotosButton.Size = new Size(120, 36);
            resetPhotosButton.Text = "🔄 Reset Photos";
            resetPhotosButton.UseVisualStyleBackColor = false;
            resetPhotosButton.Click += resetPhotosButton_Click;
            //
            // saveButton
            //
            saveButton.BackColor = Color.FromArgb(212, 160, 23);
            saveButton.Cursor = Cursors.Hand;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            saveButton.ForeColor = Color.White;
            saveButton.Location = new Point(20, 565);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(320, 48);
            saveButton.Text = "💾 Save Student";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += saveButton_Click;
            //
            // statusLabel
            //
            statusLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            statusLabel.Location = new Point(20, 625);
            statusLabel.MaximumSize = new Size(320, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(320, 60);
            statusLabel.Text = "";
            //
            // UC_RegisterStudent
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(mainLayout);
            Name = "UC_RegisterStudent";
            Size = new Size(1280, 675);
            mainLayout.ResumeLayout(false);
            cameraGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cameraPictureBox).EndInit();
            formPanel.ResumeLayout(false);
            formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)capturedPreviewBox).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private GroupBox cameraGroupBox;
        private PictureBox cameraPictureBox;
        private Label progressLabel;
        private Button captureButton;
        private Panel formPanel;
        private Label titleLabel;
        private Label regNumberLabel;
        private TextBox regNumberTextBox;
        private Label fullNameLabel;
        private TextBox fullNameTextBox;
        private Label courseLabel;
        private ComboBox courseComboBox;
        private Label studentEmailLabel;
        private TextBox studentEmailTextBox;
        private Label capturedPreviewLabel;
        private PictureBox capturedPreviewBox;
        private Button resetPhotosButton;
        private Button saveButton;
        private Label statusLabel;
    }
}
