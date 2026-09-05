namespace FaceAttendanceSystem.Forms
{
    partial class UC_Timetable
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
            formGroupBox = new GroupBox();
            addButton = new Button();
            semesterComboBox = new ComboBox();
            semesterLabel = new Label();
            yearComboBox = new ComboBox();
            yearLabel = new Label();
            subjectComboBox = new ComboBox();
            subjectLabel = new Label();
            roomTextBox = new TextBox();
            roomLabel = new Label();
            endTimePicker = new DateTimePicker();
            endTimeLabel = new Label();
            startTimePicker = new DateTimePicker();
            startTimeLabel = new Label();
            dayComboBox = new ComboBox();
            dayLabel = new Label();
            gridPanel = new Panel();
            timetableGrid = new DataGridView();
            toolbarPanel = new Panel();
            statusLabel = new Label();
            countLabel = new Label();
            refreshButton = new Button();
            importPhotoButton = new Button();
            deleteButton = new Button();
            mainLayout.SuspendLayout();
            formGroupBox.SuspendLayout();
            gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)timetableGrid).BeginInit();
            toolbarPanel.SuspendLayout();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(245, 245, 245);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(30);
            mainLayout.Controls.Add(formGroupBox, 0, 0);
            mainLayout.Controls.Add(gridPanel, 0, 1);
            mainLayout.Name = "mainLayout";
            //
            // formGroupBox
            //
            formGroupBox.Controls.Add(addButton);
            formGroupBox.Controls.Add(semesterComboBox);
            formGroupBox.Controls.Add(semesterLabel);
            formGroupBox.Controls.Add(yearComboBox);
            formGroupBox.Controls.Add(yearLabel);
            formGroupBox.Controls.Add(subjectComboBox);
            formGroupBox.Controls.Add(subjectLabel);
            formGroupBox.Controls.Add(roomTextBox);
            formGroupBox.Controls.Add(roomLabel);
            formGroupBox.Controls.Add(endTimePicker);
            formGroupBox.Controls.Add(endTimeLabel);
            formGroupBox.Controls.Add(startTimePicker);
            formGroupBox.Controls.Add(startTimeLabel);
            formGroupBox.Controls.Add(dayComboBox);
            formGroupBox.Controls.Add(dayLabel);
            formGroupBox.Dock = DockStyle.Fill;
            formGroupBox.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            formGroupBox.ForeColor = Color.FromArgb(141, 21, 58);
            formGroupBox.Name = "formGroupBox";
            formGroupBox.Text = "Schedule a Class";
            //
            // dayLabel
            //
            dayLabel.AutoSize = true;
            dayLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dayLabel.Location = new Point(15, 35);
            dayLabel.Name = "dayLabel";
            dayLabel.Text = "Day";
            //
            // dayComboBox
            //
            dayComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            dayComboBox.Font = new Font("Segoe UI", 10F);
            dayComboBox.Items.AddRange(new object[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            dayComboBox.Location = new Point(15, 58);
            dayComboBox.Name = "dayComboBox";
            dayComboBox.Size = new Size(120, 28);
            //
            // startTimeLabel
            //
            startTimeLabel.AutoSize = true;
            startTimeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            startTimeLabel.Location = new Point(150, 35);
            startTimeLabel.Name = "startTimeLabel";
            startTimeLabel.Text = "Start Time";
            //
            // startTimePicker
            //
            startTimePicker.Font = new Font("Segoe UI", 10F);
            startTimePicker.Format = DateTimePickerFormat.Time;
            startTimePicker.ShowUpDown = true;
            startTimePicker.Location = new Point(150, 58);
            startTimePicker.Name = "startTimePicker";
            startTimePicker.Size = new Size(110, 27);
            //
            // endTimeLabel
            //
            endTimeLabel.AutoSize = true;
            endTimeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            endTimeLabel.Location = new Point(270, 35);
            endTimeLabel.Name = "endTimeLabel";
            endTimeLabel.Text = "End Time";
            //
            // endTimePicker
            //
            endTimePicker.Font = new Font("Segoe UI", 10F);
            endTimePicker.Format = DateTimePickerFormat.Time;
            endTimePicker.ShowUpDown = true;
            endTimePicker.Location = new Point(270, 58);
            endTimePicker.Name = "endTimePicker";
            endTimePicker.Size = new Size(110, 27);
            //
            // roomLabel
            //
            roomLabel.AutoSize = true;
            roomLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            roomLabel.Location = new Point(390, 35);
            roomLabel.Name = "roomLabel";
            roomLabel.Text = "Room / Hall";
            //
            // roomTextBox
            //
            roomTextBox.Font = new Font("Segoe UI", 10F);
            roomTextBox.Location = new Point(390, 58);
            roomTextBox.Name = "roomTextBox";
            roomTextBox.Size = new Size(130, 27);
            //
            // subjectLabel
            //
            subjectLabel.AutoSize = true;
            subjectLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            subjectLabel.Location = new Point(15, 100);
            subjectLabel.Name = "subjectLabel";
            subjectLabel.Text = "Subject";
            //
            // subjectComboBox
            //
            subjectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            subjectComboBox.Font = new Font("Segoe UI", 10F);
            subjectComboBox.Location = new Point(15, 123);
            subjectComboBox.Name = "subjectComboBox";
            subjectComboBox.Size = new Size(300, 28);
            //
            // yearLabel
            //
            yearLabel.AutoSize = true;
            yearLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            yearLabel.Location = new Point(330, 100);
            yearLabel.Name = "yearLabel";
            yearLabel.Text = "Year";
            //
            // yearComboBox
            //
            yearComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            yearComboBox.Font = new Font("Segoe UI", 10F);
            yearComboBox.Items.AddRange(new object[] { "1", "2", "3", "4" });
            yearComboBox.Location = new Point(330, 123);
            yearComboBox.Name = "yearComboBox";
            yearComboBox.SelectedIndex = 0;
            yearComboBox.Size = new Size(70, 28);
            //
            // semesterLabel
            //
            semesterLabel.AutoSize = true;
            semesterLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            semesterLabel.Location = new Point(415, 100);
            semesterLabel.Name = "semesterLabel";
            semesterLabel.Text = "Semester";
            //
            // semesterComboBox
            //
            semesterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            semesterComboBox.Font = new Font("Segoe UI", 10F);
            semesterComboBox.Items.AddRange(new object[] { "1", "2" });
            semesterComboBox.Location = new Point(415, 123);
            semesterComboBox.Name = "semesterComboBox";
            semesterComboBox.SelectedIndex = 0;
            semesterComboBox.Size = new Size(70, 28);
            //
            // addButton
            //
            addButton.BackColor = Color.FromArgb(141, 21, 58);
            addButton.Cursor = Cursors.Hand;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.FlatAppearance.BorderSize = 0;
            addButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(510, 122);
            addButton.Name = "addButton";
            addButton.Size = new Size(150, 32);
            addButton.Text = "➕ Add To Timetable";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            //
            // gridPanel
            //
            gridPanel.Controls.Add(timetableGrid);
            gridPanel.Controls.Add(toolbarPanel);
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.Name = "gridPanel";
            //
            // toolbarPanel
            //
            toolbarPanel.Controls.Add(deleteButton);
            toolbarPanel.Controls.Add(refreshButton);
            toolbarPanel.Controls.Add(importPhotoButton);
            toolbarPanel.Controls.Add(countLabel);
            toolbarPanel.Controls.Add(statusLabel);
            toolbarPanel.Dock = DockStyle.Top;
            toolbarPanel.Height = 40;
            toolbarPanel.Name = "toolbarPanel";
            //
            // statusLabel
            //
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            statusLabel.Location = new Point(240, 10);
            statusLabel.Name = "statusLabel";
            statusLabel.Text = "";
            //
            // countLabel
            //
            countLabel.AutoSize = true;
            countLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            countLabel.ForeColor = Color.Gray;
            countLabel.Location = new Point(0, 10);
            countLabel.Name = "countLabel";
            countLabel.Text = "0 scheduled class(es)";
            //
            // refreshButton
            //
            refreshButton.BackColor = Color.FromArgb(212, 160, 23);
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            refreshButton.ForeColor = Color.White;
            refreshButton.Location = new Point(700, 5);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(90, 30);
            refreshButton.Text = "Refresh";
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Click += refreshButton_Click;
            //
            // importPhotoButton
            //
            importPhotoButton.BackColor = Color.FromArgb(141, 21, 58);
            importPhotoButton.Cursor = Cursors.Hand;
            importPhotoButton.FlatStyle = FlatStyle.Flat;
            importPhotoButton.FlatAppearance.BorderSize = 0;
            importPhotoButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            importPhotoButton.ForeColor = Color.White;
            importPhotoButton.Location = new Point(930, 5);
            importPhotoButton.Name = "importPhotoButton";
            importPhotoButton.Size = new Size(180, 30);
            importPhotoButton.Text = "📷 Import Photo / PDF";
            importPhotoButton.UseVisualStyleBackColor = false;
            //
            // deleteButton
            //
            deleteButton.BackColor = Color.Firebrick;
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            deleteButton.ForeColor = Color.White;
            deleteButton.Location = new Point(800, 5);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(120, 30);
            deleteButton.Text = "🗑 Remove Selected";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;
            //
            // timetableGrid
            //
            timetableGrid.AllowUserToAddRows = false;
            timetableGrid.AllowUserToDeleteRows = false;
            timetableGrid.BackgroundColor = Color.White;
            timetableGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(141, 21, 58);
            timetableGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            timetableGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            timetableGrid.Dock = DockStyle.Fill;
            timetableGrid.Font = new Font("Segoe UI", 10F);
            timetableGrid.Name = "timetableGrid";
            timetableGrid.ReadOnly = true;
            timetableGrid.RowHeadersVisible = false;
            timetableGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            timetableGrid.MultiSelect = false;
            timetableGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // UC_Timetable
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(mainLayout);
            Name = "UC_Timetable";
            Size = new Size(1280, 675);
            mainLayout.ResumeLayout(false);
            formGroupBox.ResumeLayout(false);
            formGroupBox.PerformLayout();
            gridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)timetableGrid).EndInit();
            toolbarPanel.ResumeLayout(false);
            toolbarPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private GroupBox formGroupBox;
        private Label dayLabel;
        private ComboBox dayComboBox;
        private Label startTimeLabel;
        private DateTimePicker startTimePicker;
        private Label endTimeLabel;
        private DateTimePicker endTimePicker;
        private Label roomLabel;
        private TextBox roomTextBox;
        private Label subjectLabel;
        private ComboBox subjectComboBox;
        private Label yearLabel;
        private ComboBox yearComboBox;
        private Label semesterLabel;
        private ComboBox semesterComboBox;
        private Button addButton;
        private Panel gridPanel;
        private DataGridView timetableGrid;
        private Panel toolbarPanel;
        private Label countLabel;
        private Label statusLabel;
        private Button refreshButton;
        private Button importPhotoButton;
        private Button deleteButton;
    }
}
