using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;
using FaceAttendanceSystem.Services;

namespace FaceAttendanceSystem.Forms
{
    public partial class TimetableImportForm : Form
    {
        private readonly TimetableRepository _timetableRepository = new();
        private List<TimetableEntry> _candidates = new();
        private string? _selectedFilePath;

        public bool AnyEntriesImported { get; private set; }

        public TimetableImportForm()
        {
            InitializeComponent();
            chooseFileButton.Click += ChooseFileButton_Click;
            extractButton.Click += ExtractButton_Click;
            importButton.Click += ImportButton_Click;
            closeButton.Click += (s, e) => Close();
            extractButton.Enabled = false;
            importButton.Enabled = false;
        }

        private void ChooseFileButton_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Timetable Photo or PDF|*.jpg;*.jpeg;*.png;*.bmp;*.pdf|All Files|*.*",
                Title = "Select a Timetable Photo or PDF"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _selectedFilePath = dialog.FileName;
            selectedFileLabel.Text = $"Selected: {Path.GetFileName(_selectedFilePath)}";
            extractButton.Enabled = true;
            statusLabel.Text = "File selected. Click 'Extract Timetable' to continue.";
            statusLabel.ForeColor = Color.DimGray;
        }

        private async void ExtractButton_Click(object? sender, EventArgs e)
        {
            if (_selectedFilePath == null)
            {
                return;
            }

            extractButton.Enabled = false;
            chooseFileButton.Enabled = false;
            Cursor = Cursors.WaitCursor;
            statusLabel.Text = "Reading the file and detecting text... this can take a little while for photos.";
            statusLabel.ForeColor = Color.FromArgb(141, 21, 58);

            try
            {
                string extractedText = await TimetableExtractionService.ExtractTextAsync(_selectedFilePath);
                _candidates = TimetableExtractionService.ParseCandidateEntries(extractedText);

                if (_candidates.Count == 0)
                {
                    statusLabel.Text = "No subject codes (e.g. 'PST 21202') were found in this file. " +
                        "Please check the photo is clear and readable, or add classes manually instead.";
                    statusLabel.ForeColor = Color.Firebrick;
                }
                else
                {
                    statusLabel.Text = $"Found {_candidates.Count} possible class(es) below. " +
                        "Please check/edit the Day, Time and Room for each before importing - " +
                        "automatic detection from a photo is not always perfect.";
                    statusLabel.ForeColor = Color.FromArgb(30, 130, 30);
                    importButton.Enabled = true;
                }

                RefreshGrid();
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Couldn't read this file: {ex.Message}";
                statusLabel.ForeColor = Color.Firebrick;
            }
            finally
            {
                Cursor = Cursors.Default;
                chooseFileButton.Enabled = true;
                extractButton.Enabled = true;
            }
        }

        private void RefreshGrid()
        {
            resultsGrid.DataSource = _candidates
                .Select(c => new
                {
                    Include = true,
                    c.SubjectCode,
                    c.SubjectName,
                    Day = c.Day.ToString(),
                    c.StartTime,
                    c.EndTime,
                    c.Room,
                    c.Year,
                    c.Semester
                })
                .ToList();

            if (resultsGrid.Columns["Include"] != null)
            {
                // Make sure the checkbox column is actually editable/checkable.
                resultsGrid.Columns["Include"].DisplayIndex = 0;
            }
        }

        private void ImportButton_Click(object? sender, EventArgs e)
        {
            int importedCount = 0;

            for (int i = 0; i < resultsGrid.Rows.Count; i++)
            {
                DataGridViewRow row = resultsGrid.Rows[i];
                bool include = row.Cells["Include"].Value is bool b && b;
                if (!include || i >= _candidates.Count)
                {
                    continue;
                }

                TimetableEntry candidate = _candidates[i];

                // Pick up any edits the lecturer made directly in the grid.
                if (Enum.TryParse(row.Cells["Day"].Value?.ToString(), ignoreCase: true, out DayOfWeek day))
                {
                    candidate.Day = day;
                }
                candidate.StartTime = row.Cells["StartTime"].Value?.ToString() ?? candidate.StartTime;
                candidate.EndTime = row.Cells["EndTime"].Value?.ToString() ?? candidate.EndTime;
                candidate.Room = row.Cells["Room"].Value?.ToString() ?? candidate.Room;
                candidate.SubjectCode = row.Cells["SubjectCode"].Value?.ToString() ?? candidate.SubjectCode;
                candidate.SubjectName = row.Cells["SubjectName"].Value?.ToString() ?? candidate.SubjectName;

                if (int.TryParse(row.Cells["Year"].Value?.ToString(), out int year))
                {
                    candidate.Year = year;
                }
                if (int.TryParse(row.Cells["Semester"].Value?.ToString(), out int semester))
                {
                    candidate.Semester = semester;
                }

                _timetableRepository.Add(candidate);
                importedCount++;
            }

            if (importedCount == 0)
            {
                statusLabel.Text = "No rows were selected to import. Tick the 'Include' box for the classes you want to add.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            AnyEntriesImported = true;
            statusLabel.Text = $"✅ Imported {importedCount} class(es) into the timetable.";
            statusLabel.ForeColor = Color.FromArgb(30, 130, 30);
            importButton.Enabled = false;
        }
    }
}
