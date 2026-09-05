namespace FaceAttendanceSystem.Forms
{
    partial class UC_ManageSubjects
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
            creditsNumeric = new NumericUpDown();
            creditsLabel = new Label();
            semesterComboBox = new ComboBox();
            semesterLabel = new Label();
            yearComboBox = new ComboBox();
            yearLabel = new Label();
            subjectNameTextBox = new TextBox();
            subjectNameLabel = new Label();
            subjectCodeTextBox = new TextBox();
            subjectCodeLabel = new Label();
            gridPanel = new Panel();
            subjectsGrid = new DataGridView();
            filterPanel = new Panel();
            filterYearLabel = new Label();
            filterYearComboBox = new ComboBox();
            filterSemesterLabel = new Label();
            filterSemesterComboBox = new ComboBox();
            searchLabel = new Label();
            searchTextBox = new TextBox();
            toolbarPanel = new Panel();
            statusLabel = new Label();
            countLabel = new Label();
            refreshButton = new Button();
            deleteButton = new Button();
            mainLayout.SuspendLayout();
            formGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)creditsNumeric).BeginInit();
            gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)subjectsGrid).BeginInit();
            filterPanel.SuspendLayout();
            toolbarPanel.SuspendLayout();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(245, 245, 245);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 2;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
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
            formGroupBox.Controls.Add(creditsNumeric);
            formGroupBox.Controls.Add(creditsLabel);
            formGroupBox.Controls.Add(semesterComboBox);
            formGroupBox.Controls.Add(semesterLabel);
            formGroupBox.Controls.Add(yearComboBox);
            formGroupBox.Controls.Add(yearLabel);
            formGroupBox.Controls.Add(subjectNameTextBox);
            formGroupBox.Controls.Add(subjectNameLabel);
            formGroupBox.Controls.Add(subjectCodeTextBox);
            formGroupBox.Controls.Add(subjectCodeLabel);
            formGroupBox.Dock = DockStyle.Fill;
            formGroupBox.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            formGroupBox.ForeColor = Color.FromArgb(141, 21, 58);
            formGroupBox.Name = "formGroupBox";
            formGroupBox.Text = "Add New Subject";
            //
            // subjectCodeLabel
            //
            subjectCodeLabel.AutoSize = true;
            subjectCodeLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            subjectCodeLabel.Location = new Point(15, 35);
            subjectCodeLabel.Name = "subjectCodeLabel";
            subjectCodeLabel.Text = "Subject Code";
            //
            // subjectCodeTextBox
            //
            subjectCodeTextBox.Font = new Font("Segoe UI", 10F);
            subjectCodeTextBox.Location = new Point(15, 58);
            subjectCodeTextBox.Name = "subjectCodeTextBox";
            subjectCodeTextBox.Size = new Size(140, 27);
            //
            // subjectNameLabel
            //
            subjectNameLabel.AutoSize = true;
            subjectNameLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            subjectNameLabel.Location = new Point(170, 35);
            subjectNameLabel.Name = "subjectNameLabel";
            subjectNameLabel.Text = "Subject Name";
            //
            // subjectNameTextBox
            //
            subjectNameTextBox.Font = new Font("Segoe UI", 10F);
            subjectNameTextBox.Location = new Point(170, 58);
            subjectNameTextBox.Name = "subjectNameTextBox";
            subjectNameTextBox.Size = new Size(260, 27);
            //
            // yearLabel
            //
            yearLabel.AutoSize = true;
            yearLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            yearLabel.Location = new Point(445, 35);
            yearLabel.Name = "yearLabel";
            yearLabel.Text = "Year";
            //
            // yearComboBox
            //
            yearComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            yearComboBox.Font = new Font("Segoe UI", 10F);
            yearComboBox.Items.AddRange(new object[] { "1", "2", "3", "4" });
            yearComboBox.Location = new Point(445, 58);
            yearComboBox.Name = "yearComboBox";
            yearComboBox.Size = new Size(70, 28);
            //
            // semesterLabel
            //
            semesterLabel.AutoSize = true;
            semesterLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            semesterLabel.Location = new Point(530, 35);
            semesterLabel.Name = "semesterLabel";
            semesterLabel.Text = "Semester";
            //
            // semesterComboBox
            //
            semesterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            semesterComboBox.Font = new Font("Segoe UI", 10F);
            semesterComboBox.Items.AddRange(new object[] { "1", "2" });
            semesterComboBox.Location = new Point(530, 58);
            semesterComboBox.Name = "semesterComboBox";
            semesterComboBox.Size = new Size(70, 28);
            //
            // creditsLabel
            //
            creditsLabel.AutoSize = true;
            creditsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            creditsLabel.Location = new Point(620, 35);
            creditsLabel.Name = "creditsLabel";
            creditsLabel.Text = "Credits";
            //
            // creditsNumeric
            //
            creditsNumeric.Font = new Font("Segoe UI", 10F);
            creditsNumeric.Location = new Point(620, 58);
            creditsNumeric.Maximum = 10;
            creditsNumeric.Minimum = 1;
            creditsNumeric.Name = "creditsNumeric";
            creditsNumeric.Size = new Size(70, 27);
            //
            // addButton
            //
            addButton.BackColor = Color.FromArgb(141, 21, 58);
            addButton.Cursor = Cursors.Hand;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.FlatAppearance.BorderSize = 0;
            addButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Location = new Point(720, 55);
            addButton.Name = "addButton";
            addButton.Size = new Size(130, 32);
            addButton.Text = "➕ Add Subject";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;
            //
            // gridPanel
            //
            gridPanel.Controls.Add(subjectsGrid);
            gridPanel.Controls.Add(toolbarPanel);
            gridPanel.Controls.Add(filterPanel);
            gridPanel.Dock = DockStyle.Fill;
            gridPanel.Name = "gridPanel";
            //
            // filterPanel
            //
            filterPanel.Controls.Add(searchTextBox);
            filterPanel.Controls.Add(searchLabel);
            filterPanel.Controls.Add(filterSemesterComboBox);
            filterPanel.Controls.Add(filterSemesterLabel);
            filterPanel.Controls.Add(filterYearComboBox);
            filterPanel.Controls.Add(filterYearLabel);
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 45;
            filterPanel.Name = "filterPanel";
            //
            // filterYearLabel
            //
            filterYearLabel.AutoSize = true;
            filterYearLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filterYearLabel.ForeColor = Color.FromArgb(141, 21, 58);
            filterYearLabel.Location = new Point(0, 12);
            filterYearLabel.Name = "filterYearLabel";
            filterYearLabel.Text = "Year:";
            //
            // filterYearComboBox
            //
            filterYearComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterYearComboBox.Font = new Font("Segoe UI", 9F);
            filterYearComboBox.Items.AddRange(new object[] { "All Years", "Year 1", "Year 2", "Year 3", "Year 4" });
            filterYearComboBox.Location = new Point(45, 8);
            filterYearComboBox.Name = "filterYearComboBox";
            filterYearComboBox.Size = new Size(110, 27);
            //
            // filterSemesterLabel
            //
            filterSemesterLabel.AutoSize = true;
            filterSemesterLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filterSemesterLabel.ForeColor = Color.FromArgb(141, 21, 58);
            filterSemesterLabel.Location = new Point(170, 12);
            filterSemesterLabel.Name = "filterSemesterLabel";
            filterSemesterLabel.Text = "Semester:";
            //
            // filterSemesterComboBox
            //
            filterSemesterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            filterSemesterComboBox.Font = new Font("Segoe UI", 9F);
            filterSemesterComboBox.Items.AddRange(new object[] { "Both Semesters", "Semester 1", "Semester 2" });
            filterSemesterComboBox.Location = new Point(240, 8);
            filterSemesterComboBox.Name = "filterSemesterComboBox";
            filterSemesterComboBox.Size = new Size(130, 27);
            //
            // searchLabel
            //
            searchLabel.AutoSize = true;
            searchLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            searchLabel.ForeColor = Color.FromArgb(141, 21, 58);
            searchLabel.Location = new Point(385, 12);
            searchLabel.Name = "searchLabel";
            searchLabel.Text = "Search:";
            //
            // searchTextBox
            //
            searchTextBox.Font = new Font("Segoe UI", 9F);
            searchTextBox.Location = new Point(440, 8);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "Subject code or name...";
            searchTextBox.Size = new Size(220, 27);
            //
            // toolbarPanel
            //
            toolbarPanel.Controls.Add(deleteButton);
            toolbarPanel.Controls.Add(refreshButton);
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
            countLabel.Text = "0 subject(s)";
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
            deleteButton.Text = "🗑 Delete Selected";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;
            //
            // subjectsGrid
            //
            subjectsGrid.AllowUserToAddRows = false;
            subjectsGrid.AllowUserToDeleteRows = false;
            subjectsGrid.BackgroundColor = Color.White;
            subjectsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(141, 21, 58);
            subjectsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            subjectsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            subjectsGrid.Dock = DockStyle.Fill;
            subjectsGrid.Font = new Font("Segoe UI", 10F);
            subjectsGrid.Name = "subjectsGrid";
            subjectsGrid.ReadOnly = true;
            subjectsGrid.RowHeadersVisible = false;
            subjectsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            subjectsGrid.MultiSelect = false;
            subjectsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // UC_ManageSubjects
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(mainLayout);
            Name = "UC_ManageSubjects";
            Size = new Size(1280, 675);
            mainLayout.ResumeLayout(false);
            formGroupBox.ResumeLayout(false);
            formGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)creditsNumeric).EndInit();
            gridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)subjectsGrid).EndInit();
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            toolbarPanel.ResumeLayout(false);
            toolbarPanel.PerformLayout();
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private GroupBox formGroupBox;
        private Label subjectCodeLabel;
        private TextBox subjectCodeTextBox;
        private Label subjectNameLabel;
        private TextBox subjectNameTextBox;
        private Label yearLabel;
        private ComboBox yearComboBox;
        private Label semesterLabel;
        private ComboBox semesterComboBox;
        private Label creditsLabel;
        private NumericUpDown creditsNumeric;
        private Button addButton;
        private Panel gridPanel;
        private DataGridView subjectsGrid;
        private Panel filterPanel;
        private Label filterYearLabel;
        private ComboBox filterYearComboBox;
        private Label filterSemesterLabel;
        private ComboBox filterSemesterComboBox;
        private Label searchLabel;
        private TextBox searchTextBox;
        private Panel toolbarPanel;
        private Label countLabel;
        private Label statusLabel;
        private Button refreshButton;
        private Button deleteButton;
    }
}
