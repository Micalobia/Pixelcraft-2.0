using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixelcraft_2
{
    public partial class PreviewForm : Form
    {

        private Work _work;

        public PreviewForm()
        {
            InitializeComponent();
            _work = Work.GetWork();
        }

        private void Btn_SaveImage_Click(object sender, EventArgs e)
        {
            sfg_SaveImage.FileName = "";
            if(sfg_SaveImage.ShowDialog() != DialogResult.Cancel) pcb_Preview.Image.Save(sfg_SaveImage.FileName);
        }

        private void btn_SaveSchematic_Click(object sender, EventArgs e)
        {
            sfg_SaveSchem.FileName = "";
            if (sfg_SaveSchem.ShowDialog() != DialogResult.Cancel) _work.GetSchematic().Export(sfg_SaveSchem.FileName);
        }

        internal void Show(bool focus)
        {
            Show();
            Focus();
        }

        public void SetColor(Color color)
        {
            color = Color.FromArgb(255, color);
            Color color2 = color.Divide(1.5);
            Color color3 = color.Divide(3);
            Color color4 = color.Divide(6);

            //Form
            BackColor = color2;
            ForeColor = color2.Text();
            //Buttons
            btn_SaveSchematic.FlatAppearance.BorderColor = color2;
            btn_SaveSchematic.BackColor = color3;
            btn_SaveSchematic.ForeColor = color3.Text();
            //
            btn_SaveImage.FlatAppearance.BorderColor = color2;
            btn_SaveImage.BackColor = color3;
            btn_SaveImage.ForeColor = color3.Text();
            //
            btn_Close.FlatAppearance.BorderColor = color3;
            btn_Close.BackColor = color4;
            btn_Close.ForeColor = color4.Text();
            //
            btn_Maximize.FlatAppearance.BorderColor = color3;
            btn_Maximize.BackColor = color4;
            btn_Maximize.ForeColor = color4.Text();
            //Misc
            pnl_menuBar.BackColor = color3;
            pnl_menuBar.ForeColor = color3.Text();
        }

        #region Menu Bar
        private void Btn_Close_Click(object sender, EventArgs e) => Hide();

        private void Btn_Maximize_Click(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Maximized:
                    WindowState = FormWindowState.Normal;
                    pcb_Preview.Size = new Size(512, 512);
                    break;
                case FormWindowState.Normal:
                    WindowState = FormWindowState.Maximized;
                    pcb_Preview.Size = new Size(pcb_Preview.Size.Height, pcb_Preview.Size.Height);
                    break;
            }
        }

        private int snapX, snapY;
        private bool moving = false;

        private void Pnl_menuBar_MouseDown(object sender, MouseEventArgs e)
        {
            snapX = e.X;
            snapY = e.Y;
            moving = true;
        }

        private void Pnl_menuBar_MouseUp(object sender, EventArgs e) => moving = false;

        private void Pnl_menuBar_MouseMoving(object sender, MouseEventArgs e)
        {
            if (moving && e.Button == MouseButtons.Left)
            {
                int x = snapX - e.X;
                int y = snapY - e.Y;
                Left -= x;
                Top -= y;
                Console.WriteLine("{0}, {1}, {2}, {3}", x, y, Left, Top);
            }
        }
        #endregion
    }
}
