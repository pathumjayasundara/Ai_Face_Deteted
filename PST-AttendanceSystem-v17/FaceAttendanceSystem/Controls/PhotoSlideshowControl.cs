using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FaceAttendanceSystem.Controls
{
    // Auto-advancing photo slideshow: shows each image for a few seconds, then
    // smoothly crossfades into the next one. Built directly on GDI+ (no extra
    // NuGet dependency) and kept flicker-free via double buffering.
    public class PhotoSlideshowControl : Panel
    {
        private readonly List<Image> _photos = new();
        private readonly System.Windows.Forms.Timer _advanceTimer = new() { Interval = 2000 };
        private readonly System.Windows.Forms.Timer _fadeTimer = new() { Interval = 16 };

        private int _currentIndex;
        private int _nextIndex;
        private float _fadeProgress; // 0 = fully on current photo, 1 = fully on next photo
        private bool _isFading;

        private const int FadeDurationMs = 600;

        public PhotoSlideshowControl()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            UpdateStyles();

            _advanceTimer.Tick += (s, e) => AdvanceToNextPhoto();
            _fadeTimer.Tick += (s, e) => StepFade();

            Disposed += (s, e) =>
            {
                _advanceTimer.Dispose();
                _fadeTimer.Dispose();
                foreach (Image photo in _photos)
                {
                    photo.Dispose();
                }
            };
        }

        public void LoadPhotos(IEnumerable<string> imagePaths)
        {
            foreach (Image photo in _photos)
            {
                photo.Dispose();
            }
            _photos.Clear();

            foreach (string path in imagePaths)
            {
                try
                {
                    // Load into memory (not a file lock) so the photo files
                    // themselves stay free to be read elsewhere if needed.
                    using FileStream stream = new(path, FileMode.Open, FileAccess.Read);
                    _photos.Add(Image.FromStream(stream));
                }
                catch
                {
                    // Skip any photo that can't be loaded rather than crashing the whole slideshow.
                }
            }

            _currentIndex = 0;
            _nextIndex = _photos.Count > 1 ? 1 : 0;
            _fadeProgress = 0;
            _isFading = false;

            Invalidate();

            if (_photos.Count > 1)
            {
                _advanceTimer.Start();
            }
        }

        private void AdvanceToNextPhoto()
        {
            if (_photos.Count < 2 || _isFading)
            {
                return;
            }

            _nextIndex = (_currentIndex + 1) % _photos.Count;
            _fadeProgress = 0;
            _isFading = true;
            _fadeTimer.Start();
        }

        private void StepFade()
        {
            _fadeProgress += 16f / FadeDurationMs;

            if (_fadeProgress >= 1f)
            {
                _fadeProgress = 1f;
                _isFading = false;
                _fadeTimer.Stop();
                _currentIndex = _nextIndex;
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_photos.Count == 0 || Width <= 0 || Height <= 0)
            {
                return;
            }

            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle destRect = ClientRectangle;

            DrawPhotoCover(g, _photos[_currentIndex], destRect, 1f);

            if (_isFading && _nextIndex != _currentIndex)
            {
                DrawPhotoCover(g, _photos[_nextIndex], destRect, _fadeProgress);
            }
        }

        // Draws the image scaled to fully cover the control (like CSS "background-size: cover"),
        // cropping any overflow, so photos of different aspect ratios all fill the space neatly.
        private static void DrawPhotoCover(Graphics g, Image photo, Rectangle destRect, float opacity)
        {
            float destAspect = (float)destRect.Width / destRect.Height;
            float srcAspect = (float)photo.Width / photo.Height;

            Rectangle srcRect;
            if (srcAspect > destAspect)
            {
                int srcWidth = (int)(photo.Height * destAspect);
                int srcX = (photo.Width - srcWidth) / 2;
                srcRect = new Rectangle(srcX, 0, srcWidth, photo.Height);
            }
            else
            {
                int srcHeight = (int)(photo.Width / destAspect);
                int srcY = (photo.Height - srcHeight) / 2;
                srcRect = new Rectangle(0, srcY, photo.Width, srcHeight);
            }

            if (opacity >= 0.999f)
            {
                g.DrawImage(photo, destRect, srcRect, GraphicsUnit.Pixel);
                return;
            }

            var colorMatrix = new ColorMatrix { Matrix33 = opacity };
            using var attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            g.DrawImage(photo, destRect, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, attributes);
        }
    }
}
