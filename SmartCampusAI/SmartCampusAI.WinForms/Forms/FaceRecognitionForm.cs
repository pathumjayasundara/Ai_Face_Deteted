using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using SmartCampusAI.Data;
using SmartCampusAI.Models;
using SmartCampusAI.Services;
using SmartCampusAI.Styles;
using SmartCampusAI.Utils;

namespace SmartCampusAI.Forms
{
    public class FaceRecognitionForm : Form
    {
        // ── Camera preview ───────────────────────────────────────────────────
        private PictureBox _pbCamera         = new();
        private Button _btnStartCamera        = new();
        private Button _btnStopCamera         = new();

        // ── Mode buttons ─────────────────────────────────────────────────────
        private Button _btnRegisterMode       = new();
        private Button _btnRecognizeMode      = new();
        private Button _btnTrainModel         = new();

        // ── Registration ─────────────────────────────────────────────────────
        private Panel _regPanel               = new();
        private ComboBox _cboStudents         = new();
        private Label _lblSampleCount         = new();
        private Button _btnCaptureSample      = new();
        private Button _btnClearFaceData      = new();
        private ProgressBar _pbSamples        = new();

        // ── Recognition result ────────────────────────────────────────────────
        private Panel _resultPanel            = new();
        private Label _lblStudentName         = new();
        private Label _lblConfidence          = new();
        private Label _lblAttendanceStatus    = new();
        private Button _btnManualFallback     = new();

        // ── Status ────────────────────────────────────────────────────────────
        private Label _lblSystemStatus        = new();
        private Label _lblCameraIndex         = new();
        private NumericUpDown _nudCameraIdx   = new();

        // ── Services & state ──────────────────────────────────────────────────
        private readonly CameraService _camera          = new();
        private readonly FaceRecognitionService _faceAI = new();
        private readonly FaceRepository _faceRepo       = new();
        private readonly StudentRepository _studentRepo  = new();
        private readonly AttendanceService _attendanceSvc = new();

        private enum AppMode { Idle, Registering, Recognizing }
        private AppMode _mode = AppMode.Idle;

        private int _capturedSamples = 0;
        private const int RequiredSamples = 20;
        private int _selectedStudentId   = 0;
        private bool _recognizerTrained  = false;

        // Used for recognition de-bounce: don't mark attendance twice in one session
        private readonly HashSet<int> _markedThisSession = new();

        // Thread-safe UI delegate
        private delegate void SafeUpdateUI(Action action);

        public FaceRecognitionForm()
        {
            InitializeComponent();
            _faceAI.Initialize();
            LoadStudentsCombo();
            UpdateSystemStatus("✅ AI Engine ready. Start camera to begin.", ColorPalette.Success);
        }

