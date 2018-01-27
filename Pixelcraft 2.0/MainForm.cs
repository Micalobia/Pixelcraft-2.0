using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixelcraft_2._0
{
    public partial class MainForm : Form
    {
        private PreviewForm _preview;

        public MainForm()
        {
            InitializeComponent();
            _preview = new PreviewForm();
        }

        private void btn_LoadImage_Click(object sender, EventArgs e)
        {

        }

        private void btn_ConvertImage_Click(object sender, EventArgs e)
        {

        }

        private void btn_LoadTexture_Click(object sender, EventArgs e)
        {

        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            _preview.Show();
        }

        private void chk_Width_CheckedChanged(object sender, EventArgs e)
        {
            nud_Width.Enabled = !chk_Width.Checked;
        }

        private void chk_Height_CheckedChanged(object sender, EventArgs e)
        {
            nud_Height.Enabled = !chk_Height.Checked;
        }
    }
}
