using System.Drawing.Imaging;
using DlibDotNet;
using DlibDotNet.Dnn;
using DlibDotNet.Extensions;
using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Services
{
    // A face is considered a "match" when the Euclidean distance between two
    // 128-value Dlib face descriptors is below this threshold. Dlib's own docs
    // suggest 0.6 as a general default; this app uses a stricter 0.50 for
    // higher-confidence ("ultra accurate") attendance matching, at the cost of
    // occasionally asking a legitimate student to try again if lighting is poor.
    public class FaceRecognitionService : IDisposable
    {
        private const double MatchThreshold = 0.50;

        // The best match must also be at least this much closer than the second-best
        // candidate - otherwise two students look too similar to be confident, and
        // we'd rather ask for a retry than risk marking the wrong person present.
        private const double MinimumConfidenceMargin = 0.07;

        // A detected face smaller than this (in pixels, on either side) is treated
        // as "too far from the camera" and rejected, since a small, low-detail
        // face produces a much less reliable descriptor.
        private const int MinimumFaceSizePixels = 90;

        private readonly FrontalFaceDetector _detector;
        private readonly ShapePredictor _shapePredictor;
        private readonly LossMetric _recognitionNet;

        public FaceRecognitionService()
        {
            if (!File.Exists(FilePaths.ShapePredictorModelPath))
            {
                throw new FileNotFoundException(
                    "Model file not found. Please download shape_predictor_68_face_landmarks.dat and place it in the Models folder.",
                    FilePaths.ShapePredictorModelPath);
            }

            if (!File.Exists(FilePaths.FaceRecognitionModelPath))
            {
                throw new FileNotFoundException(
                    "Model file not found. Please download dlib_face_recognition_resnet_model_v1.dat and place it in the Models folder.",
                    FilePaths.FaceRecognitionModelPath);
            }

            _detector = Dlib.GetFrontalFaceDetector();
            _shapePredictor = ShapePredictor.Deserialize(FilePaths.ShapePredictorModelPath);
            _recognitionNet = LossMetric.Deserialize(FilePaths.FaceRecognitionModelPath);
        }

        // Returns the bounding rectangles of every face found in the frame.
        // NOTE: explicitly System.Drawing.Rectangle here because DlibDotNet also
        // defines its own "Rectangle" type and the two would otherwise clash.
        public System.Drawing.Rectangle[] DetectFaces(System.Drawing.Bitmap frame)
        {
            using Matrix<RgbPixel> image = BitmapToMatrix(frame);
            DlibDotNet.Rectangle[] faces = _detector.Operator(image);
            return faces
                .Select(f => new System.Drawing.Rectangle(f.Left, f.Top, (int)f.Width, (int)f.Height))
                .ToArray();
        }

        // Computes the 128-value face descriptor for the largest face found in the frame.
        // Returns null if no face was found, or if the face is too small/low-detail
        // to produce a reliable descriptor.
        public double[]? GetFaceEncoding(System.Drawing.Bitmap frame)
        {
            using Matrix<RgbPixel> image = BitmapToMatrix(frame);
            DlibDotNet.Rectangle[] faces = _detector.Operator(image);
            if (faces.Length == 0)
            {
                return null;
            }

            DlibDotNet.Rectangle largest = faces.OrderByDescending(f => f.Width * f.Height).First();

            if (largest.Width < MinimumFaceSizePixels || largest.Height < MinimumFaceSizePixels)
            {
                // Face is too small/far away to trust - ask the student to move closer instead.
                return null;
            }

            FullObjectDetection shape = _shapePredictor.Detect(image, largest);

            ChipDetails chipDetails = Dlib.GetFaceChipDetails(shape, 150, 0.25);

            // Because "image" here is a Matrix<RgbPixel> (not an Array2D<RgbPixel>),
            // Dlib.ExtractImageChip resolves to the overload that also returns a
            // Matrix<RgbPixel> chip - which is exactly what the recognition network needs.
            using Matrix<RgbPixel> faceChip = Dlib.ExtractImageChip<RgbPixel>(image, chipDetails);

            using OutputLabels<Matrix<float>> result = _recognitionNet.Operator(faceChip);
            Matrix<float> descriptorMatrix = result.First();

            float[] values = descriptorMatrix.ToArray();
            var descriptor = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                descriptor[i] = values[i];
            }

            return descriptor;
        }

        // Compares an encoding against every enrolled student and returns the
        // closest match, if any is within the recognition threshold AND clearly
        // more confident than the next-closest student (avoids mixing up two
        // students who happen to look similar).
        public (Student? student, double distance) FindBestMatch(double[] encoding, IEnumerable<Student> students)
        {
            Student? bestStudent = null;
            double bestDistance = double.MaxValue;
            double secondBestDistance = double.MaxValue;

            foreach (Student student in students)
            {
                if (student.FaceEncoding.Length != encoding.Length || encoding.Length == 0)
                {
                    continue;
                }

                double distance = EuclideanDistance(encoding, student.FaceEncoding);
                if (distance < bestDistance)
                {
                    secondBestDistance = bestDistance;
                    bestDistance = distance;
                    bestStudent = student;
                }
                else if (distance < secondBestDistance)
                {
                    secondBestDistance = distance;
                }
            }

            bool withinThreshold = bestStudent != null && bestDistance <= MatchThreshold;
            bool clearlyBetterThanRunnerUp = secondBestDistance - bestDistance >= MinimumConfidenceMargin;

            if (withinThreshold && clearlyBetterThanRunnerUp)
            {
                return (bestStudent, bestDistance);
            }

            return (null, bestDistance);
        }

        private static double EuclideanDistance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }

        // Converts a System.Drawing.Bitmap into a Matrix<RgbPixel> using DlibDotNet's
        // own official Bitmap.ToMatrix<T>() extension (DlibDotNet.Extensions package),
        // instead of hand-rolled unsafe pixel copying.
        private static Matrix<RgbPixel> BitmapToMatrix(System.Drawing.Bitmap source)
        {
            if (source.PixelFormat == PixelFormat.Format24bppRgb)
            {
                return source.ToMatrix<RgbPixel>();
            }

            using System.Drawing.Bitmap converted = source.Clone(
                new System.Drawing.Rectangle(0, 0, source.Width, source.Height),
                PixelFormat.Format24bppRgb);
            return converted.ToMatrix<RgbPixel>();
        }

        public void Dispose()
        {
            _detector.Dispose();
            _shapePredictor.Dispose();
            _recognitionNet.Dispose();
        }
    }
}
