using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Pixelcraft_2
{
    public static class Extensions
    {
        public static Color Divide(this Color color, int value) => Color.FromArgb(255, color.R / value, color.G / value, color.B / value);

        public static Color Divide(this Color color, double value) => Color.FromArgb(255, (int)(color.R / value), (int)(color.G / value), (int)(color.B / value));

        public static Color Text(this Color color) => (color.R + color.G + color.B) / 3 < 128 ? Color.White : Color.Black;
    }

    public class ProgressBarEx : ProgressBar
    {
        private SolidBrush brush = null;

        public ProgressBarEx()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (brush == null || brush.Color != this.ForeColor)
                brush = new SolidBrush(this.ForeColor);

            Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, rec);
            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(brush, 2, 2, rec.Width, rec.Height);
        }
    }
}