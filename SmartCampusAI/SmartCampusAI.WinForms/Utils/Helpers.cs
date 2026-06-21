using System;
using System.Windows.Forms;

namespace SmartCampusAI.Utils
{
    public static class Helpers
    {
        public static void ShowError(string message, string title = "Error")
            => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void ShowInfo(string message, string title = "Information")
            => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static bool Confirm(string message, string title = "Confirm")
            => MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
               == DialogResult.Yes;

        public static string SampleImagesFolder()
        {
            string folder = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "FaceSamples");
            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);
            return folder;
        }

        public static string StudentFaceFolder(int studentId)
        {
            string folder = System.IO.Path.Combine(SampleImagesFolder(), $"Student_{studentId}");
            if (!System.IO.Directory.Exists(folder))
                System.IO.Directory.CreateDirectory(folder);
            return folder;
        }
    }
}
