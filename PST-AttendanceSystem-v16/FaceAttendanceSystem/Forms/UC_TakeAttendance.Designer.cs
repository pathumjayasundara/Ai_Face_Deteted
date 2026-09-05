namespace FaceAttendanceSystem.Forms
{
    partial class UC_TakeAttendance
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
            subjectPanel = new Panel();
            joinedCountLabel = new Label();
            downloadPdfButton = new Button();
            lectureLabel = new Label();
            startButton = new Button();
            subjectComboBox = new ComboBox();
            subjectLabel = new Label();
            filterYearComboBox = new ComboBox();
            filterSemesterComboBox = new ComboBox();
            cameraGroupBox = new GroupBox();
            cameraPictureBox = new PictureBox();
            thankYouPanel = new Panel();
            thankYouNameLabel = new Label();
            thankYouMessageLabel = new Label();
            statusPanel = new Panel();
            statusLabel = new Label();
            mainLayout.SuspendLayout();
            subjectPanel.SuspendLayout();
            cameraGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cameraPictureBox).BeginInit();
            thankYouPanel.SuspendLayout();
            statusPanel.SuspendLayout();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(245, 245, 245);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(30);
            mainLayout.Controls.Add(subjectPanel, 0, 0);
            mainLayout.Controls.Add(cameraGroupBox, 0, 1);
            mainLayout.Controls.Add(statusPanel, 0, 2);
            mainLayout.Name = "mainLayout";
            //
            // subjectPanel
            //
            subjectPanel.Controls.Add(joinedCountLabel);
            subjectPanel.Controls.Add(downloadPdfButton);
            subjectPanel.Controls.Add(lectureLabel);
            subjectPanel.Controls.Add(startButton);
            subjectPanel.Controls.Add(filterSemesterComboBox);
            subjectPanel.Controls.Add(filterYearComboBox);
            subjectPanel.Controls.Add(subjectComboBox);
            subjectPanel.Controls.Add(subjectLabel);
            subjectPanel.Dock = DockStyle.Fill;
            subjectPanel.Name = "subjectPanel";
            //
            // joinedCountLabel
            //
            joinedCountLabel.AutoSize = true;
            joinedCountLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            joinedCountLabel.ForeColor = Color.FromArgb(212, 160, 23);
            joinedCountLabel.Location = new Point(0, 72);
            joinedCountLabel.Name = "joinedCountLabel";
            joinedCountLabel.Text = "";
            //
            // subjectLabel
            //
            subjectLabel.AutoSize = true;
            subjectLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            subjectLabel.ForeColor = Color.FromArgb(141, 21, 58);
            subjectLabel.Location = new Point(0, 15);
            subjectLabel.Name = "subjectLabel";
            subjectLabel.Text = "Select Subject / Lecture:";
            //
            // subjectComboBox
            //
            subjectComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            subjectComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            subjectComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            subjectComboBox.Font = new Font("Segoe UI", 10F);
            subjectComboBox.Location = new Point(200, 10);
            subjectComboBox.Name = "subjectComboBox";
            subjectComboBox.Size = new Size(400, 29);
            //
            // filterYearComboBox
            //
            filterYearComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterYearComboBox.Font = new Font("Segoe UI", 9F);
            filterYearComboBox.Items.AddRange(new object[] { "All Years", "Year 1", "Year 2", "Year 3", "Year 4" });
            filterYearComboBox.Location = new Point(615, 10);
            filterYearComboBox.Name = "filterYearComboBox";
            filterYearComboBox.Size = new Size(105, 29);
            //
            // filterSemesterComboBox
            //
            filterSemesterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterSemesterComboBox.Font = new Font("Segoe UI", 9F);
            filterSemesterComboBox.Items.AddRange(new object[] { "Both Sem.", "Semester 1", "Semester 2" });
            filterSemesterComboBox.Location = new Point(725, 10);
            filterSemesterComboBox.Name = "filterSemesterComboBox";
            filterSemesterComboBox.Size = new Size(110, 29);
            //
            // startButton
            //
            startButton.BackColor = Color.FromArgb(141, 21, 58);
            startButton.Cursor = Cursors.Hand;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.FlatAppearance.BorderSize = 0;
            startButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            startButton.ForeColor = Color.White;
            startButton.Location = new Point(845, 9);
            startButton.Name = "startButton";
            startButton.Size = new Size(160, 32);
            startButton.Text = "▶ Start Lecture";
            startButton.UseVisualStyleBackColor = false;
            //
            // downloadPdfButton
            //
            downloadPdfButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            downloadPdfButton.BackColor = Color.FromArgb(212, 160, 23);
            downloadPdfButton.Cursor = Cursors.Hand;
            downloadPdfButton.FlatStyle = FlatStyle.Flat;
            downloadPdfButton.FlatAppearance.BorderSize = 0;
            downloadPdfButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            downloadPdfButton.ForeColor = Color.White;
            downloadPdfButton.Location = new Point(1030, 9);
            downloadPdfButton.Name = "downloadPdfButton";
            downloadPdfButton.Size = new Size(180, 32);
            downloadPdfButton.Text = "📄 Download PDF Sheet";
            downloadPdfButton.UseVisualStyleBackColor = false;
            //
            // lectureLabel
            //
            lectureLabel.AutoSize = true;
            lectureLabel.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lectureLabel.ForeColor = Color.DimGray;
            lectureLabel.Location = new Point(0, 45);
            lectureLabel.Name = "lectureLabel";
            lectureLabel.Text = "No lecture started yet.";
            //
            // cameraGroupBox
            //
            cameraGroupBox.Controls.Add(thankYouPanel);
            cameraGroupBox.Controls.Add(cameraPictureBox);
            cameraGroupBox.Dock = DockStyle.Fill;
            cameraGroupBox.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cameraGroupBox.ForeColor = Color.FromArgb(141, 21, 58);
            cameraGroupBox.Name = "cameraGroupBox";
            cameraGroupBox.Text = "Face the camera to mark attendance automatically";
            //
            // cameraPictureBox
            //
            cameraPictureBox.BackColor = Color.Black;
            cameraPictureBox.Dock = DockStyle.Fill;
            cameraPictureBox.Margin = new Padding(15, 35, 15, 15);
            cameraPictureBox.Name = "cameraPictureBox";
            cameraPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            cameraPictureBox.TabStop = false;
            //
            // thankYouPanel
            //
            thankYouPanel.BackColor = Color.FromArgb(230, 141, 21, 58);
            thankYouPanel.Controls.Add(thankYouMessageLabel);
            thankYouPanel.Controls.Add(thankYouNameLabel);
            thankYouPanel.Name = "thankYouPanel";
            thankYouPanel.Size = new Size(600, 0);
            //
            // thankYouNameLabel
            //
            thankYouNameLabel.Dock = DockStyle.Top;
            thankYouNameLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            thankYouNameLabel.ForeColor = Color.White;
            thankYouNameLabel.Height = 40;
            thankYouNameLabel.Name = "thankYouNameLabel";
            thankYouNameLabel.Text = "";
            thankYouNameLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // thankYouMessageLabel
            //
            thankYouMessageLabel.Dock = DockStyle.Fill;
            thankYouMessageLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            thankYouMessageLabel.ForeColor = Color.FromArgb(212, 160, 23);
            thankYouMessageLabel.Name = "thankYouMessageLabel";
            thankYouMessageLabel.Text = "Take Your Attend, Thank You! 🎉\nPlease Come Next Student";
            thankYouMessageLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // statusPanel
            //
            statusPanel.Controls.Add(statusLabel);
            statusPanel.Dock = DockStyle.Fill;
            statusPanel.Name = "statusPanel";
            //
            // statusLabel
            //
            statusLabel.Dock = DockStyle.Fill;
            statusLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            statusLabel.ForeColor = Color.FromArgb(141, 21, 58);
            statusLabel.Name = "statusLabel";
            statusLabel.Text = "Select a subject and click Start Lecture.";
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // UC_TakeAttendance
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(mainLayout);
            Name = "UC_TakeAttendance";
            Size = new Size(1280, 675);
            mainLayout.ResumeLayout(false);
            subjectPanel.ResumeLayout(false);
            subjectPanel.PerformLayout();
            cameraGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)cameraPictureBox).EndInit();
            thankYouPanel.ResumeLayout(false);
            statusPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private Panel subjectPanel;
        private Label joinedCountLabel;
        private Button downloadPdfButton;
        private Label subjectLabel;
        private ComboBox subjectComboBox;
        private ComboBox filterYearComboBox;
        private ComboBox filterSemesterComboBox;
        private Button startButton;
        private Label lectureLabel;
        private GroupBox cameraGroupBox;
        private PictureBox cameraPictureBox;
        private Panel thankYouPanel;
        private Label thankYouNameLabel;
        private Label thankYouMessageLabel;
        private Panel statusPanel;
        private Label statusLabel;
    }
}
