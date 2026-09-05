namespace FaceAttendanceSystem.Forms
{
    partial class UC_AttendanceRecords
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
            toolbarPanel = new Panel();
            refreshButton = new Button();
            subjectFilterComboBox = new ComboBox();
            subjectFilterLabel = new Label();
            courseFilterComboBox = new ComboBox();
            courseFilterLabel = new Label();
            datePicker = new DateTimePicker();
            dateLabel = new Label();
            countLabel = new Label();
            contentSplit = new TableLayoutPanel();
            recordsGrid = new DataGridView();
            summaryPanel = new Panel();
            summaryGrid = new DataGridView();
            summaryTitleLabel = new Label();
            mainLayout.SuspendLayout();
            toolbarPanel.SuspendLayout();
            contentSplit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)recordsGrid).BeginInit();
            summaryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)summaryGrid).BeginInit();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(245, 245, 245);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(30);
            mainLayout.Controls.Add(toolbarPanel, 0, 0);
            mainLayout.Controls.Add(contentSplit, 0, 1);
            mainLayout.Name = "mainLayout";
            //
            // toolbarPanel
            //
            toolbarPanel.Controls.Add(refreshButton);
            toolbarPanel.Controls.Add(subjectFilterComboBox);
            toolbarPanel.Controls.Add(subjectFilterLabel);
            toolbarPanel.Controls.Add(courseFilterComboBox);
            toolbarPanel.Controls.Add(courseFilterLabel);
            toolbarPanel.Controls.Add(datePicker);
            toolbarPanel.Controls.Add(dateLabel);
            toolbarPanel.Controls.Add(countLabel);
            toolbarPanel.Dock = DockStyle.Fill;
            toolbarPanel.Name = "toolbarPanel";
            //
            // dateLabel
            //
            dateLabel.AutoSize = true;
            dateLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dateLabel.ForeColor = Color.FromArgb(141, 21, 58);
            dateLabel.Location = new Point(0, 15);
            dateLabel.Name = "dateLabel";
            dateLabel.Text = "Date:";
            //
            // datePicker
            //
            datePicker.Font = new Font("Segoe UI", 9F);
            datePicker.Format = DateTimePickerFormat.Short;
            datePicker.Location = new Point(45, 11);
            datePicker.Name = "datePicker";
            datePicker.Size = new Size(140, 27);
            //
            // courseFilterLabel
            //
            courseFilterLabel.AutoSize = true;
            courseFilterLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            courseFilterLabel.ForeColor = Color.FromArgb(141, 21, 58);
            courseFilterLabel.Location = new Point(200, 15);
            courseFilterLabel.Name = "courseFilterLabel";
            courseFilterLabel.Text = "Course:";
            //
            // courseFilterComboBox
            //
            courseFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            courseFilterComboBox.Font = new Font("Segoe UI", 9F);
            courseFilterComboBox.Location = new Point(265, 11);
            courseFilterComboBox.Name = "courseFilterComboBox";
            courseFilterComboBox.Size = new Size(260, 27);
            //
            // subjectFilterLabel
            //
            subjectFilterLabel.AutoSize = true;
            subjectFilterLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            subjectFilterLabel.ForeColor = Color.FromArgb(141, 21, 58);
            subjectFilterLabel.Location = new Point(540, 15);
            subjectFilterLabel.Name = "subjectFilterLabel";
            subjectFilterLabel.Text = "Subject:";
            //
            // subjectFilterComboBox
            //
            subjectFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            subjectFilterComboBox.Font = new Font("Segoe UI", 9F);
            subjectFilterComboBox.Location = new Point(610, 11);
            subjectFilterComboBox.Name = "subjectFilterComboBox";
            subjectFilterComboBox.Size = new Size(300, 27);
            //
            // refreshButton
            //
            refreshButton.BackColor = Color.FromArgb(141, 21, 58);
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            refreshButton.ForeColor = Color.White;
            refreshButton.Location = new Point(925, 10);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(90, 30);
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Click += refreshButton_Click;
            //
            // countLabel
            //
            countLabel.AutoSize = true;
            countLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            countLabel.ForeColor = Color.Gray;
            countLabel.Location = new Point(0, 55);
            countLabel.Name = "countLabel";
            countLabel.Text = "0 student(s) marked present";
            //
            // recordsGrid
            //
            recordsGrid.AllowUserToAddRows = false;
            recordsGrid.AllowUserToDeleteRows = false;
            recordsGrid.BackgroundColor = Color.White;
            recordsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(141, 21, 58);
            recordsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            recordsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            recordsGrid.Dock = DockStyle.Fill;
            recordsGrid.Font = new Font("Segoe UI", 10F);
            recordsGrid.Margin = new Padding(0, 0, 10, 0);
            recordsGrid.Name = "recordsGrid";
            recordsGrid.ReadOnly = true;
            recordsGrid.RowHeadersVisible = false;
            recordsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // contentSplit
            //
            contentSplit.ColumnCount = 2;
            contentSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            contentSplit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            contentSplit.RowCount = 1;
            contentSplit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            contentSplit.Dock = DockStyle.Fill;
            contentSplit.Controls.Add(recordsGrid, 0, 0);
            contentSplit.Controls.Add(summaryPanel, 1, 0);
            contentSplit.Name = "contentSplit";
            //
            // summaryPanel
            //
            summaryPanel.Controls.Add(summaryGrid);
            summaryPanel.Controls.Add(summaryTitleLabel);
            summaryPanel.Dock = DockStyle.Fill;
            summaryPanel.Margin = new Padding(10, 0, 0, 0);
            summaryPanel.Name = "summaryPanel";
            //
            // summaryTitleLabel
            //
            summaryTitleLabel.Dock = DockStyle.Top;
            summaryTitleLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            summaryTitleLabel.ForeColor = Color.FromArgb(141, 21, 58);
            summaryTitleLabel.Height = 30;
            summaryTitleLabel.Name = "summaryTitleLabel";
            summaryTitleLabel.Text = "Subject-wise Attendance Summary (All Students, All Time)";
            //
            // summaryGrid
            //
            summaryGrid.AllowUserToAddRows = false;
            summaryGrid.AllowUserToDeleteRows = false;
            summaryGrid.BackgroundColor = Color.White;
            summaryGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(212, 160, 23);
            summaryGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            summaryGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            summaryGrid.Dock = DockStyle.Fill;
            summaryGrid.Font = new Font("Segoe UI", 9F);
            summaryGrid.Name = "summaryGrid";
            summaryGrid.ReadOnly = true;
            summaryGrid.RowHeadersVisible = false;
            summaryGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // UC_AttendanceRecords
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainLayout);
            Name = "UC_AttendanceRecords";
            Size = new Size(1280, 675);
            mainLayout.ResumeLayout(false);
            toolbarPanel.ResumeLayout(false);
            toolbarPanel.PerformLayout();
            contentSplit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)recordsGrid).EndInit();
            summaryPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)summaryGrid).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private Panel toolbarPanel;
        private Label dateLabel;
        private DateTimePicker datePicker;
        private Label courseFilterLabel;
        private ComboBox courseFilterComboBox;
        private Label subjectFilterLabel;
        private ComboBox subjectFilterComboBox;
        private Button refreshButton;
        private Label countLabel;
        private TableLayoutPanel contentSplit;
        private DataGridView recordsGrid;
        private Panel summaryPanel;
        private Label summaryTitleLabel;
        private DataGridView summaryGrid;
    }
}
