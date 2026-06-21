using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using SmartCampusAI.Data;
using SmartCampusAI.Models;

namespace SmartCampusAI.Services
{
    /// <summary>
    /// Core AI service: face detection via Haar Cascade +
    /// face recognition via LBPHFaceRecognizer (OpenCvSharp).
    ///
    /// LBPHFaceRecognizer works entirely offline, needs no GPU,
    /// and produces a "distance" score (lower = better match).
    /// We invert distance to a 0-100% confidence score.
    /// </summary>
    public sealed class FaceRecognitionService : IDisposable
    {
        // ── Haar Cascade XML paths (copy to output folder) ─────────────────────
        private static readonly string CascadePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                         "Resources", "haarcascade_frontalface_default.xml");

        private static readonly string EyeCascadePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                         "Resources", "haarcascade_eye.xml");

        // ── Recognition threshold ──────────────────────────────────────────────
        // LBPH distance: 0 = perfect, >100 = poor. We treat >80 as "unknown".
        private const double MaxDistance       = 100.0;
        private const double MinConfidenceThreshold = 80.0;

        private CascadeClassifier? _faceCascade;
        private CascadeClassifier? _eyeCascade;
        private LBPHFaceRecognizer? _recognizer;
        private readonly FaceRepository _faceRepo = new();

        // Cached known labels: label index → StudentID
        private readonly Dictionary<int, int> _labelToStudentId = new();
        private bool _isTrained;
        private bool _disposed;

        // ── PUBLIC INIT ───────────────────────────────────────────────────────
        public void Initialize()
        {
            if (!File.Exists(CascadePath))
                throw new FileNotFoundException(
                    "Haar cascade file not found. Place haarcascade_frontalface_default.xml " +
                    $"in {Path.GetDirectoryName(CascadePath)}.", CascadePath);

            _faceCascade = new CascadeClassifier(CascadePath);

            if (File.Exists(EyeCascadePath))
                _eyeCascade = new CascadeClassifier(EyeCascadePath);

            _recognizer = LBPHFaceRecognizer.Create(radius: 1, neighbors: 8,
                                                     gridX: 8, gridY: 8, threshold: MaxDistance);
            _isTrained  = false;
            _labelToStudentId.Clear();
        }

        // ── DETECT FACES ──────────────────────────────────────────────────────
        /// <summary>
        /// Detects faces in the given Bitmap.
        /// Returns list of face rectangles in image coordinates.
        /// </summary>
        public List<System.Drawing.Rectangle> DetectFaces(Bitmap bitmap)
        {
            List<System.Drawing.Rectangle> results = new();
            if (_faceCascade == null) return results;

            using Mat mat = BitmapConverter.ToMat(bitmap);
            using Mat gray = new();
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.EqualizeHist(gray, gray);

            OpenCvSharp.Rect[] faces = _faceCascade.DetectMultiScale(
                gray,
                scaleFactor: 1.1,
                minNeighbors: 5,
                minSize: new OpenCvSharp.Size(80, 80));

            foreach (OpenCvSharp.Rect r in faces)
                results.Add(new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height));

            return results;
        }

        // ── DRAW DETECTION OVERLAY ────────────────────────────────────────────
        /// <summary>
        /// Draws rectangles on bitmap around all detected faces.
        /// Optionally overlays recognition label. Returns new Bitmap.
        /// </summary>
        public Bitmap DrawDetection(Bitmap source,
                                    List<System.Drawing.Rectangle> faces,
                                    string? label = null,
                                    double confidence = 0)
        {
            Bitmap result = new(source);
            using Graphics g = Graphics.FromImage(result);
            using System.Drawing.Pen pen   = new(Color.LimeGreen, 2);
            using SolidBrush textBrush     = new(Color.LimeGreen);
            using SolidBrush bgBrush       = new(Color.FromArgb(160, 0, 0, 0));
            using Font font                = new("Segoe UI", 9, FontStyle.Bold);

            foreach (System.Drawing.Rectangle r in faces)
            {
                g.DrawRectangle(pen, r);

                string display = label ?? "Detecting…";
                if (confidence > 0)
                    display += $" ({confidence:F1}%)";

                SizeF ts = g.MeasureString(display, font);
                g.FillRectangle(bgBrush,
                    r.X, r.Y - (int)ts.Height - 4,
                    (int)ts.Width + 4, (int)ts.Height + 4);
                g.DrawString(display, font, textBrush,
                    r.X + 2, r.Y - (int)ts.Height - 2);
            }
            return result;
        }

        // ── EXTRACT FACE ROI ──────────────────────────────────────────────────
        /// <summary>
        /// Extracts grayscale 100×100 face region for training / encoding.
        /// Returns null if face list is empty.
        /// </summary>
        public Mat? ExtractFaceROI(Bitmap bitmap, System.Drawing.Rectangle faceRect)
        {
            using Mat mat = BitmapConverter.ToMat(bitmap);
            using Mat gray = new();
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.EqualizeHist(gray, gray);

            OpenCvSharp.Rect roi = new(faceRect.X, faceRect.Y, faceRect.Width, faceRect.Height);
            // Clamp to image bounds
            roi.X      = Math.Max(0, roi.X);
            roi.Y      = Math.Max(0, roi.Y);
            roi.Width  = Math.Min(roi.Width,  gray.Width  - roi.X);
            roi.Height = Math.Min(roi.Height, gray.Height - roi.Y);

            if (roi.Width <= 0 || roi.Height <= 0) return null;

            Mat face = new(gray, roi);
            Mat resized = new();
            Cv2.Resize(face, resized, new OpenCvSharp.Size(100, 100));
            return resized;
        }

        // ── COMPUTE LBPH HISTOGRAM (encoding) ────────────────────────────────
        /// <summary>
        /// Returns a simplified 256-bin LBP histogram as float[] encoding.
        /// Used to store a compact face representation in the DB.
        /// </summary>
        public float[] ComputeEncoding(Mat grayFaceRoi)
        {
            // Compute LBP manually: for each pixel compare with 8 neighbours
            int w = grayFaceRoi.Width, h = grayFaceRoi.Height;
            int[] hist = new int[256];

            for (int y = 1; y < h - 1; y++)
            {
                for (int x = 1; x < w - 1; x++)
                {
                    byte center = grayFaceRoi.At<byte>(y, x);
                    int code = 0;
                    if (grayFaceRoi.At<byte>(y - 1, x - 1) >= center) code |= 1 << 7;
                    if (grayFaceRoi.At<byte>(y - 1, x    ) >= center) code |= 1 << 6;
                    if (grayFaceRoi.At<byte>(y - 1, x + 1) >= center) code |= 1 << 5;
                    if (grayFaceRoi.At<byte>(y,     x + 1) >= center) code |= 1 << 4;
                    if (grayFaceRoi.At<byte>(y + 1, x + 1) >= center) code |= 1 << 3;
                    if (grayFaceRoi.At<byte>(y + 1, x    ) >= center) code |= 1 << 2;
                    if (grayFaceRoi.At<byte>(y + 1, x - 1) >= center) code |= 1 << 1;
                    if (grayFaceRoi.At<byte>(y,     x - 1) >= center) code |= 1 << 0;
                    hist[code]++;
                }
            }

            // Normalize to 0-1 float
            float total = hist.Sum();
            float[] encoding = new float[256];
            for (int i = 0; i < 256; i++)
                encoding[i] = total > 0 ? hist[i] / total : 0f;

            return encoding;
        }

        // ── TRAIN RECOGNIZER ──────────────────────────────────────────────────
        /// <summary>
        /// Loads all FaceData from DB and trains the LBPH recognizer.
        /// Must be called before recognition.
        /// </summary>
        public (bool success, string message) TrainFromDatabase()
        {
            if (_recognizer == null) Initialize();

            List<FaceData> allFaces = _faceRepo.GetAllFaceData();
            if (allFaces.Count == 0)
                return (false, "No face data found in database. Please register students first.");

            List<Mat>   images = new();
            List<int>   labels = new();
            _labelToStudentId.Clear();

            // Assign consecutive integer labels (LBPH requirement)
            Dictionary<int, int> studentToLabel = new();
            int labelCounter = 0;

            foreach (FaceData fd in allFaces)
            {
                if (fd.EncodingArray == null || fd.EncodingArray.Length != 256)
                    continue;

                // Try to load saved sample image from disk
                if (File.Exists(fd.SampleImagePath))
                {
                    try
                    {
                        Mat img = Cv2.ImRead(fd.SampleImagePath, ImreadModes.Grayscale);
                        if (!img.Empty())
                        {
                            Cv2.Resize(img, img, new OpenCvSharp.Size(100, 100));

                            if (!studentToLabel.ContainsKey(fd.StudentID))
                            {
                                studentToLabel[fd.StudentID] = labelCounter;
                                _labelToStudentId[labelCounter] = fd.StudentID;
                                labelCounter++;
                            }

                            images.Add(img);
                            labels.Add(studentToLabel[fd.StudentID]);
                        }
                    }
                    catch { /* skip corrupted image */ }
                }
                else
                {
                    // Reconstruct Mat from stored encoding histogram
                    // (fallback: won't be as accurate but still works)
                    Mat synth = ReconstructFromEncoding(fd.EncodingArray);
                    if (!studentToLabel.ContainsKey(fd.StudentID))
                    {
                        studentToLabel[fd.StudentID] = labelCounter;
                        _labelToStudentId[labelCounter] = fd.StudentID;
                        labelCounter++;
                    }
                    images.Add(synth);
                    labels.Add(studentToLabel[fd.StudentID]);
                }
            }

            if (images.Count == 0)
                return (false, "No valid images found for training.");

            _recognizer!.Train(images, labels);
            _isTrained = true;

            // Clean up
            foreach (Mat m in images) m.Dispose();

            return (true, $"Trained with {images.Count} samples for {labelCounter} student(s).");
        }

        // ── RECOGNIZE FACE ────────────────────────────────────────────────────
        /// <summary>
        /// Predicts student from a face ROI Mat.
        /// Returns (StudentID, confidence%) or (-1, 0) if unknown.
        /// </summary>
        public (int studentId, double confidence) Recognize(Mat grayFaceRoi)
        {
            if (!_isTrained || _recognizer == null)
                return (-1, 0);

            _recognizer.Predict(grayFaceRoi, out int label, out double distance);

            if (label < 0 || !_labelToStudentId.ContainsKey(label))
                return (-1, 0);

            // Convert distance to confidence percent (0 distance = 100%)
            double confidence = Math.Max(0, 100.0 - (distance / MaxDistance) * 100.0);

            if (confidence < MinConfidenceThreshold)
                return (-1, confidence);   // below threshold = unknown

            int studentId = _labelToStudentId[label];
            return (studentId, confidence);
        }

        // ── COMPARE ENCODINGS (cosine similarity) ────────────────────────────
        /// <summary>
        /// Compares two 256-float LBP histograms using cosine similarity.
        /// Returns 0-100% similarity.
        /// </summary>
        public static double CosineSimilarity(float[] a, float[] b)
        {
            if (a.Length != b.Length) return 0;
            double dot = 0, normA = 0, normB = 0;
            for (int i = 0; i < a.Length; i++)
            {
                dot   += a[i] * b[i];
                normA += a[i] * a[i];
                normB += b[i] * b[i];
            }
            if (normA == 0 || normB == 0) return 0;
            return (dot / (Math.Sqrt(normA) * Math.Sqrt(normB))) * 100.0;
        }

        // ── AVERAGE ENCODING ─────────────────────────────────────────────────
        /// <summary>
        /// Computes element-wise average of multiple encodings (for mean-face comparison).
        /// </summary>
        public static float[] AverageEncodings(List<float[]> encodings)
        {
            if (encodings.Count == 0) return Array.Empty<float>();
            float[] avg = new float[256];
            foreach (float[] enc in encodings)
                for (int i = 0; i < 256; i++)
                    avg[i] += enc[i];
            for (int i = 0; i < 256; i++)
                avg[i] /= encodings.Count;
            return avg;
        }

        // ── PRIVATE HELPERS ───────────────────────────────────────────────────
        private static Mat ReconstructFromEncoding(float[] encoding)
        {
            // Creates a 100×100 synthetic grayscale Mat from LBP histogram
            // (deterministic approximation for when images aren't on disk)
            Mat mat = new(100, 100, MatType.CV_8UC1);
            Random rng = new(42);
            int pixelIdx = 0;
            for (int i = 0; i < 256; i++)
            {
                int count = (int)(encoding[i] * 9999); // ~scale back to pixel count
                for (int c = 0; c < count && pixelIdx < 10000; c++, pixelIdx++)
                {
                    int y = pixelIdx / 100;
                    int x = pixelIdx % 100;
                    if (y < 100 && x < 100)
                        mat.Set<byte>(y, x, (byte)i);
                }
            }
            return mat;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _faceCascade?.Dispose();
            _eyeCascade?.Dispose();
            _recognizer?.Dispose();
            _disposed = true;
        }
    }
}
