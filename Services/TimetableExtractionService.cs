using System.Text.RegularExpressions;
using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;
using OpenCvSharp;
using UglyToad.PdfPig;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using Windows.Storage;
using Windows.Storage.Streams;

namespace FaceAttendanceSystem.Services
{
    // Extracts raw text from an uploaded timetable photo or PDF, then makes a
    // best-effort attempt to turn that text into candidate timetable entries by
    // matching against the subjects already in Manage Subjects. This is
    // intentionally "best effort, then review" - OCR and table-layout parsing
    // are never 100% reliable, so the results are always shown to the lecturer
    // to check/edit/remove before anything is actually saved to the timetable.
    public static class TimetableExtractionService
    {
        private static readonly string[] DayNames =
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
        };

        // Parallel array to DayNames above - avoids any error-prone arithmetic
        // converting an array index into a DayOfWeek enum value.
        private static readonly DayOfWeek[] DayValues =
        {
            DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday,
            DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
        };

        // Matches subject codes like "PST 21202" or "PST21202".
        private static readonly Regex SubjectCodePattern = new(@"PST\s?\d{5}", RegexOptions.IgnoreCase);

        // Matches time ranges like "8.00-10.00", "8:00 - 10.00", "10.00-12.00".
        private static readonly Regex TimeRangePattern = new(@"(\d{1,2})[.:](\d{2})\s*[-–]\s*(\d{1,2})[.:](\d{2})");

        // Matches simple room codes like "LT 202", "COM L", "Ch. L".
        private static readonly Regex RoomPattern = new(@"\b([A-Z]{2,6}\s?\d{2,4}|LT\s?\d{2,4}|COM\s?L|Ch\.?\s?L)\b");

        public static async Task<string> ExtractTextAsync(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            return extension switch
            {
                ".pdf" => ExtractTextFromPdf(filePath),
                _ => await ExtractTextFromImageAsync(filePath)
            };
        }

        // Uses Windows' own built-in OCR engine (Windows.Media.Ocr) - this ships
        // with Windows itself, so unlike third-party OCR wrappers there is no
        // native DLL that can fail to load. The image is pre-processed first
        // (upscaled, contrast-enhanced, sharpened) so blurry or low-quality
        // photos are recognized much more reliably.
        private static async Task<string> ExtractTextFromImageAsync(string imagePath)
        {
            Windows.Globalization.Language language = new("en");
            if (!OcrEngine.IsLanguageSupported(language))
            {
                throw new InvalidOperationException(
                    "The English OCR language pack isn't installed on this Windows PC. " +
                    "Go to Settings > Time & Language > Language & region > add 'English' " +
                    "and make sure 'Optical character recognition' is included, then try again.");
            }

            OcrEngine? ocrEngine = OcrEngine.TryCreateFromLanguage(language);
            if (ocrEngine == null)
            {
                throw new InvalidOperationException("Could not start the Windows OCR engine for English.");
            }

            string preprocessedPath = PreprocessImageForOcr(imagePath);

            try
            {
                StorageFile file = await StorageFile.GetFileFromPathAsync(preprocessedPath);
                using IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                SoftwareBitmap rawBitmap = await decoder.GetSoftwareBitmapAsync();

                // Windows OCR specifically requires Bgra8/Premultiplied input.
                using SoftwareBitmap ocrReadyBitmap = SoftwareBitmap.Convert(
                    rawBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                rawBitmap.Dispose();

                OcrResult result = await ocrEngine.RecognizeAsync(ocrReadyBitmap);
                return result.Text;
            }
            finally
            {
                if (File.Exists(preprocessedPath))
                {
                    File.Delete(preprocessedPath);
                }
            }
        }

        // Improves OCR reliability on real-world (often imperfect) phone photos:
        // upscales small images, converts to grayscale, evens out lighting with
        // CLAHE, and sharpens to counteract camera blur.
        private static string PreprocessImageForOcr(string imagePath)
        {
            using Mat original = Cv2.ImRead(imagePath, ImreadModes.Color);
            using Mat gray = new();
            Cv2.CvtColor(original, gray, ColorConversionCodes.BGR2GRAY);

            // Text recognition works much better at higher resolution - scale up
            // any photo whose narrower side is smaller than ~1600px.
            Mat scaled = gray;
            int shorterSide = Math.Min(gray.Width, gray.Height);
            if (shorterSide < 1600 && shorterSide > 0)
            {
                double scaleFactor = 1600.0 / shorterSide;
                scaled = new Mat();
                Cv2.Resize(gray, scaled, new OpenCvSharp.Size(), scaleFactor, scaleFactor, InterpolationFlags.Cubic);
            }

            // CLAHE evens out uneven lighting/shadows across the photo, making
            // faint text much more readable to the OCR engine.
            using var clahe = Cv2.CreateCLAHE(clipLimit: 2.5, tileGridSize: new OpenCvSharp.Size(8, 8));
            using var enhanced = new Mat();
            clahe.Apply(scaled, enhanced);

            // A mild unsharp mask to counteract camera blur/soft focus.
            using var blurred = new Mat();
            Cv2.GaussianBlur(enhanced, blurred, new OpenCvSharp.Size(0, 0), 3);
            using var sharpened = new Mat();
            Cv2.AddWeighted(enhanced, 1.5, blurred, -0.5, 0, sharpened);

            string outputPath = Path.Combine(Path.GetTempPath(), $"timetable_ocr_{Guid.NewGuid():N}.png");
            Cv2.ImWrite(outputPath, sharpened);

            if (!ReferenceEquals(scaled, gray))
            {
                scaled.Dispose();
            }

            return outputPath;
        }

        private static string ExtractTextFromPdf(string pdfPath)
        {
            var textBuilder = new System.Text.StringBuilder();

            using (PdfDocument document = PdfDocument.Open(pdfPath))
            {
                foreach (UglyToad.PdfPig.Content.Page page in document.GetPages())
                {
                    textBuilder.AppendLine(page.Text);
                }
            }

            string text = textBuilder.ToString();

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new InvalidOperationException(
                    "This PDF doesn't contain a readable text layer (it's likely a scanned image saved as PDF). " +
                    "Please save/export it as a JPG or PNG photo instead and upload that.");
            }

            return text;
        }

