using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;
using FaceAttendanceSystem.Services;

namespace FaceAttendanceSystem.Forms
{
    public partial class UC_TakeAttendance : UserControl
    {
        private readonly CameraService _camera = new();
        private readonly System.Windows.Forms.Timer _previewTimer = new() { Interval = 40 };
        private readonly System.Windows.Forms.Timer _thankYouTimer = new() { Interval = 30 };
        private readonly StudentRepository _studentRepository = new();
        private readonly AttendanceRepository _attendanceRepository = new();
        private readonly SubjectRepository _subjectRepository = new();

        private FaceRecognitionService? _faceService;
        private List<Student> _students = new();
        private Subject? _selectedSubject;
        private readonly Lecturer _lecturer;
        private DateTime _lastRecognitionAttempt = DateTime.MinValue;
        private bool _isProcessingFrame;

        // "Ultra accurate" recognition: a student is only marked present after
        // being recognized 2 times in a row, not from a single (possibly noisy)
        // frame. This resets if a different face - or no face - is seen in between.
        private string? _pendingRegNumber;
        private int _pendingConfirmations;
        private const int RequiredConsecutiveMatches = 2;

        // Animation state for the "Thank you, please come next student" overlay.
        private bool _thankYouActive;
        private int _thankYouStep;
        private const int ThankYouTotalSteps = 130; // ~4 seconds at 30ms/step

        private class SubjectListItem
        {
            public string SubjectCode { get; set; } = string.Empty;
            public string SubjectName { get; set; } = string.Empty;
            public string DisplayText => $"{SubjectCode} - {SubjectName}";
        }

        public UC_TakeAttendance(Lecturer lecturer)
        {
            _lecturer = lecturer;
            InitializeComponent();
            filterYearComboBox.SelectedIndex = 0;
            filterSemesterComboBox.SelectedIndex = 0;
            LoadSubjectsIntoComboBox();
            thankYouPanel.Visible = false;
            downloadPdfButton.Enabled = false;
            _previewTimer.Tick += PreviewTimer_Tick;
            _thankYouTimer.Tick += ThankYouTimer_Tick;
            HandleCreated += (s, e) => StartCamera();
            HandleDestroyed += (s, e) => StopCamera();
            startButton.Click += StartButton_Click;
            downloadPdfButton.Click += DownloadPdfButton_Click;
            filterYearComboBox.SelectedIndexChanged += (s, e) => LoadSubjectsIntoComboBox();
            filterSemesterComboBox.SelectedIndexChanged += (s, e) => LoadSubjectsIntoComboBox();
        }

        private void LoadSubjectsIntoComboBox()
        {
            IEnumerable<Subject> subjects = _subjectRepository.LoadAll();

            if (filterYearComboBox.SelectedIndex > 0)
            {
                int year = filterYearComboBox.SelectedIndex; // index 1-4 maps to Year 1-4
                subjects = subjects.Where(s => s.Year == year);
            }

            if (filterSemesterComboBox.SelectedIndex > 0)
            {
                int semester = filterSemesterComboBox.SelectedIndex; // index 1-2 maps to Semester 1-2
                subjects = subjects.Where(s => s.Semester == semester);
            }

            List<Subject> orderedSubjects = subjects
                .OrderBy(s => s.Year).ThenBy(s => s.Semester).ThenBy(s => s.SubjectCode)
                .ToList();

            List<SubjectListItem> items = orderedSubjects
                .Select(s => new SubjectListItem { SubjectCode = s.SubjectCode, SubjectName = s.SubjectName })
                .ToList();

            subjectComboBox.DataSource = null;
            subjectComboBox.DisplayMember = "DisplayText";
            subjectComboBox.DataSource = items;
            subjectComboBox.Text = string.Empty;

            // Lets the lecturer just start typing the subject code or name to
            // narrow the list, instead of scrolling through every subject.
            var autoComplete = new AutoCompleteStringCollection();
            autoComplete.AddRange(items.Select(i => i.DisplayText).ToArray());
            subjectComboBox.AutoCompleteCustomSource = autoComplete;

            if (orderedSubjects.Count == 0)
            {
                statusLabel.Text = "No subjects match this filter. Try 'All Years' / 'Both Sem.', or add subjects in 'Manage Subjects'.";
                statusLabel.ForeColor = Color.Firebrick;
                startButton.Enabled = false;
            }
            else
            {
                startButton.Enabled = true;
            }
        }