        private void InitializeComponent()
        {
            this.Text          = "🤖 AI Face Recognition – Smart Attendance";
            this.Size          = new Size(1150, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor     = ColorPalette.NavyBlue;
            this.FormClosing   += FaceForm_Closing;

            // ── TOP BAR ───────────────────────────────────────────────────────
            Panel topBar = new() { Dock = DockStyle.Top, Height = 55, BackColor = ColorPalette.NavyBlue };
            Label lblTitle = new() { Text = "🤖 AI Face Recognition – Smart Attendance System", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(15, 14) };
            topBar.Controls.Add(lblTitle);

            // ── LEFT: CAMERA PANEL ────────────────────────────────────────────
            Panel leftPanel = new() { Width = 680, Dock = DockStyle.Left, BackColor = ColorPalette.NavyBlue, Padding = new Padding(10) };

            _pbCamera.Location  = new Point(10, 10);
            _pbCamera.Size      = new Size(656, 492);
            _pbCamera.BackColor = Color.Black;
            _pbCamera.SizeMode  = PictureBoxSizeMode.StretchImage;
            _pbCamera.BorderStyle = BorderStyle.FixedSingle;

            // Camera controls row
            Panel camCtrl = new() { Location = new Point(10, 510), Size = new Size(656, 45), BackColor = ColorPalette.NavyBlue };

            Label lblIdx = new() { Text = "Camera:", ForeColor = Color.White, Font = new Font("Segoe UI", 9), AutoSize = true, Location = new Point(0, 12) };
            _nudCameraIdx.Location = new Point(62, 9); _nudCameraIdx.Size = new Size(50, 26); _nudCameraIdx.Minimum = 0; _nudCameraIdx.Maximum = 5; _nudCameraIdx.Value = 0; _nudCameraIdx.Font = Theme.InputFont;

            _btnStartCamera.Text = "▶ Start Camera"; _btnStartCamera.Location = new Point(122, 5); _btnStartCamera.Size = new Size(140, 36); Theme.StyleSuccessButton(_btnStartCamera); _btnStartCamera.Click += BtnStart_Click;
            _btnStopCamera.Text = "⬛ Stop Camera"; _btnStopCamera.Location = new Point(272, 5); _btnStopCamera.Size = new Size(140, 36); Theme.StyleDangerButton(_btnStopCamera); _btnStopCamera.Enabled = false; _btnStopCamera.Click += BtnStop_Click;

            camCtrl.Controls.AddRange(new Control[] { lblIdx, _nudCameraIdx, _btnStartCamera, _btnStopCamera });

            // Status bar
            _lblSystemStatus.Location  = new Point(10, 558);
            _lblSystemStatus.Size      = new Size(656, 32);
            _lblSystemStatus.Font      = new Font("Segoe UI", 9, FontStyle.Bold);
            _lblSystemStatus.ForeColor = ColorPalette.Success;
            _lblSystemStatus.BackColor = Color.FromArgb(30, 30, 50);
            _lblSystemStatus.TextAlign = ContentAlignment.MiddleLeft;
            _lblSystemStatus.Padding   = new Padding(5, 0, 0, 0);

            leftPanel.Controls.AddRange(new Control[] { _pbCamera, camCtrl, _lblSystemStatus });

            // ── RIGHT: CONTROL PANEL ──────────────────────────────────────────
            Panel rightPanel = new() { Dock = DockStyle.Fill, BackColor = Color.FromArgb(20, 30, 55), Padding = new Padding(15) };

            // Mode buttons
            Label lblMode = new() { Text = "Select Mode", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(15, 15) };

            _btnRegisterMode.Text = "📸 Register Face"; _btnRegisterMode.Location = new Point(15, 45); _btnRegisterMode.Size = new Size(195, 40); Theme.StylePrimaryButton(_btnRegisterMode); _btnRegisterMode.Click += (s, e) => SetMode(AppMode.Registering);
            _btnRecognizeMode.Text = "🔍 Recognize Face"; _btnRecognizeMode.Location = new Point(15, 95); _btnRecognizeMode.Size = new Size(195, 40); Theme.StyleSuccessButton(_btnRecognizeMode); _btnRecognizeMode.Click += (s, e) => SetMode(AppMode.Recognizing);
            _btnTrainModel.Text = "🧠 Train AI Model"; _btnTrainModel.Location = new Point(15, 145); _btnTrainModel.Size = new Size(195, 40); Theme.StyleWarningButton(_btnTrainModel); _btnTrainModel.Click += BtnTrain_Click;

            // ── REGISTRATION PANEL ────────────────────────────────────────────
            _regPanel.Location  = new Point(10, 205);
            _regPanel.Size      = new Size(420, 200);
            _regPanel.BackColor = Color.FromArgb(30, 45, 80);
            _regPanel.Visible   = false;

            Label lblRegTitle = new() { Text = "Face Registration", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(10, 10) };
            Label lblStuLabel = new() { Text = "Select Student:", ForeColor = Color.LightGray, AutoSize = true, Location = new Point(10, 40) };

            _cboStudents.Location = new Point(10, 60); _cboStudents.Size = new Size(390, 28); _cboStudents.DropDownStyle = ComboBoxStyle.DropDownList; _cboStudents.Font = Theme.InputFont; _cboStudents.BackColor = Color.White;
            _cboStudents.SelectedIndexChanged += CboStudents_Changed;

            _lblSampleCount.Location = new Point(10, 98); _lblSampleCount.AutoSize = true; _lblSampleCount.ForeColor = Color.LightYellow; _lblSampleCount.Font = new Font("Segoe UI", 9);

            _pbSamples.Location = new Point(10, 118); _pbSamples.Size = new Size(390, 16); _pbSamples.Maximum = RequiredSamples; _pbSamples.Value = 0; _pbSamples.ForeColor = ColorPalette.Success;

            _btnCaptureSample.Text = "📷 Capture Sample"; _btnCaptureSample.Location = new Point(10, 144); _btnCaptureSample.Size = new Size(185, 36); Theme.StyleSuccessButton(_btnCaptureSample); _btnCaptureSample.Enabled = false; _btnCaptureSample.Click += BtnCapture_Click;
            _btnClearFaceData.Text = "🗑 Clear Face Data"; _btnClearFaceData.Location = new Point(205, 144); _btnClearFaceData.Size = new Size(185, 36); Theme.StyleDangerButton(_btnClearFaceData); _btnClearFaceData.Click += BtnClearFace_Click;

            _regPanel.Controls.AddRange(new Control[] { lblRegTitle, lblStuLabel, _cboStudents, _lblSampleCount, _pbSamples, _btnCaptureSample, _btnClearFaceData });

            // ── RESULT PANEL ──────────────────────────────────────────────────
            _resultPanel.Location  = new Point(10, 205);
            _resultPanel.Size      = new Size(420, 220);
            _resultPanel.BackColor = Color.FromArgb(10, 40, 20);
            _resultPanel.Visible   = false;

            Label lblResTitle = new() { Text = "Recognition Result", Font = new Font("Segoe UI", 11, FontStyle.Bold), ForeColor = Color.White, AutoSize = true, Location = new Point(10, 10) };

            _lblStudentName.Location  = new Point(10, 40); _lblStudentName.Size = new Size(390, 40); _lblStudentName.Font = new Font("Segoe UI", 16, FontStyle.Bold); _lblStudentName.ForeColor = Color.LimeGreen; _lblStudentName.Text = "Waiting for face…";
            _lblConfidence.Location   = new Point(10, 90); _lblConfidence.AutoSize = true; _lblConfidence.Font = new Font("Segoe UI", 13); _lblConfidence.ForeColor = Color.White;
            _lblAttendanceStatus.Location = new Point(10, 125); _lblAttendanceStatus.Size = new Size(390, 40); _lblAttendanceStatus.Font = new Font("Segoe UI", 10, FontStyle.Bold); _lblAttendanceStatus.ForeColor = ColorPalette.Warning; _lblAttendanceStatus.Text = "";

            _btnManualFallback.Text = "✏ Manual Attendance Fallback"; _btnManualFallback.Location = new Point(10, 172); _btnManualFallback.Size = new Size(390, 36); Theme.StyleWarningButton(_btnManualFallback); _btnManualFallback.Click += BtnManualFallback_Click;

            _resultPanel.Controls.AddRange(new Control[] { lblResTitle, _lblStudentName, _lblConfidence, _lblAttendanceStatus, _btnManualFallback });

            rightPanel.Controls.AddRange(new Control[] { lblMode, _btnRegisterMode, _btnRecognizeMode, _btnTrainModel, _regPanel, _resultPanel });

            this.Controls.Add(rightPanel);
            this.Controls.Add(leftPanel);
            this.Controls.Add(topBar);
        }

        // ── CAMERA ────────────────────────────────────────────────────────────

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            try
            {
                int idx = (int)_nudCameraIdx.Value;
                _camera.FrameReady += Camera_FrameReady;
                _camera.Start(idx);
                _btnStartCamera.Enabled = false;
                _btnStopCamera.Enabled  = true;
                _btnCaptureSample.Enabled = true;
                UpdateSystemStatus($"📷 Camera {idx} started.", ColorPalette.Success);
            }
            catch (Exception ex)
            {
                UpdateSystemStatus($"❌ {ex.Message}", ColorPalette.Danger);
            }
        }

