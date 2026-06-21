using System.Drawing;

namespace SmartCampusAI.Styles
{
    public static class ColorPalette
    {
        // Primary blues
        public static readonly Color NavyBlue    = Color.FromArgb(22,  43,  72);
        public static readonly Color MidBlue     = Color.FromArgb(27,  96, 178);
        public static readonly Color SkyBlue     = Color.FromArgb(66, 135, 245);
        public static readonly Color LightBlue   = Color.FromArgb(219, 234, 254);

        // Accent
        public static readonly Color Success     = Color.FromArgb(34, 197,  94);
        public static readonly Color Danger      = Color.FromArgb(239,  68,  68);
        public static readonly Color Warning     = Color.FromArgb(251, 191,  36);
        public static readonly Color Info        = Color.FromArgb(59, 130, 246);

        // Neutral
        public static readonly Color White       = Color.White;
        public static readonly Color LightGray   = Color.FromArgb(248, 249, 250);
        public static readonly Color BorderGray  = Color.FromArgb(206, 212, 218);
        public static readonly Color TextDark    = Color.FromArgb(33,   37,  41);
        public static readonly Color TextMuted   = Color.FromArgb(108, 117, 125);
    }
}
