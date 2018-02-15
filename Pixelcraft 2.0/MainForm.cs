using Mycan_Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
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
            DoubleBuffered = true;
            _preview = new PreviewForm();
            _work = Work.GetWork();
            _work.Tick += _work_Tick;
            _work.ConvertThreadEnd += _work_ConvertEnd;
            _work.TextureThreadEnd += _work_TextureEnd;

            foreach (Control c in Controls)
            {
                c.TabStop = false;
            }
            foreach (Control c in pnl_menuBar.Controls)
            {
                c.TabStop = false;
            }

            List<CheckBox> test = new List<CheckBox>();
            for (int i = 0; i < 5; i++)
            {
                test.Add(new CheckBox
                {
                    Text = "Test " + i.ToString(),
                    Dock = DockStyle.Top
                });
            }
            pnl_checks.Controls.Add(_work.CheckPanel.Panel);
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
            //
            btn_Close.FlatAppearance.BorderColor = color2;
            btn_Close.BackColor = color3;
            btn_Close.ForeColor = color3.Text();
            //Misc
            pnl_menuBar.BackColor = color3;
            pnl_menuBar.ForeColor = color3.Text();
            //
            pbx_Convert.BackColor = color3.Shift(ColorShift.GBR);
            pbx_Convert.ForeColor = color.Shift(ColorShift.GBR);

        }

        #region Convert Image Thread End
        /// <summary>
        /// Is called when the conversion thread ends to set the preview image
        /// </summary>
        /// <param name="source">Needed for events, I dont use it, you can just set it to null</param>
        /// <param name="e">Gives the method the image needed for setting</param>
        private void _work_ConvertEnd(object source, ConvertImageEndArgs e)
        {
            if (InvokeRequired)
            {
                ImageThreadEnd te = new ImageThreadEnd(UpdatePreviewImage);
                Invoke
                    (te, new object[] { e.GetBitmap() });
            }
            else
            {
                UpdatePreviewImage(e.GetBitmap());
            }
        }

        /// <summary>
        /// Sets the preview image
        /// </summary>
        /// <param name="bitmap">The image to set it to</param>
        private void UpdatePreviewImage(Canvas bitmap)
        {
            _preview.pcb_Preview.Image = bitmap.GetImage();
            _preview.SetColor(Color.FromArgb(bitmap.AverageColor()));
            pbx_Convert.Hide();
            EnableButton(DisableButtonMode.Convert);
        }
        #endregion

        #region Load Texture Thread End
        private void _work_TextureEnd(object source, EventArgs e)
        {
            if (InvokeRequired)
            {
                TextureThreadEnd te = new TextureThreadEnd(UpdateTextureEnd);
                Invoke
                    (te);
            }
            else
            {
                UpdateTextureEnd();
            }
        }

        private void UpdateTextureEnd() => EnableButton(DisableButtonMode.Texture);
        #endregion

        private void _work_Tick(object source, UpdateEventArgs e)
        {
            int val = (int)((e.Percent * 1000) <= 1000 ? e.Percent * 1000 : 1000);
            if (InvokeRequired)
            {
                SetProgressBar spb = new SetProgressBar(UpdateProgressBar);
                Invoke
                    (spb, new object[] { val });
            }
            else
            {
                pbx_Convert.Value = val;
                Invalidate();
            }
        }

        /// <summary>
        /// Updates the progress bar with the value specified
        /// </summary>
        /// <param name="value">A number between 1 and 1000</param>
        private void UpdateProgressBar(int value) => pbx_Convert.Value = value;

        /// <summary>
        /// Loads an image into the picture box to prepare for conversion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_LoadImage_Click(object sender, EventArgs e)
        {
            ofd_LoadImage.FileName = "";
            var result = ofd_LoadImage.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                _originalImage = new Bitmap(Image.FromFile(ofd_LoadImage.FileName));
                pcb_Original.Image = _originalImage;
                //Backcolor Change
                Color color = Color.FromArgb((int)(0xff000000 | new Canvas(_originalImage).AverageColor()));
                SetColor(color);
            }
        }

        /// <summary>
        /// Converts the image in the picture box into another image and a minecraft schematic file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ConvertImage_Click(object sender, EventArgs e)
        {
            if (pcb_Original.Image == null) Btn_LoadImage_Click(sender, e);
            if (pcb_Original.Image == null) return;
            //_preview.pcb_Preview.Image = _work.Test(new Bitmap(pcb_Original.Image));
            DisableButton(DisableButtonMode.Convert);
            pbx_Convert.Show();
            int x = rad_useWidth.Checked ? (int)nud_input.Value : -1;
            int y = rad_useHeight.Checked ? (int)nud_input.Value : -1;
            if (rad_maxRes.Checked) x = y = -1;
            _work.ConvertImageThread(new Bitmap(pcb_Original.Image), x, y);
        }

        /// <summary>
        /// Loads a texture pack for use when calulating the blocks used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_LoadTexture_Click(object sender, EventArgs e)
        {
            DisableButton(DisableButtonMode.Texture);
            if (ofd_LoadTexture.ShowDialog() != DialogResult.Cancel) _work.LoadTextureThread(ofd_LoadTexture.FileName);
        }

        /// <summary>
        /// Disables buttons according to what thread is running
        /// </summary>
        /// <param name="mode">Specifies the thread running</param>
        private void DisableButton(DisableButtonMode mode)
        {
            switch (mode)
            {
                case DisableButtonMode.Convert:
                    btn_ConvertImage.Disable();
                    btn_LoadImage.Disable();
                    btn_LoadTexture.Disable();
                    break;
                case DisableButtonMode.Texture:
                    btn_ConvertImage.Disable();
                    btn_LoadTexture.Disable();
                    break;
            }
            btn_cancel.Show();
        }

        /// <summary>
        /// Enables buttons according to what thread is running
        /// </summary>
        /// <param name="mode">Specifies which thread is running</param>
        private void EnableButton(DisableButtonMode mode)
        {
            switch (mode)
            {
                case DisableButtonMode.Convert:
                    btn_ConvertImage.Enable();
                    btn_LoadImage.Enable();
                    btn_LoadTexture.Enable();
                    break;
                case DisableButtonMode.Texture:
                    btn_ConvertImage.Enable();
                    btn_LoadTexture.Enable();
                    break;
            }
            btn_cancel.Hide();
        }

        /// <summary>
        /// Opens up the preview menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Preview_Click(object sender, EventArgs e) => _preview.Show(true);


        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            _work.InterruptConvertThread();
            _work.InterruptTextureThread();
            EnableButton(DisableButtonMode.Convert);
            EnableButton(DisableButtonMode.Texture);
            btn_cancel.Hide();
            pbx_Convert.Hide();
        }

        //Menu Bar Stuff
        #region Menu Bar
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private void Btn_Maximize_Click(object sender, EventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

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
        private void Pnl_menuBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            _snapX = e.X;
            _snapY = e.Y;
            _moving = true;
        }

        private void Pnl_menuBar_MouseUp(object sender, EventArgs e) => _moving = false;

        private void Pnl_menuBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_moving && e.Button == MouseButtons.Left)
            {
                int x = _snapX - e.X;
                int y = _snapY - e.Y;
                Left -= x;
                Top -= y;
            }
        }
        #endregion
    }
}
