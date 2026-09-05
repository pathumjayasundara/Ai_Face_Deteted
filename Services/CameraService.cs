using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace FaceAttendanceSystem.Services
{
    // Wraps an OpenCvSharp VideoCapture so the Forms layer only has to deal with Bitmaps.
    public class CameraService : IDisposable
    {
        private VideoCapture? _capture;

        public bool IsOpen => _capture != null && _capture.IsOpened();

        public bool Start(int cameraIndex = 0)
        {
            _capture = new VideoCapture(cameraIndex);
            return _capture.IsOpened();
        }

        // Grabs the current frame from the webcam. Returns null if a frame could not be read.
        public Bitmap? GrabFrame()
        {
            if (_capture == null || !_capture.IsOpened())
            {
                return null;
            }

            using Mat frame = new Mat();
            _capture.Read(frame);

            if (frame.Empty())
            {
                return null;
            }

            return BitmapConverter.ToBitmap(frame);
        }

        public void Stop()
        {
            _capture?.Release();
            _capture?.Dispose();
            _capture = null;
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
