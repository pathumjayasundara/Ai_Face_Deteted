using System.Drawing;
using System.Windows.Forms;
using SmartCampusAI.Styles;

namespace SmartCampusAI.Styles
{
    public static class Theme
    {
        public static Font HeaderFont  => new("Segoe UI", 14, FontStyle.Bold);
        public static Font LabelFont   => new("Segoe UI", 9,  FontStyle.Regular);
        public static Font InputFont   => new("Segoe UI", 10, FontStyle.Regular);
        public static Font ButtonFont  => new("Segoe UI", 10, FontStyle.Bold);
        public static Font SmallFont   => new("Segoe UI", 8,  FontStyle.Regular);

        public static void StylePrimaryButton(Button btn)
        {
            btn.BackColor   = ColorPalette.MidBlue;
            btn.ForeColor   = ColorPalette.White;
            btn.Font        = ButtonFont;
            btn.FlatStyle   = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize  = 0;
            btn.Cursor      = Cursors.Hand;
            btn.Height      = 38;
        }

        public static void StyleDangerButton(Button btn)
        {
            StylePrimaryButton(btn);
            btn.BackColor = ColorPalette.Danger;
        }

        public static void StyleSuccessButton(Button btn)
        {
            StylePrimaryButton(btn);
            btn.BackColor = ColorPalette.Success;
        }

        public static void StyleWarningButton(Button btn)
        {
            StylePrimaryButton(btn);
            btn.BackColor = ColorPalette.Warning;
            btn.ForeColor = ColorPalette.TextDark;
        }

        public static void StyleTextBox(TextBox tb)
        {
            tb.Font        = InputFont;
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.BackColor   = ColorPalette.White;
        }

        public static void StyleDataGrid(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode            = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode                  = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly                       = true;
            dgv.AllowUserToAddRows             = false;
            dgv.BackgroundColor                = ColorPalette.White;
            dgv.BorderStyle                    = BorderStyle.None;
            dgv.RowHeadersVisible              = false;
            dgv.EnableHeadersVisualStyles      = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor  = ColorPalette.NavyBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor  = ColorPalette.White;
            dgv.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.ColumnHeadersHeight            = 36;
            dgv.RowTemplate.Height             = 30;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorPalette.LightBlue;
            dgv.DefaultCellStyle.Font          = new Font("Segoe UI", 9);
            dgv.DefaultCellStyle.SelectionBackColor      = ColorPalette.SkyBlue;
            dgv.DefaultCellStyle.SelectionForeColor      = ColorPalette.White;
            dgv.CellBorderStyle                = DataGridViewCellBorderStyle.SingleHorizontal;
        }
    }
}
