using System.Drawing.Drawing2D;

namespace FaceAttendanceSystem.Controls
{
    // A single rounded "speech bubble" used in the AI Assistant chat screen -
    // bot messages align left in maroon, the lecturer's own messages align
    // right in gold, similar to a modern chat app.
    public class ChatBubbleControl : Panel
    {
        private readonly Label _messageLabel;
        private Color _bubbleColor;

        public ChatBubbleControl(string message, bool isFromUser, int maxBubbleWidth)
        {
            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw,
                true);

            _bubbleColor = isFromUser
                ? Color.FromArgb(212, 160, 23)
                : Color.FromArgb(141, 21, 58);

            BackColor = Color.Transparent;
            Padding = new Padding(16, 10, 16, 10);

            _messageLabel = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(maxBubbleWidth - 32, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Text = message
            };
            Controls.Add(_messageLabel);

            _messageLabel.Location = new Point(Padding.Left, Padding.Top);
            Size = new Size(
                _messageLabel.PreferredWidth + Padding.Left + Padding.Right,
                _messageLabel.PreferredHeight + Padding.Top + Padding.Bottom);

            _messageLabel.SizeChanged += (s, e) =>
            {
                Size = new Size(
                    _messageLabel.PreferredWidth + Padding.Left + Padding.Right,
                    _messageLabel.PreferredHeight + Padding.Top + Padding.Bottom);
            };
        }

        public void UpdateText(string message)
        {
            _messageLabel.Text = message;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using GraphicsPath path = RoundedRect(new Rectangle(0, 0, Width - 1, Height - 1), 14);
            using var brush = new SolidBrush(_bubbleColor);
            g.FillPath(brush, path);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();
            var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