        private void BtnStop_Click(object? sender, EventArgs e)
        {
            _camera.FrameReady -= Camera_FrameReady;
            _camera.Stop();
            _pbCamera.Image = null;
            _btnStartCamera.Enabled = true;
            _btnStopCamera.Enabled  = false;
            _btnCaptureSample.Enabled = false;
            UpdateSystemStatus("⬛ Camera stopped.", ColorPalette.Warning);
        }

        private void Camera_FrameReady(object? sender, Bitmap frame)
        {
            // Detect faces
            List<System.Drawing.Rectangle> faces = _faceAI.DetectFaces(frame);

            if (_mode == AppMode.Recognizing && _recognizerTrained && faces.Count > 0)
            {
                // Recognition: run on first detected face
                System.Drawing.Rectangle faceRect = faces[0];
                using Mat? roi = _faceAI.ExtractFaceROI(frame, faceRect);
                if (roi != null)
                {
                    var (studentId, confidence) = _faceAI.Recognize(roi);
                    string label;

                    if (studentId > 0)
                    {
                        Student? stu = _studentRepo.GetById(studentId);
                        label = stu?.FullName ?? $"ID:{studentId}";
                        SafeUI(() =>
                        {
                            _lblStudentName.Text = label;
                            _lblStudentName.ForeColor = ColorPalette.Success;
                            _lblConfidence.Text = $"Confidence: {confidence:F1}%";
                            TryAutoMarkAttendance(studentId, confidence);
                        });
                    }
                    else
                    {
                        label = "Unknown";
                        SafeUI(() =>
                        {
                            _lblStudentName.Text  = "⚠ Unknown Person";
                            _lblStudentName.ForeColor = ColorPalette.Warning;
                            _lblConfidence.Text   = confidence > 0 ? $"Similarity: {confidence:F1}% (too low)" : "No match";
                            _lblAttendanceStatus.Text = "";
                        });
                    }

                    Bitmap overlay = _faceAI.DrawDetection(frame, faces, label, confidence);
                    SafeUI(() => UpdatePreview(overlay));
                    return;
                }
            }

            // Just detection overlay (no recognition)
            Bitmap overlayOnly = _faceAI.DrawDetection(frame, faces);
            SafeUI(() => UpdatePreview(overlayOnly));
        }

