using FaceAttendanceSystem.Data;
using FaceAttendanceSystem.Models;

namespace FaceAttendanceSystem.Forms
{
    public partial class UC_Home : UserControl
    {
        public UC_Home(Lecturer lecturer)
        {
            InitializeComponent();
            welcomeLabel.Text = $"Welcome, {lecturer.FullName}!\nDepartment of Physical Science and Technology";
            LoadLogo();
            LoadStudentsList();
            LoadSlideshow();
        }

        private void LoadSlideshow()
        {
            try
            {
                if (Directory.Exists(FilePaths.CampusPhotosFolder))
                {
                    List<string> photoPaths = Directory
                        .GetFiles(FilePaths.CampusPhotosFolder, "*.jpg")
                        .OrderBy(p => p)
                        .ToList();

                    slideshowPanel.LoadPhotos(photoPaths);
                }
            }
            catch
            {
                // The slideshow is decorative only - the dashboard still works fine without it.
            }
        }

        private void LoadLogo()
        {
            try
            {
                if (File.Exists(FilePaths.UniversityLogoPath))
                {
                    logoPictureBox.Image = Image.FromFile(FilePaths.UniversityLogoPath);
                }
            }
            catch
            {
                // Logo is decorative only - the dashboard still works fine without it.
            }
        }

        private void LoadStudentsList()
        {
            List<Models.Student> students = new StudentRepository().LoadAll()
                .OrderBy(s => s.FullName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            studentsListTitleLabel.Text = $"Registered Students ({students.Count})";

            // "#" is the alphabetical index number (1, 2, 3...) once the list is
            // sorted by name - not the student's registration number.
            studentsListGrid.DataSource = students
                .Select((s, index) => new
                {
                    Index = index + 1,
                    s.RegNumber,
                    s.FullName,
                    s.Course,
                    s.Email
                })
                .ToList();

            if (studentsListGrid.Columns["Index"] != null)
            {
                studentsListGrid.Columns["Index"].HeaderText = "#";
                studentsListGrid.Columns["Index"].FillWeight = 25;
            }
        }
    }
}
