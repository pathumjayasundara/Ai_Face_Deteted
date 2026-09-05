namespace FaceAttendanceSystem.Controls
{
    // A plain Panel does not double-buffer its own painting, so repeatedly calling
    // Invalidate() on it (as an animation does) causes visible flicker - the panel's
    // default background flashes through for a frame before the custom Paint code
    // draws over it, which looks like a "white line/tear" sweeping across the panel.
    // Turning on these three ControlStyles fixes that completely.
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);
            UpdateStyles();
        }
    }
}