        private void UpdatePreview(Bitmap bmp)
        {
            var old = _pbCamera.Image;
            _pbCamera.Image = bmp;
            old?.Dispose();
        }

        // ── MODES ────────────────────────────────────────────────────────────

        private void SetMode(AppMode mode)
        {
            _mode = mode;
            _regPanel.Visible    = (mode == AppMode.Registering);
            _resultPanel.Visible = (mode == AppMode.Recognizing);

            if (mode == AppMode.Registering)
                UpdateSystemStatus("📸 Registration mode: select student and capture 20 samples.", ColorPalette.Info);
            else if (mode == AppMode.Recognizing)
            {
                if (!_recognizerTrained)
                    UpdateSystemStatus("⚠ Model not trained. Click 'Train AI Model' first.", ColorPalette.Warning);
                else
                    UpdateSystemStatus("🔍 Recognition mode: face the camera.", ColorPalette.Success);
            }
        }

        // ── TRAINING ─────────────────────────────────────────────────────────

        private async void BtnTrain_Click(object? sender, EventArgs e)
        {
            _btnTrainModel.Enabled = false;
            UpdateSystemStatus("🧠 Training AI model…", ColorPalette.Info);

            var (success, message) = await Task.Run(() => _faceAI.TrainFromDatabase());

            _recognizerTrained = success;
            _btnTrainModel.Enabled = true;
            UpdateSystemStatus(success ? $"✅ {message}" : $"❌ {message}",
                               success ? ColorPalette.Success : ColorPalette.Danger);
        }

        // ── FACE REGISTRATION ─────────────────────────────────────────────────

