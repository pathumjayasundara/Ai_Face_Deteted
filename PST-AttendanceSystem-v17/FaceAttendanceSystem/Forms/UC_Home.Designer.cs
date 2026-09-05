namespace FaceAttendanceSystem.Forms
{
    partial class UC_Home
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
            welcomeHeaderPanel = new TableLayoutPanel();
            welcomeLabel = new Label();
            logoPictureBox = new PictureBox();
            slideshowPanel = new FaceAttendanceSystem.Controls.PhotoSlideshowControl();
            slideshowLabel = new Label();
            studentsListPanel = new Panel();
            studentsListTitleLabel = new Label();
            studentsListGrid = new DataGridView();
            mainLayout.SuspendLayout();
            welcomeHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            studentsListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)studentsListGrid).BeginInit();
            SuspendLayout();
            //
            // mainLayout
            //
            mainLayout.BackColor = Color.FromArgb(245, 245, 245);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 18F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 42F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Controls.Add(welcomeHeaderPanel, 0, 0);
            mainLayout.Controls.Add(slideshowPanel, 0, 1);
            mainLayout.Controls.Add(studentsListPanel, 0, 2);
            mainLayout.Name = "mainLayout";
            //
            // welcomeHeaderPanel
            //
            welcomeHeaderPanel.ColumnCount = 2;
            welcomeHeaderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            welcomeHeaderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            welcomeHeaderPanel.RowCount = 1;
            welcomeHeaderPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            welcomeHeaderPanel.Controls.Add(welcomeLabel, 0, 0);
            welcomeHeaderPanel.Controls.Add(logoPictureBox, 1, 0);
            welcomeHeaderPanel.Dock = DockStyle.Fill;
            welcomeHeaderPanel.Name = "welcomeHeaderPanel";
            //
            // logoPictureBox
            //
            logoPictureBox.Anchor = AnchorStyles.None;
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(150, 150);
            logoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoPictureBox.TabStop = false;
            //
            // welcomeLabel
            //
            welcomeLabel.Anchor = AnchorStyles.Left;
            welcomeLabel.AutoSize = true;
            welcomeLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            welcomeLabel.ForeColor = Color.FromArgb(141, 21, 58);
            welcomeLabel.Margin = new Padding(40, 0, 0, 0);
            welcomeLabel.Name = "welcomeLabel";
            welcomeLabel.Text = "Welcome!\nDepartment of Physical Science and Technology";
            welcomeLabel.TextAlign = ContentAlignment.MiddleLeft;
            //
            // slideshowPanel
            //
            slideshowPanel.BackColor = Color.Black;
            slideshowPanel.Controls.Add(slideshowLabel);
            slideshowPanel.Dock = DockStyle.Fill;
            slideshowPanel.Margin = new Padding(0, 5, 0, 15);
            slideshowPanel.Name = "slideshowPanel";
            //
            // slideshowLabel
            //
            slideshowLabel.BackColor = Color.FromArgb(140, 0, 0, 0);
            slideshowLabel.Dock = DockStyle.Bottom;
            slideshowLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            slideshowLabel.ForeColor = Color.White;
            slideshowLabel.Height = 42;
            slideshowLabel.Name = "slideshowLabel";
            slideshowLabel.Text = "Sabaragamuwa University of Sri Lanka";
            slideshowLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // studentsListPanel
            //
            studentsListPanel.BackColor = Color.White;
            studentsListPanel.Controls.Add(studentsListGrid);
            studentsListPanel.Controls.Add(studentsListTitleLabel);
            studentsListPanel.Dock = DockStyle.Fill;
            studentsListPanel.Margin = new Padding(60, 0, 60, 20);
            studentsListPanel.Name = "studentsListPanel";
            studentsListPanel.Padding = new Padding(15);
            //
            // studentsListTitleLabel
            //
            studentsListTitleLabel.Dock = DockStyle.Top;
            studentsListTitleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            studentsListTitleLabel.ForeColor = Color.FromArgb(141, 21, 58);
            studentsListTitleLabel.Height = 34;
            studentsListTitleLabel.Name = "studentsListTitleLabel";
            studentsListTitleLabel.Text = "Registered Students";
            //
            // studentsListGrid
            //
            studentsListGrid.AllowUserToAddRows = false;
            studentsListGrid.AllowUserToDeleteRows = false;
            studentsListGrid.BackgroundColor = Color.White;
            studentsListGrid.BorderStyle = BorderStyle.None;
            studentsListGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 235, 240);
            studentsListGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(141, 21, 58);
            studentsListGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            studentsListGrid.Dock = DockStyle.Fill;
            studentsListGrid.Font = new Font("Segoe UI", 9.5F);
            studentsListGrid.Name = "studentsListGrid";
            studentsListGrid.ReadOnly = true;
            studentsListGrid.RowHeadersVisible = false;
            studentsListGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            // UC_Home
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            Controls.Add(mainLayout);
            Name = "UC_Home";
            Size = new Size(1280, 675);
            mainLayout.ResumeLayout(false);
            welcomeHeaderPanel.ResumeLayout(false);
            welcomeHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            studentsListPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)studentsListGrid).EndInit();
            ResumeLayout(false);
        }

        private TableLayoutPanel mainLayout;
        private TableLayoutPanel welcomeHeaderPanel;
        private PictureBox logoPictureBox;
        private Label welcomeLabel;
        private FaceAttendanceSystem.Controls.PhotoSlideshowControl slideshowPanel;
        private Label slideshowLabel;
        private Panel studentsListPanel;
        private Label studentsListTitleLabel;
        private DataGridView studentsListGrid;
    }
}
