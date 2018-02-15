using Mycan_Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixelcraft_2
{
    delegate void ImageThreadEnd(Canvas bitmap);
    delegate void SetProgressBar(int value);
    delegate void TextureThreadEnd();

    public delegate void TickProgress(object source, UpdateEventArgs e);
    public delegate void ConvertImageEnd(object source, ConvertImageEndArgs e);
    public delegate void LoadTextureEnd(object source, EventArgs e);

    public class UpdateEventArgs : EventArgs
    {
        private double _percent;
        public UpdateEventArgs(double percent) => _percent = percent;
        public double Percent => _percent;
    }

    public class ConvertImageEndArgs : EventArgs
    {
        private Canvas image;
        public ConvertImageEndArgs(Canvas image) => this.image = image;
        public Canvas GetBitmap() => image;
    }

    public class LoadTextureEndArgs : EventArgs
    {
        
    }
}