        private void LoadStudentsCombo()
        {
            try
            {
                _cboStudents.Items.Clear();
                var students = _studentRepo.GetAll();
                foreach (var s in students)
                    _cboStudents.Items.Add(new StudentItem(s));
                if (_cboStudents.Items.Count > 0) _cboStudents.SelectedIndex = 0;
            }
            catch { }
        }

        private void CboStudents_Changed(object? sender, EventArgs e)
        {
            if (_cboStudents.SelectedItem is StudentItem item)
            {
                _selectedStudentId = item.Student.StudentID;
                int count = _faceRepo.CountSamples(_selectedStudentId);
                UpdateSampleProgress(count);
            }
        }

        private void UpdateSampleProgress(int count)
        {
            _capturedSamples         = count;
            _pbSamples.Value         = Math.Min(count, RequiredSamples);
            _lblSampleCount.Text     = $"Samples: {count} / {RequiredSamples}  {(count >= RequiredSamples ? "✅ Ready to train" : "Keep capturing")}";
        }

        private async void BtnCapture_Click(object? sender, EventArgs e)
        {
            if (_selectedStudentId == 0) { Helpers.ShowError("Select a student first."); return; }
            if (!_camera.IsRunning)      { Helpers.ShowError("Start camera first."); return; }

            _btnCaptureSample.Enabled = false;

            Bitmap? frame = _camera.GrabFrame();
            if (frame == null) { _btnCaptureSample.Enabled = true; return; }

            var faces = _faceAI.DetectFaces(frame);
            if (faces.Count == 0)
            {
                UpdateSystemStatus("⚠ No face detected. Ensure face is clearly visible.", ColorPalette.Warning);
                _btnCaptureSample.Enabled = true;
                frame.Dispose();
                return;
            }

            // Extract and save
            Mat? roi = _faceAI.ExtractFaceROI(frame, faces[0]);
            if (roi == null) { _btnCaptureSample.Enabled = true; frame.Dispose(); return; }

            float[] encoding = _faceAI.ComputeEncoding(roi);

            // Save image to disk
            string folder    = Helpers.StudentFaceFolder(_selectedStudentId);
            string imagePath = Path.Combine(folder, $"sample_{DateTime.Now:yyyyMMdd_HHmmss_fff}.jpg");

            await Task.Run(() =>
            {
                Cv2.ImWrite(imagePath, roi);
                _faceRepo.InsertFaceEncoding(_selectedStudentId, encoding, imagePath);
            });

            roi.Dispose();
            frame.Dispose();

            int newCount = _faceRepo.CountSamples(_selectedStudentId);
            SafeUI(() =>
            {
                UpdateSampleProgress(newCount);
                UpdateSystemStatus($"📷 Sample {newCount}/{RequiredSamples} captured.", ColorPalette.Success);
                _btnCaptureSample.Enabled = true;
            });
        }

        private void BtnClearFace_Click(object? sender, EventArgs e)
        {
            if (_selectedStudentId == 0) return;
            if (!Helpers.Confirm("Delete all face data for this student?")) return;
            _faceRepo.DeleteByStudent(_selectedStudentId);
            UpdateSampleProgress(0);
            UpdateSystemStatus("🗑 Face data cleared.", ColorPalette.Warning);
        }

        // ── ATTENDANCE ────────────────────────────────────────────────────────

        private void TryAutoMarkAttendance(int studentId, double confidence)
        {
            if (_markedThisSession.Contains(studentId))
            {
                _lblAttendanceStatus.Text      = "✅ Already marked today";
                _lblAttendanceStatus.ForeColor = ColorPalette.Info;
                return;
            }

            var (ok, msg) = _attendanceSvc.MarkByFace(studentId, confidence);
            _lblAttendanceStatus.Text      = msg;
            _lblAttendanceStatus.ForeColor = ok ? ColorPalette.Success : ColorPalette.Warning;
            if (ok) _markedThisSession.Add(studentId);
        }

        private void BtnManualFallback_Click(object? sender, EventArgs e)
        {
            using FallbackAttendanceDialog dlg = new(_studentRepo, _attendanceSvc);
            dlg.ShowDialog(this);
        }

