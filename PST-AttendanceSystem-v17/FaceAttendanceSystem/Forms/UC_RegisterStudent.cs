using System.IO.Compression;
using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;
using FaceAttendanceSystem.Services;

namespace FaceAttendanceSystem.Forms
{
    public partial class UC_RegisterStudent : UserControl
    {
        private const int RequiredPhotoCount = 5;

        private readonly CameraService _camera = new();
        private readonly System.Windows.Forms.Timer _previewTimer = new() { Interval = 40 };
        private readonly StudentRepository _studentRepository = new();

        private FaceRecognitionService? _faceService;
        private readonly List<double[]> _capturedEncodings = new();
        private readonly List<Bitmap> _capturedPhotos = new();

        public UC_RegisterStudent()
        {
            InitializeComponent();
            courseComboBox.SelectedIndex = 0;
            UpdateProgressLabel();
            _previewTimer.Tick += PreviewTimer_Tick;
            HandleCreated += (s, e) => StartCamera();
            HandleDestroyed += (s, e) => StopCamera();
        }

        private void StartCamera()
        {
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
        }

        private void UpdateProgressLabel()
        {
            progressLabel.Text = $"Photos captured: {_capturedPhotos.Count} / {RequiredPhotoCount}";
            captureButton.Enabled = _capturedPhotos.Count < RequiredPhotoCount;
            captureButton.Text = _capturedPhotos.Count == 0
                ? "📸 Capture Photo 1"
                : $"📸 Capture Photo {_capturedPhotos.Count + 1}";
        }

        private void captureButton_Click(object sender, EventArgs e)
        {
            if (_faceService == null || cameraPictureBox.Image == null)
            {
                return;
            }

            using var snapshot = new Bitmap(cameraPictureBox.Image);
            double[]? encoding = _faceService.GetFaceEncoding(snapshot);

            if (encoding == null)
            {
                statusLabel.Text = "No clear face detected. Please face the camera directly and move a bit closer.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            _capturedEncodings.Add(encoding);
            _capturedPhotos.Add(new Bitmap(snapshot));

            capturedPreviewBox.Image?.Dispose();
            capturedPreviewBox.Image = new Bitmap(snapshot);

            UpdateProgressLabel();

            statusLabel.Text = _capturedPhotos.Count < RequiredPhotoCount
                ? "Great! Move your head slightly and capture the next photo."
                : "All 5 photos captured. Fill in the details and click Save.";
            statusLabel.ForeColor = Color.FromArgb(30, 130, 30);
        }

        private void resetPhotosButton_Click(object sender, EventArgs e)
        {
            ClearCapturedPhotos();
            statusLabel.Text = "Photos cleared. Start capturing again.";
            statusLabel.ForeColor = Color.DimGray;
        }

        private void ClearCapturedPhotos()
        {
            foreach (Bitmap photo in _capturedPhotos)
            {
                photo.Dispose();
            }
            _capturedPhotos.Clear();
            _capturedEncodings.Clear();
            capturedPreviewBox.Image?.Dispose();
            capturedPreviewBox.Image = null;
            UpdateProgressLabel();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string regNumber = regNumberTextBox.Text.Trim();
            string fullName = fullNameTextBox.Text.Trim();
            string course = courseComboBox.Text;
            string email = studentEmailTextBox.Text.Trim();

            // Nothing is optional - every field must be filled in before registration is allowed.
            if (string.IsNullOrEmpty(regNumber) || string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(course) || string.IsNullOrEmpty(email))
            {
                statusLabel.Text = "Please fill in every field - Student ID, Full Name, Course, and Email are all required.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            if (!ValidationHelper.IsValidStudentId(regNumber))
            {
                statusLabel.Text = $"Student ID must be in the format YYAPP#### ({ValidationHelper.StudentIdExample}).";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            if (!ValidationHelper.IsValidStudentEmail(email))
            {
                statusLabel.Text = $"Please use the student's official university email, not a personal one ({ValidationHelper.StudentEmailExample}).";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            if (_capturedPhotos.Count < RequiredPhotoCount)
            {
                statusLabel.Text = $"Please capture all {RequiredPhotoCount} photos first.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            if (_studentRepository.RegNumberExists(regNumber))
            {
                statusLabel.Text = "A student with this ID already exists.";
                statusLabel.ForeColor = Color.Firebrick;
                return;
            }

            // Averaging the 5 face descriptors gives a more robust "reference face"
            // than relying on a single photo, which improves recognition accuracy.
            double[] averagedEncoding = AverageEncodings(_capturedEncodings);

            // Save a single photo as the student's profile picture (shown in the UI).
            string photoPath = FilePaths.GetPhotoPath(regNumber);
            _capturedPhotos[0].Save(photoPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            // Save all 5 raw training photos, zip them up, then delete the raw
            // folder - this is what "Zip the photos to save storage" means here.
            SaveAndZipTrainingPhotos(regNumber);

            _studentRepository.Add(new Student
            {
                RegNumber = regNumber,
                FullName = fullName,
                Course = course,
                Email = email,
                FaceEncoding = averagedEncoding,
                PhotoPath = photoPath
            });

            statusLabel.Text = $"'{fullName}' registered successfully!";
            statusLabel.ForeColor = Color.FromArgb(30, 130, 30);

            regNumberTextBox.Clear();
            fullNameTextBox.Clear();
            studentEmailTextBox.Clear();
            courseComboBox.SelectedIndex = 0;
            ClearCapturedPhotos();
        }

        private static double[] AverageEncodings(List<double[]> encodings)
        {
            int length = encodings[0].Length;
            var averaged = new double[length];

            foreach (double[] encoding in encodings)
            {
                for (int i = 0; i < length; i++)
                {
                    averaged[i] += encoding[i];
                }
            }

            for (int i = 0; i < length; i++)
            {
                averaged[i] /= encodings.Count;
            }

            return averaged;
        }

        private void SaveAndZipTrainingPhotos(string regNumber)
        {
            string folder = FilePaths.GetTrainingPhotosFolder(regNumber);
            string zipPath = FilePaths.GetTrainingPhotosZipPath(regNumber);

            Directory.CreateDirectory(folder);

            for (int i = 0; i < _capturedPhotos.Count; i++)
            {
                string photoFile = Path.Combine(folder, $"photo{i + 1}.jpg");
                _capturedPhotos[i].Save(photoFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            ZipFile.CreateFromDirectory(folder, zipPath, CompressionLevel.Optimal, includeBaseDirectory: false);

            // The zip is what we keep; the loose photo files are no longer needed.
            Directory.Delete(folder, recursive: true);
        }
    }
}