        // Scans the raw extracted text for every recognizable subject code, and for
        // each one looks at the surrounding text for a day name, a time range, and
        // a room code. This works reasonably well for text that reads roughly
        // left-to-right per class block, but table layouts vary a lot - hence the
        // mandatory review step before anything is saved.
        public static List<TimetableEntry> ParseCandidateEntries(string rawText)
        {
            var subjectRepository = new SubjectRepository();
            List<Subject> knownSubjects = subjectRepository.LoadAll();

            var candidates = new List<TimetableEntry>();
            string normalizedText = rawText.Replace("\r\n", "\n");

            foreach (Match codeMatch in SubjectCodePattern.Matches(normalizedText))
            {
                string rawCode = codeMatch.Value.ToUpperInvariant().Replace(" ", "");
                string formattedCode = InsertSpaceAfterPst(rawCode);

                Subject? knownSubject = knownSubjects.FirstOrDefault(s =>
                    s.SubjectCode.Replace(" ", "").Equals(rawCode, StringComparison.OrdinalIgnoreCase));

                // Look at a window of text around the code for the day/time/room -
                // this is the "best effort" part that the lecturer should double-check.
                int windowStart = Math.Max(0, codeMatch.Index - 150);
                int windowLength = Math.Min(300, normalizedText.Length - windowStart);
                string window = normalizedText.Substring(windowStart, windowLength);

                DayOfWeek day = FindNearestDay(normalizedText, codeMatch.Index);
                (string startTime, string endTime) = FindTimeRange(window);
                string room = FindRoom(window);

                candidates.Add(new TimetableEntry
                {
                    SubjectCode = knownSubject?.SubjectCode ?? formattedCode,
                    SubjectName = knownSubject?.SubjectName ?? "(unknown subject - please check)",
                    Year = knownSubject?.Year ?? 0,
                    Semester = knownSubject?.Semester ?? 0,
                    Day = day,
                    StartTime = startTime,
                    EndTime = endTime,
                    Room = room
                });
            }

            return candidates;
        }

        private static string InsertSpaceAfterPst(string code)
        {
            return code.StartsWith("PST", StringComparison.OrdinalIgnoreCase) && !code.Contains(' ')
                ? "PST " + code.Substring(3)
                : code;
        }

        private static DayOfWeek FindNearestDay(string text, int position)
        {
            int bestDistance = int.MaxValue;
            DayOfWeek bestDay = DayOfWeek.Monday;

            for (int i = 0; i < DayNames.Length; i++)
            {
                foreach (Match dayMatch in Regex.Matches(text, DayNames[i], RegexOptions.IgnoreCase))
                {
                    int distance = Math.Abs(dayMatch.Index - position);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestDay = DayValues[i];
                    }
                }
            }

            return bestDay;
        }

        private static (string startTime, string endTime) FindTimeRange(string window)
        {
            Match match = TimeRangePattern.Match(window);
            if (!match.Success)
            {
                return ("08:00", "09:00");
            }

            string start = $"{match.Groups[1].Value.PadLeft(2, '0')}:{match.Groups[2].Value}";
            string end = $"{match.Groups[3].Value.PadLeft(2, '0')}:{match.Groups[4].Value}";
            return (start, end);
        }

        private static string FindRoom(string window)
        {
            Match match = RoomPattern.Match(window);
            return match.Success ? match.Value.Trim() : string.Empty;
        }
    }
}