        // ── HELPERS ───────────────────────────────────────────────────────────

        private void UpdateSystemStatus(string msg, Color color)
        {
            SafeUI(() =>
            {
                _lblSystemStatus.Text      = msg;
                _lblSystemStatus.ForeColor = color;
            });
        }

        private void SafeUI(Action action)
        {
            if (this.IsDisposed) return;
            if (this.InvokeRequired)
                this.BeginInvoke(action);
            else
                action();
        }

        private void FaceForm_Closing(object? sender, FormClosingEventArgs e)
        {
            _camera.FrameReady -= Camera_FrameReady;
            _camera.Stop();
            _camera.Dispose();
            _faceAI.Dispose();
        }

        // ── INNER TYPES ───────────────────────────────────────────────────────

        private class StudentItem
        {
            public Student Student { get; }
            public StudentItem(Student s) { Student = s; }
            public override string ToString() => $"[{Student.StudentID}] {Student.FullName}";
        }
    }

    // ── MANUAL FALLBACK DIALOG ────────────────────────────────────────────────

    internal class FallbackAttendanceDialog : Form
    {
        private ComboBox _cboStudent        = new();
        private ComboBox _cboStatus         = new();
        private Button _btnMark             = new();
        private Label _lblResult            = new();
        private readonly StudentRepository _studentRepo;
        private readonly AttendanceService _service;

        public FallbackAttendanceDialog(StudentRepository studentRepo, AttendanceService service)
        {
            _studentRepo = studentRepo;
            _service     = service;
            Build();
            LoadStudents();
        }

        private void Build()
        {
            this.Text          = "Manual Attendance Fallback";
            this.Size          = new Size(420, 230);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox   = false;
            this.BackColor     = Color.White;

            Label l1 = new() { Text = "Student:", AutoSize = true, Location = new Point(20, 20), Font = Theme.LabelFont };
            _cboStudent.Location = new Point(20, 42); _cboStudent.Size = new Size(360, 28); _cboStudent.DropDownStyle = ComboBoxStyle.DropDownList; _cboStudent.Font = Theme.InputFont;

            Label l2 = new() { Text = "Status:", AutoSize = true, Location = new Point(20, 80), Font = Theme.LabelFont };
            _cboStatus.Location = new Point(20, 102); _cboStatus.Size = new Size(200, 28); _cboStatus.DropDownStyle = ComboBoxStyle.DropDownList; _cboStatus.Font = Theme.InputFont;
            _cboStatus.Items.AddRange(new object[] { "Present", "Absent", "Late" }); _cboStatus.SelectedIndex = 0;

            _btnMark.Text = "✅ Mark Attendance"; _btnMark.Location = new Point(20, 145); _btnMark.Size = new Size(360, 36); Theme.StyleSuccessButton(_btnMark); _btnMark.Click += BtnMark_Click;

            _lblResult.Location = new Point(20, 188); _lblResult.AutoSize = true; _lblResult.Font = new Font("Segoe UI", 9);

            this.Controls.AddRange(new Control[] { l1, _cboStudent, l2, _cboStatus, _btnMark, _lblResult });
        }

        private void LoadStudents()
        {
            var students = _studentRepo.GetAll();
            foreach (var s in students)
                _cboStudent.Items.Add(new StuItem(s));
            if (_cboStudent.Items.Count > 0) _cboStudent.SelectedIndex = 0;
        }

        private void BtnMark_Click(object? sender, EventArgs e)
        {
            if (_cboStudent.SelectedItem is not StuItem item) return;
            var (ok, msg) = _service.ManualMark(item.Id, DateTime.Today, _cboStatus.Text);
            _lblResult.Text      = msg;
            _lblResult.ForeColor = ok ? ColorPalette.Success : ColorPalette.Danger;
        }

        private class StuItem
        {
            public int Id { get; }
            private string Name { get; }
            public StuItem(Student s) { Id = s.StudentID; Name = s.FullName; }
            public override string ToString() => $"[{Id}] {Name}";
        }
    }
}
