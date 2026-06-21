using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartCampusAI.Data;
using SmartCampusAI.Models;
using SmartCampusAI.Services;
using SmartCampusAI.Styles;
using SmartCampusAI.Utils;

namespace SmartCampusAI.Forms
{
    public class AttendanceForm : Form
    {
        private DataGridView _dgv           = new();
        private DateTimePicker _dtpStart    = new();
        private DateTimePicker _dtpEnd      = new();
        private Button _btnFilter           = new();
        private Button _btnToday            = new();
        private Button _btnManual           = new();
        private Label _lblCount             = new();
        private ComboBox _cboStudentFilter  = new();

        // Manual panel
        private Panel _manualPanel          = new();
        private ComboBox _cboManualStudent  = new();
        private DateTimePicker _dtpManual   = new();
        private ComboBox _cboManualStatus   = new();
        private Button _btnMarkManual       = new();
        private Label _lblManualStatus      = new();

        private readonly AttendanceRepository _repo     = new();
        private readonly StudentRepository _studentRepo  = new();
        private readonly AttendanceService _service      = new();

        public AttendanceForm()
        {
            InitializeComponent();
            LoadStudentsIntoCombo();
            LoadToday();
        }

        private void InitializeComponent()
        {
            this.Text          = "Attendance Report";
            this.Size          = new Size(1050, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = ColorPalette.LightGray;

            // Top bar
            Panel topBar = new() { Dock = DockStyle.Top, Height = 55, BackColor = ColorPalette.NavyBlue, Padding = new Padding(10, 8, 10, 0) };
            Label lblTitle = new() { Text = "📅 Attendance Report", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(15, 14) };
            topBar.Controls.Add(lblTitle);

            // Filter panel
            Panel filterPanel = new() { Dock = DockStyle.Top, Height = 60, BackColor = Color.White, Padding = new Padding(10, 8, 10, 8) };

            Label l1 = MakeLbl("From:", new Point(10, 22));
            _dtpStart.Location = new Point(60, 18); _dtpStart.Size = new Size(140, 28); _dtpStart.Format = DateTimePickerFormat.Short; _dtpStart.Value = DateTime.Today.AddDays(-7);

            Label l2 = MakeLbl("To:", new Point(210, 22));
            _dtpEnd.Location = new Point(240, 18); _dtpEnd.Size = new Size(140, 28); _dtpEnd.Format = DateTimePickerFormat.Short;

            _btnFilter.Text = "🔍 Filter"; _btnFilter.Location = new Point(390, 15); _btnFilter.Size = new Size(90, 30); Theme.StylePrimaryButton(_btnFilter); _btnFilter.Click += (s, e) => LoadByRange();

            _btnToday.Text = "📅 Today"; _btnToday.Location = new Point(490, 15); _btnToday.Size = new Size(85, 30); Theme.StyleSuccessButton(_btnToday); _btnToday.Click += (s, e) => LoadToday();

            _btnManual.Text = "✏ Manual Entry"; _btnManual.Location = new Point(585, 15); _btnManual.Size = new Size(130, 30); Theme.StyleWarningButton(_btnManual); _btnManual.Click += ToggleManualPanel;

            _lblCount.Location = new Point(730, 20); _lblCount.AutoSize = true; _lblCount.Font = new Font("Segoe UI", 10, FontStyle.Bold); _lblCount.ForeColor = ColorPalette.NavyBlue;

            filterPanel.Controls.AddRange(new Control[] { l1, _dtpStart, l2, _dtpEnd, _btnFilter, _btnToday, _btnManual, _lblCount });

            // Manual panel (hidden initially)
            _manualPanel.Dock = DockStyle.Top; _manualPanel.Height = 70; _manualPanel.BackColor = ColorPalette.LightBlue; _manualPanel.Padding = new Padding(10, 8, 10, 8); _manualPanel.Visible = false;

            Label lm1 = MakeLbl("Student:", new Point(10, 22));
            _cboManualStudent.Location = new Point(75, 18); _cboManualStudent.Size = new Size(200, 28); _cboManualStudent.DropDownStyle = ComboBoxStyle.DropDownList; _cboManualStudent.Font = Theme.InputFont;

            Label lm2 = MakeLbl("Date:", new Point(285, 22));
            _dtpManual.Location = new Point(325, 18); _dtpManual.Size = new Size(130, 28); _dtpManual.Format = DateTimePickerFormat.Short;

            Label lm3 = MakeLbl("Status:", new Point(465, 22));
            _cboManualStatus.Location = new Point(515, 18); _cboManualStatus.Size = new Size(100, 28); _cboManualStatus.DropDownStyle = ComboBoxStyle.DropDownList; _cboManualStatus.Font = Theme.InputFont;
            _cboManualStatus.Items.AddRange(new object[] { "Present", "Absent", "Late" }); _cboManualStatus.SelectedIndex = 0;

            _btnMarkManual.Text = "✅ Mark"; _btnMarkManual.Location = new Point(625, 15); _btnMarkManual.Size = new Size(85, 30); Theme.StyleSuccessButton(_btnMarkManual); _btnMarkManual.Click += BtnMarkManual_Click;

            _lblManualStatus.Location = new Point(720, 22); _lblManualStatus.AutoSize = true; _lblManualStatus.Font = new Font("Segoe UI", 9);

            _manualPanel.Controls.AddRange(new Control[] { lm1, _cboManualStudent, lm2, _dtpManual, lm3, _cboManualStatus, _btnMarkManual, _lblManualStatus });

            // Grid
            Panel gridPanel = new() { Dock = DockStyle.Fill, Padding = new Padding(10), BackColor = ColorPalette.LightGray };
            _dgv.Dock = DockStyle.Fill;
            Theme.StyleDataGrid(_dgv);
            gridPanel.Controls.Add(_dgv);

            this.Controls.Add(gridPanel);
            this.Controls.Add(_manualPanel);
            this.Controls.Add(filterPanel);
            this.Controls.Add(topBar);
        }

        private static Label MakeLbl(string text, Point loc) =>
            new() { Text = text, Font = Theme.LabelFont, AutoSize = true, Location = loc };

        private void LoadStudentsIntoCombo()
        {
            try
            {
                var students = _studentRepo.GetAll();
                _cboManualStudent.Items.Clear();
                foreach (var s in students)
                    _cboManualStudent.Items.Add(new ComboItem(s.StudentID, s.FullName));
                if (_cboManualStudent.Items.Count > 0) _cboManualStudent.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadToday()
        {
            try
            {
                var list = _repo.GetToday();
                _dgv.DataSource = list;
                _lblCount.Text  = $"Records: {list.Count}";
                HideCols();
            }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void LoadByRange()
        {
            try
            {
                var list = _repo.GetByDateRange(_dtpStart.Value, _dtpEnd.Value);
                _dgv.DataSource = list;
                _lblCount.Text  = $"Records: {list.Count}";
                HideCols();
            }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private void HideCols()
        {
            foreach (string col in new[] { "AttendanceID", "StudentID" })
                if (_dgv.Columns.Contains(col)) _dgv.Columns[col]!.Visible = false;
        }

        private void ToggleManualPanel(object? sender, EventArgs e)
        {
            _manualPanel.Visible = !_manualPanel.Visible;
        }

        private void BtnMarkManual_Click(object? sender, EventArgs e)
        {
            if (_cboManualStudent.SelectedItem is not ComboItem item) { _lblManualStatus.Text = "Select a student."; return; }
            try
            {
                var (ok, msg) = _service.ManualMark(item.Id, _dtpManual.Value, _cboManualStatus.Text);
                _lblManualStatus.Text      = msg;
                _lblManualStatus.ForeColor = ok ? ColorPalette.Success : ColorPalette.Danger;
                if (ok) LoadByRange();
            }
            catch (Exception ex) { Helpers.ShowError(ex.Message); }
        }

        private class ComboItem
        {
            public int Id { get; }
            private string Name { get; }
            public ComboItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => $"[{Id}] {Name}";
        }
    }
}
