namespace FaceAttendanceSystem.Data
{
    public static class FilePaths
    {
        private static readonly string DataDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "XML Local Data Files");

        private static readonly string PhotosDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Student Photos");

        private static readonly string TrainingPhotosDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Training Photos");

        public static readonly string StudentsFilePath = Path.Combine(DataDirectory, "students.xml");
        public static readonly string AttendanceFilePath = Path.Combine(DataDirectory, "attendance.xml");
        public static readonly string SubjectsFilePath = Path.Combine(DataDirectory, "subjects.xml");
        public static readonly string TimetableFilePath = Path.Combine(DataDirectory, "timetable.xml");

        public static readonly string ShapePredictorModelPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "shape_predictor_68_face_landmarks.dat");

        public static readonly string FaceRecognitionModelPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Models", "dlib_face_recognition_resnet_model_v1.dat");

        public static readonly string UniversityLogoPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "university_logo.png");

        public static readonly string FacultyLogoPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "faculty_logo.png");

        public static readonly string AppIconPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "app_icon.ico");

        public static readonly string CampusPhotosFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "CampusPhotos");

        // TesseractEngine's "datapath" parameter must be the PARENT directory that
        // CONTAINS a folder literally named "tessdata" (e.g. if the trained data
        // file lives at "...\bin\Debug\tessdata\eng.traineddata", this should be
        // "...\bin\Debug", NOT "...\bin\Debug\tessdata" - that's a common mistake).
        public static readonly string TessDataParentFolder = AppDomain.CurrentDomain.BaseDirectory;

        public static readonly string TessDataFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata", "eng.traineddata");

        public static readonly string LecturersFilePath = Path.Combine(DataDirectory, "lecturers.xml");

        public static readonly string EmailSettingsFilePath = Path.Combine(DataDirectory, "email-settings.xml");
        public static readonly string EmailLogFilePath = Path.Combine(DataDirectory, "email-log.txt");
        public static readonly string AdminAccountFilePath = Path.Combine(DataDirectory, "admin-account.xml");

        static FilePaths()
        {
            Directory.CreateDirectory(DataDirectory);
            Directory.CreateDirectory(PhotosDirectory);
            Directory.CreateDirectory(TrainingPhotosDirectory);
        }

        public static string GetPhotoPath(string regNumber) =>
            Path.Combine(PhotosDirectory, regNumber + ".jpg");

        // A temporary folder to hold the 5 raw training photos while they're being
        // captured. It gets zipped up (and then deleted) once registration is complete.
        public static string GetTrainingPhotosFolder(string regNumber) =>
            Path.Combine(TrainingPhotosDirectory, regNumber);

        // Where the zipped-up training photos end up living permanently (much smaller
        // than keeping 5 loose JPEGs per student).
        public static string GetTrainingPhotosZipPath(string regNumber) =>
            Path.Combine(TrainingPhotosDirectory, regNumber + ".zip");
    }
}
