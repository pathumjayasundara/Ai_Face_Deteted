using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace SmartCampusAI.Services
{
    /// <summary>
    /// Manages webcam capture using OpenCvSharp.
    /// Raises FrameReady event on each new frame.
    /// Thread-safe; all heavy work runs on a background Task.
    /// </summary>
    public sealed class CameraService : IDisposable
    {
        private VideoCapture? _capture;
        private CancellationTokenSource? _cts;
        private Task? _captureTask;
        private bool _disposed;

        public bool IsRunning { get; private set; }

        /// <summary>Fires on every captured frame. Bitmap is caller's to dispose.</summary>
        public event EventHandler<Bitmap>? FrameReady;

        /// <summary>
        /// Starts the webcam. cameraIndex 0 = default webcam; 1+ = external/DroidCam.
        /// </summary>
        public void Start(int cameraIndex = 0)
        {
            if (IsRunning) return;

            _capture = new VideoCapture(cameraIndex);
            if (!_capture.IsOpened())
                throw new InvalidOperationException(
                    $"Cannot open camera at index {cameraIndex}. " +
                    "Ensure your webcam or DroidCam is connected and not in use.");

            // Reasonable preview resolution
            _capture.Set(VideoCaptureProperties.FrameWidth,  640);
            _capture.Set(VideoCaptureProperties.FrameHeight, 480);
            _capture.Set(VideoCaptureProperties.Fps, 30);

            _cts         = new CancellationTokenSource();
            IsRunning    = true;
            _captureTask = Task.Run(() => CaptureLoop(_cts.Token), _cts.Token);
        }

        /// <summary>Stops the webcam cleanly.</summary>
        public void Stop()
        {
            if (!IsRunning) return;

            IsRunning = false;
            _cts?.Cancel();

            try { _captureTask?.Wait(2000); } catch { /* ignore */ }

            _capture?.Release();
            _capture?.Dispose();
            _capture = null;
            _cts?.Dispose();
            _cts = null;
        }

        /// <summary>Grabs a single raw frame (no detection overlay). Returns null if not running.</summary>
        public Bitmap? GrabFrame()
        {
            if (_capture == null || !_capture.IsOpened()) return null;

            using Mat frame = new();
            if (!_capture.Read(frame) || frame.Empty()) return null;
            return BitmapConverter.ToBitmap(frame);
        }

        // ── PRIVATE ────────────────────────────────────────────────────────────
        private void CaptureLoop(CancellationToken token)
        {
            using Mat frame = new();
            while (!token.IsCancellationRequested && IsRunning)
            {
                if (_capture == null || !_capture.IsOpened()) break;

                if (!_capture.Read(frame) || frame.Empty())
                {
                    Thread.Sleep(30);
                    continue;
                }

                Bitmap bmp = BitmapConverter.ToBitmap(frame);
                FrameReady?.Invoke(this, bmp);

                // ~30 fps cap
                Thread.Sleep(33);
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            Stop();
            _disposed = true;
        }
    }
}
