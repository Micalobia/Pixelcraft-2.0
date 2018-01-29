using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixelcraft_2
{
    public partial class MainForm : Form
    {
        private PreviewForm _preview;

        private Work _work;

        private Bitmap _originalImage = null;

        private int _snapX, _snapY;
        private bool _moving;

        public MainForm()
        {
            InitializeComponent();
            _preview = new PreviewForm();
            _work = Work.GetWork();
            _work.Tick += _work_Tick;
            _work.ThreadEnd += _work_ThreadEnd;

            foreach(Control c in Controls)
            {
                c.TabStop = false;
            }
            foreach(Control c in pnl_menuBar.Controls)
            {
                c.TabStop = false;
            }
            GotFocus += MainForm_GotFocus;
        }

        private void SetColor(Color color)
        {
            Color color2 = color.Divide(1.5);
            Color color3 = color.Divide(3);
            Color color4 = color.Divide(6);
            //Form
            BackColor = color2;
            ForeColor = color2.Text();
            //Buttons
            btn_ConvertImage.FlatAppearance.BorderColor = color2;
            btn_ConvertImage.BackColor = color3;
            btn_ConvertImage.ForeColor = color3.Text();
            //
            btn_LoadImage.FlatAppearance.BorderColor = color2;
            btn_LoadImage.BackColor = color3;
            btn_LoadImage.ForeColor = color3.Text();
            //
            btn_LoadTexture.FlatAppearance.BorderColor = color2;
            btn_LoadTexture.BackColor = color3;
            btn_LoadTexture.ForeColor = color3.Text();
            //
            btn_Preview.FlatAppearance.BorderColor = color2;
            btn_Preview.BackColor = color3;
            btn_Preview.ForeColor = color3.Text();
            //
            btn_Close.FlatAppearance.BorderColor = color3;
            btn_Close.BackColor = color4;
            btn_Close.ForeColor = color4.Text();
            //
            btn_Maximize.FlatAppearance.BorderColor = color3;
            btn_Maximize.BackColor = color4;
            btn_Maximize.ForeColor = color4.Text();
            //Labels
            lbl_Height.BackColor = color2;
            lbl_Height.ForeColor = color2.Text();
            //
            lbl_Width.BackColor = color2;
            lbl_Width.ForeColor = color2.Text();
            //Misc
            chk_Height.BackColor = color2;
            chk_Height.ForeColor = color2.Text();
            //
            chk_Width.BackColor = color2;
            chk_Width.ForeColor = color2.Text();
            //
            pnl_menuBar.BackColor = color3;
            pnl_menuBar.ForeColor = color3.Text();
            //
            pgs_Convert.BackColor = color;
            pgs_Convert.ForeColor = color;

        }

        #region Thread End
        /// <summary>
        /// Is called when the conversion thread ends to set the preview image
        /// </summary>
        /// <param name="source">Needed for events, I dont use it, you can just set it to null</param>
        /// <param name="e">Gives the method the image needed for setting</param>
        private void _work_ThreadEnd(object source, ConvertImageEndArgs e)
        {
            if (InvokeRequired)
            {
                ThreadEnd te = new ThreadEnd(UpdatePreviewImage);
                Invoke
                    (te, new object[] { e.GetBitmap() });
            }
            else
            {
                _preview.SetColor(_work.AverageColor(new Bitmap(pcb_Original.Image)));
                _preview.pcb_Preview.Image = e.GetBitmap();
                pgs_Convert.Hide();
            }

            e.GetBitmap().Dispose();
        }

        /// <summary>
        /// Sets the preview image
        /// </summary>
        /// <param name="bitmap">The image to set it to</param>
        private void UpdatePreviewImage(Bitmap bitmap)
        {
            _preview.pcb_Preview.Image = (bitmap.Clone() as Bitmap);
            _preview.SetColor(_work.AverageColor(new Bitmap(pcb_Original.Image)));
            bitmap.Dispose();
            pgs_Convert.Hide();
        }
        #endregion

        private void _work_Tick(object source, UpdateEventArgs e)
        {
            int val = (int)((e.Percent* 1000) <= 1000 ? e.Percent* 1000 : 1000);
            if (InvokeRequired)
            {
                SetProgressBar spb = new SetProgressBar(UpdateProgressBar);
                Invoke
                    (spb, new object[] { val });
            }
            else
            {
                pgs_Convert.Value = val;
            }
        }

        private void UpdateProgressBar(int value) => pgs_Convert.Value = value;

        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            ofd_LoadImage.FileName = "";
            var result = ofd_LoadImage.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                _originalImage = new Bitmap(Image.FromFile(ofd_LoadImage.FileName));
                pcb_Original.Image = _originalImage;
            }
            //Backcolor Change
            Color color = Color.FromArgb(255, _work.AverageColor(new Bitmap(pcb_Original.Image)));
            SetColor(color);

        }

        private void Btn_ConvertImage_Click(object sender, EventArgs e)
        {
            pgs_Convert.Show();
            
            _work.ConvertImageThread(new Bitmap(pcb_Original.Image), nud_Width.Enabled ? (int)nud_Width.Value : -1, nud_Height.Enabled ? (int)nud_Height.Value : -1, true);
        }

        private void btn_LoadTexture_Click(object sender, EventArgs e)
        {
            SetColor(Color.Red);
        }

        private void Btn_Preview_Click(object sender, EventArgs e)
        {
            _preview.Show();
            _preview.Focus();
        }

        private void MainForm_GotFocus(object sender, EventArgs e) => _preview.Hide();

        private void Chk_Width_CheckedChanged(object sender, EventArgs e) => nud_Width.Enabled = !chk_Width.Checked;

        private void Chk_Height_CheckedChanged(object sender, EventArgs e) => nud_Height.Enabled = !chk_Height.Checked;

        //Menu Bar Stuff
        #region Menu Bar
        private void btn_Close_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private void btn_Maximize_Click(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    WindowState = FormWindowState.Normal;
                    pcb_Original.Size = new Size(512, 512);
                    break;
                case FormWindowState.Normal:
                    WindowState = FormWindowState.Maximized;
                    pcb_Original.Size = new Size(pcb_Original.Size.Height, pcb_Original.Size.Height);
                    break;
                default:
                    break;
            }
        }
        private void pnl_menuBar_MouseDown(object sender, MouseEventArgs e)
        {
            _snapX = e.X;
            _snapY = e.Y;
            _moving = true;
        }
        private void pnl_menuBar_MouseUp(object sender, EventArgs e)
        {
            _moving = false;
        }
        private void pnl_menuBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_moving && e.Button == MouseButtons.Left)
            {
                int x = _snapX - e.X;
                int y = _snapY - e.Y;
                Left -= x;
                Top -= y;
                Console.WriteLine("{0}, {1}, {2}, {3}", x, y, Left, Top);
            }
        }
        #endregion
    }
}