        private void StartButton_Click(object? sender, EventArgs e)
        {
            SubjectListItem? selected = subjectComboBox.SelectedItem as SubjectListItem;

            // With the searchable dropdown, SelectedItem can be null if the lecturer
            // typed the subject instead of clicking it - fall back to matching the
            // typed text against the list before giving up.
            if (selected == null && subjectComboBox.DataSource is List<SubjectListItem> items)
            {
                selected = items.FirstOrDefault(i =>
                    i.DisplayText.Equals(subjectComboBox.Text, StringComparison.OrdinalIgnoreCase));
            }

            if (selected == null)
            {
                statusLabel.Text = "Please select a subject / lecture first.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            _selectedSubject = new Subject { SubjectCode = selected.SubjectCode, SubjectName = selected.SubjectName };
            subjectComboBox.Enabled = false;
            startButton.Enabled = false;
            downloadPdfButton.Enabled = true;
            lectureLabel.Text = $"Taking attendance for: {selected.DisplayText}";
            statusLabel.Text = "Waiting for a face...";
            statusLabel.ForeColor = Color.FromArgb(141, 21, 58);
            UpdateJoinedCount();
        }

        private void UpdateJoinedCount()
        {
            if (_selectedSubject == null)
            {
                joinedCountLabel.Text = string.Empty;
                return;
            }

            int count = _attendanceRepository.GetByDate(DateTime.Today)
                .Count(r => r.SubjectCode.Equals(_selectedSubject.SubjectCode, StringComparison.OrdinalIgnoreCase));

            joinedCountLabel.Text = $"👥 {count} student(s) joined this lecture so far";
        }

        private void DownloadPdfButton_Click(object? sender, EventArgs e)
        {
            if (_selectedSubject == null)
            {
                return;
            }

            using var saveDialog = new SaveFileDialog
            {
                Filter = "PDF Document (*.pdf)|*.pdf",
                FileName = $"Attendance_{_selectedSubject.SubjectCode}_{DateTime.Today:yyyy-MM-dd}.pdf"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                AttendancePdfService.GenerateAndSave(saveDialog.FileName, _selectedSubject, _lecturer, DateTime.Today);
                statusLabel.Text = "Attendance sheet PDF saved successfully.";
                statusLabel.ForeColor = Color.FromArgb(30, 130, 30);
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Could not save PDF: {ex.Message}";
                statusLabel.ForeColor = Color.Firebrick;
            }
        }

        private void StartCamera()
        {
            _students = _studentRepository.LoadAll();

            if (_students.Count == 0)
            {
                statusLabel.Text = "No students registered yet. Please register students first.";
                statusLabel.ForeColor = Color.Firebrick;
            }

            if (!_camera.Start())
            {
                statusLabel.Text = "Webcam not found. Please connect a camera.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            try
            {
                _faceService = new FaceRecognitionService();
            }
            catch (Exception ex)
            {
                statusLabel.Text = ex.Message;
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            _previewTimer.Start();
        }

        private void StopCamera()
        {
            _previewTimer.Stop();
            _thankYouTimer.Stop();
            _camera.Stop();
            _faceService?.Dispose();
        }

        private void PreviewTimer_Tick(object? sender, EventArgs e)
        {
            Bitmap? frame = _camera.GrabFrame();
            if (frame == null)
            {
                return;
            }

            cameraPictureBox.Image?.Dispose();
            cameraPictureBox.Image = frame;

            // Only recognize once a subject/lecture has been selected, roughly twice
            // a second, never while a previous attempt is finishing, and never while
            // the "thank you" overlay is showing (gives the next student time to step up).
            if (_selectedSubject == null || _isProcessingFrame || _thankYouActive ||
                (DateTime.Now - _lastRecognitionAttempt).TotalMilliseconds < 500)
            {
                return;
            }

            _lastRecognitionAttempt = DateTime.Now;
            TryRecognizeAndMark(frame);
        }

        private void TryRecognizeAndMark(Bitmap frame)
        {
            if (_faceService == null || _students.Count == 0 || _selectedSubject == null)
            {
                return;
            }

            _isProcessingFrame = true;
            try
            {
                using var snapshot = new Bitmap(frame);
                double[]? encoding = _faceService.GetFaceEncoding(snapshot);

                if (encoding == null)
                {
                    _pendingRegNumber = null;
                    _pendingConfirmations = 0;
                    return;
                }

                (Student? student, double distance) = _faceService.FindBestMatch(encoding, _students);

                if (student == null)
                {
                    statusLabel.Text = "Face not recognized.";
                    statusLabel.ForeColor = Color.Firebrick;
                    _pendingRegNumber = null;
                    _pendingConfirmations = 0;
                    return;
                }

                if (_attendanceRepository.AlreadyMarkedToday(student.RegNumber, _selectedSubject.SubjectCode))
                {
                    statusLabel.Text = $"{student.FullName} is already marked present for this lecture today.";
                    statusLabel.ForeColor = Color.FromArgb(212, 160, 23);
                    _pendingRegNumber = null;
                    _pendingConfirmations = 0;
                    return;
                }

                // Require the SAME student to be recognized on consecutive attempts
                // before actually marking them present - a single lucky/unlucky
                // frame is not enough on its own.
                if (_pendingRegNumber == student.RegNumber)
                {
                    _pendingConfirmations++;
                }
                else
                {
                    _pendingRegNumber = student.RegNumber;
                    _pendingConfirmations = 1;
                }

                if (_pendingConfirmations < RequiredConsecutiveMatches)
                {
                    statusLabel.Text = $"Recognizing {student.FullName}... hold still.";
                    statusLabel.ForeColor = Color.FromArgb(212, 160, 23);
                    return;
                }

                _pendingRegNumber = null;
                _pendingConfirmations = 0;

                _attendanceRepository.Add(new AttendanceRecord
                {
                    RegNumber = student.RegNumber,
                    FullName = student.FullName,
                    ClassName = student.ClassName,
                    Course = student.Course,
                    SubjectCode = _selectedSubject.SubjectCode,
                    SubjectName = _selectedSubject.SubjectName,
                    Date = DateTime.Today,
                    TimeMarked = DateTime.Now,
                    Status = "Present"
                });

                UpdateJoinedCount();
                ShowThankYouAnimation(student.FullName);

                // Fire-and-forget: a slow/misconfigured mail server should never
                // block or freeze attendance marking for the next student.
                _ = EmailService.SendStudentAttendanceEmailAsync(student, _selectedSubject.SubjectName);
            }
            finally
            {
                _isProcessingFrame = false;
            }
        }

        // Shows a friendly "Take Your Attend, Thank You! Please come next student."
        // overlay that gently pops in over the camera view for a few seconds.
        private void ShowThankYouAnimation(string studentName)
        {
            thankYouNameLabel.Text = $"✅ {studentName}";
            _thankYouStep = 0;
            _thankYouActive = true;

            // Position/width always match the live camera view exactly, computed
            // fresh here rather than relying on anchor math against a container
            // whose size can change with DPI/window size.
            thankYouPanel.Width = cameraPictureBox.Width;
            thankYouPanel.Left = cameraPictureBox.Left;
            thankYouPanel.Height = 0;
            thankYouPanel.Top = cameraPictureBox.Bottom;
            thankYouPanel.Visible = true;
            thankYouPanel.BringToFront();
            _thankYouTimer.Start();
        }

        private void ThankYouTimer_Tick(object? sender, EventArgs e)
        {
            _thankYouStep++;

            int maxHeight = (int)(cameraPictureBox.Height * 0.35);

            // Ease-in for the first 15 steps (a subtle "pop" appearance), hold, then fade out.
            if (_thankYouStep <= 15)
            {
                double progress = _thankYouStep / 15.0;
                thankYouPanel.Height = (int)(maxHeight * progress);
            }
            else if (_thankYouStep >= ThankYouTotalSteps - 15)
            {
                double progress = (ThankYouTotalSteps - _thankYouStep) / 15.0;
                thankYouPanel.Height = (int)(maxHeight * Math.Max(progress, 0));
            }
            else
            {
                thankYouPanel.Height = maxHeight;
            }

            // Keep the overlay glued to the bottom edge of the camera view as it grows.
            thankYouPanel.Top = cameraPictureBox.Bottom - thankYouPanel.Height;
            thankYouPanel.Width = cameraPictureBox.Width;
            thankYouPanel.Left = cameraPictureBox.Left;

            if (_thankYouStep >= ThankYouTotalSteps)
            {
                _thankYouTimer.Stop();
                _thankYouActive = false;
                thankYouPanel.Visible = false;
                statusLabel.Text = "Waiting for a face...";
                statusLabel.ForeColor = Color.FromArgb(141, 21, 58);
            }
        }
    }
}
