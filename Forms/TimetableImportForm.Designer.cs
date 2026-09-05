namespace FaceAttendanceSystem.Forms
{
    partial class TimetableImportForm
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
            headerLabel = new Label();
            toolbarPanel = new Panel();
            chooseFileButton = new Button();
            extractButton = new Button();
            selectedFileLabel = new Label();
            resultsGrid = new DataGridView();
            bottomPanel = new Panel();
            statusLabel = new Label();
            importButton = new Button();
            closeButton = new Button();
            mainLayout.SuspendLayout();
            toolbarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)resultsGrid).BeginInit();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(245, 245, 245);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 4;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(20);
            mainLayout.Controls.Add(headerLabel, 0, 0);
            mainLayout.Controls.Add(toolbarPanel, 0, 1);
            mainLayout.Controls.Add(resultsGrid, 0, 2);
            mainLayout.Controls.Add(bottomPanel, 0, 3);
            mainLayout.Name = "mainLayout";
            //
            // headerLabel
            //
            headerLabel.AutoSize = true;
            headerLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            headerLabel.ForeColor = Color.FromArgb(141, 21, 58);
            headerLabel.Name = "headerLabel";
            headerLabel.Text = "📷 Import Timetable from Photo or PDF";
            //
            // toolbarPanel
            //
            toolbarPanel.Controls.Add(selectedFileLabel);
            toolbarPanel.Controls.Add(extractButton);
            toolbarPanel.Controls.Add(chooseFileButton);
            toolbarPanel.Dock = DockStyle.Fill;
            toolbarPanel.Name = "toolbarPanel";
            //
            // chooseFileButton
            //
            chooseFileButton.BackColor = Color.FromArgb(141, 21, 58);
            chooseFileButton.Cursor = Cursors.Hand;
            chooseFileButton.FlatStyle = FlatStyle.Flat;
            chooseFileButton.FlatAppearance.BorderSize = 0;
            chooseFileButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            chooseFileButton.ForeColor = Color.White;
            chooseFileButton.Location = new Point(0, 8);
            chooseFileButton.Name = "chooseFileButton";
            chooseFileButton.Size = new Size(180, 34);
            chooseFileButton.Text = "📁 Choose Photo / PDF";
            chooseFileButton.UseVisualStyleBackColor = false;
            //
            // extractButton
            //
            extractButton.BackColor = Color.FromArgb(212, 160, 23);
            extractButton.Cursor = Cursors.Hand;
            extractButton.FlatStyle = FlatStyle.Flat;
            extractButton.FlatAppearance.BorderSize = 0;
            extractButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            extractButton.ForeColor = Color.White;
            extractButton.Location = new Point(195, 8);
            extractButton.Name = "extractButton";
            extractButton.Size = new Size(170, 34);
            extractButton.Text = "🔍 Extract Timetable";
            extractButton.UseVisualStyleBackColor = false;
            //
            // selectedFileLabel
            //
            selectedFileLabel.AutoSize = true;
            selectedFileLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            selectedFileLabel.ForeColor = Color.DimGray;
            selectedFileLabel.Location = new Point(380, 17);
            selectedFileLabel.Name = "selectedFileLabel";
            selectedFileLabel.Text = "No file selected.";
            //
            // resultsGrid
            //
            resultsGrid.AllowUserToAddRows = false;
            resultsGrid.AllowUserToDeleteRows = false;
            resultsGrid.BackgroundColor = Color.White;
            resultsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(141, 21, 58);
            resultsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            resultsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            resultsGrid.Dock = DockStyle.Fill;
            resultsGrid.Font = new Font("Segoe UI", 9F);
            resultsGrid.Name = "resultsGrid";
            resultsGrid.RowHeadersVisible = false;
            resultsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // bottomPanel
            //
            bottomPanel.Controls.Add(closeButton);
            bottomPanel.Controls.Add(importButton);
            bottomPanel.Controls.Add(statusLabel);
            bottomPanel.Dock = DockStyle.Fill;
            bottomPanel.Name = "bottomPanel";
            //
            // statusLabel
            //
            statusLabel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            statusLabel.Location = new Point(0, 5);
            statusLabel.MaximumSize = new Size(650, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(650, 60);
            statusLabel.Text = "Choose a clear photo or a text-based PDF of the timetable to begin.";
            //
            // importButton
            //
            importButton.BackColor = Color.FromArgb(30, 130, 30);
            importButton.Cursor = Cursors.Hand;
            importButton.FlatStyle = FlatStyle.Flat;
            importButton.FlatAppearance.BorderSize = 0;
            importButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            importButton.ForeColor = Color.White;
            importButton.Location = new Point(680, 15);
            importButton.Name = "importButton";
            importButton.Size = new Size(180, 40);
            importButton.Text = "✅ Import Selected";
            importButton.UseVisualStyleBackColor = false;
            //
            // closeButton
            //
            closeButton.BackColor = Color.Gray;
            closeButton.Cursor = Cursors.Hand;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(870, 15);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(100, 40);
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = false;
            //
            // TimetableImportForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 620);
            Controls.Add(mainLayout);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TimetableImportForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Import Timetable from Photo or PDF";
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            toolbarPanel.ResumeLayout(false);
            toolbarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)resultsGrid).EndInit();
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private Label headerLabel;
        private Panel toolbarPanel;
        private Button chooseFileButton;
        private Button extractButton;
        private Label selectedFileLabel;
        private DataGridView resultsGrid;
        private Panel bottomPanel;
        private Label statusLabel;
        private Button importButton;
        private Button closeButton;
    }
}
