using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Pixelcraft_2
{
    public static class Extensions
    {
        public static Color Divide(this Color color, int value) => Color.FromArgb(255, color.R / value, color.G / value, color.B / value);

        public static Color Divide(this Color color, double value) => Color.FromArgb(255, (int)(color.R / value), (int)(color.G / value), (int)(color.B / value));

        public static Color Text(this Color color) => (color.R + color.G + color.B) / 3 < 128 ? Color.White : Color.Black;

        public static Color Invert(this Color color) => Color.FromArgb(255, 255 - color.R, 255 - color.G, 255 - color.G);

        public static Color Shift(this Color color, ColorShift shift)
        {
            switch (shift)
            {
                case ColorShift.BGR:
                    return Color.FromArgb(255, color.B, color.G, color.R);
                case ColorShift.BRG:
                    return Color.FromArgb(255, color.B, color.R, color.G);
                case ColorShift.GBR:
                    return Color.FromArgb(255, color.G, color.B, color.R);
                case ColorShift.GRB:
                    return Color.FromArgb(255, color.G, color.R, color.B);
                case ColorShift.RBG:
                    return Color.FromArgb(255, color.R, color.B, color.G);
                case ColorShift.RGB:
                    return Color.FromArgb(255, color);
                default:
                    return Color.FromArgb(255, color);
            }
        }

        public static int[] CompressToInt(this byte[] ba)
        {
            if (ba.Length % 4 != 0) throw new ArgumentException();
            int[] ia = new int[ba.Length / 4];
            for (int i = 0; i < ia.Length; i++)
            {
                ia[i] = 0;
                ia[i] += ba[i * 4 + 0] << 24; //a
                ia[i] += ba[i * 4 + 1] << 16; //r
                ia[i] += ba[i * 4 + 2] << 8;  //b
                ia[i] += ba[i * 4 + 3];       //g
            }
            return ia;
        }

        public static byte[] DecompressToByte(this int[] ia)
        {
            byte[] ba = new byte[ia.Length * 4];
            for (int i = 0; i < ia.Length; i++)
            {
                ba[i * 4 + 0] = (byte)((ia[i] & 0xff000000) >> 24);
                ba[i * 4 + 1] = (byte)((ia[i] & 0x00ff0000) >> 16);
                ba[i * 4 + 2] = (byte)((ia[i] & 0x0000ff00) >> 8);
                ba[i * 4 + 3] = (byte)(ia[i] & 0x000000ff);
            }
            return ba;
        }

        public static void Disable(this Control control) => control.Enabled = false;

        public static void Enable(this Control control) => control.Enabled = true;

        public static double StandardDeviation(this List<int> valueList)
        {
            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (double value in valueList)
            {
                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - 2));
        }

        internal static List<BlockData> Extract(this List<BlockEntity> list)
        {
            List<BlockData> newList = new List<BlockData>();
            foreach (BlockEntity b in list)
            {
                newList.Add(b.Block);
            }
            return newList;
        }

        internal static int Q(this int i) => i * i;
    }

    public enum ColorShift
    {
        /// <summary>
        /// Doesn't change the color at all
        /// </summary>
        RGB = 0,
        /// <summary>
        /// Swaps the green and blue channels
        /// </summary>
        RBG = 1,
        /// <summary>
        /// Swaps the red and green channels
        /// </summary>
        GRB = 2,
        /// <summary>
        /// Makes Red = Green, Green = Blue, and Blue = Red
        /// </summary>
        GBR = 3,
        /// <summary>
        /// Makes Red = Blue, Green - Red, Blue = Green
        /// </summary>
        BRG = 4,
        /// <summary>
        /// Swaps thr red and blue channels
        /// </summary>
        BGR = 5
    }

    public enum DisableButtonMode
    {
        Texture = 0,
        Convert = 1
    }

    public class ProgressBarEx : ProgressBar
    {
        public ProgressBarEx()
        {
            //ControlCollection controlCollection = new ControlCollection(this);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            const int inset = 2;
            using (Image offscreenImage = new Bitmap(this.Width, this.Height))
            {
                using (Graphics offscreen = Graphics.FromImage(offscreenImage))
                {
                    Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

                    if (ProgressBarRenderer.IsSupported)
                        ProgressBarRenderer.DrawHorizontalBar(offscreen, rect);

                    rect.Inflate(new Size(-inset, -inset)); // Deflate inner rect.
                    rect.Width = (int)(rect.Width * ((double)this.Value / this.Maximum));
                    if (rect.Width == 0) rect.Width = 1; // Can't draw rec with width of 0.

                    LinearGradientBrush brush = new LinearGradientBrush(rect, this.BackColor, this.ForeColor, LinearGradientMode.Vertical);
                    offscreen.FillRectangle(brush, inset, inset, rect.Width, rect.Height);

                    e.Graphics.DrawImage(offscreenImage, 0, 0);
                    offscreenImage.Dispose();
                }
            }
        }
    }

    public class CheckPanel
    {
        private Panel _panel = new Panel();
        private Dictionary<string, Panel> _panels = new Dictionary<string, Panel>();
        //private List<CheckBox> _checks = new List<CheckBox>();

        public Panel Panel { get => _panel; set => _panel = value; }

        internal void Set(BlockDataCollection collection)
        {
            _panel = new Panel
            {
                Dock = DockStyle.Fill
            };
            _panels = new Dictionary<string, Panel>();
            foreach (KeyValuePair<string, List<BlockEntity>> k in collection.Collections)
            {
                Panel panel = new Panel
                {
                    Dock = DockStyle.Left,
                    Width = 100
                };
                foreach (BlockEntity entity in k.Value)
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Dock = DockStyle.Top,
                        Text = entity.Name,
                        Checked = entity.Enabled,
                    };
                    panel.Controls.Add(checkBox);
                }
                panel.Controls.Add(new Label
                {
                    Text = k.Key,
                    Dock = DockStyle.Top
                });
                _panels.Add(k.Key, panel);
            }
            _panel.Controls.AddRange(_panels.Values.ToArray());
        }
    }
}