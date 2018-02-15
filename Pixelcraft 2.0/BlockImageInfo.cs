using Mycan_Graphics;
using System;
using System.Drawing;

namespace Pixelcraft_2
{
    internal struct BlockImageInfo : IDisposable
    {
        public Canvas Image { get; set; }
        public Color Color { get; set; }
        public string Name { get; set; }
        public int ColorCode { get; set; }

        public BlockImageInfo(Bitmap image, string name, Color color)
        {
            Image = new Canvas(image);
            Name = name;
            Color = color;
            ColorCode = color.ToArgb();
        }

        public BlockImageInfo(Bitmap image, string name, int colorCode)
        {
            Image = new Canvas(image);
            Name = name;
            ColorCode = colorCode;
            Color = Color.FromArgb(colorCode);
        }

        public BlockImageInfo(Canvas image, string name, Color color)
        {
            Image = image;
            Name = name;
            Color = color;
            ColorCode = color.ToArgb();
        }

        public BlockImageInfo(Canvas image, string name, int color)
        {
            Image = image;
            Name = name;
            Color = Color.FromArgb(color);
            ColorCode = color;
        }

        public BlockImageInfo(Canvas image, string name)
        {
            Image = image;
            Name = name;
            ColorCode = image.AverageColor();
            Color = Color.FromArgb(ColorCode);
        }

        public void Dispose()
        {
            Image = null;
            Name = null;
            Image = null;
            ColorCode = 0;
            Color = Color.Black;
        }
    }
}