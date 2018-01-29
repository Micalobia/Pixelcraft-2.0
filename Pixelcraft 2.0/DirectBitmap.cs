using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixelcraft_2
{
    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public DirectBitmap(Rectangle rectangle)
        {
            Width = rectangle.Width;
            Height = rectangle.Height;
            Bits = new Int32[Width * Height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public DirectBitmap(Size size)
        {
            Width = size.Width;
            Height = size.Height;
            Bits = new Int32[Width * Height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public DirectBitmap(Bitmap bitmap)
        {
            Width = bitmap.Width;
            Height = bitmap.Height;
            Bits = new Int32[Width * Height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                g.DrawImage(bitmap, 0, 0);
            }
        }

        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }

        public DirectBitmap Cut(Rectangle region)
        {
            if (region.Location.X >= Width || region.Location.Y >= Height) throw new ArgumentOutOfRangeException("Rectangle location is outside of image");
            DirectBitmap dbm = new DirectBitmap(region);
            for(int i = region.X;i < region.X + region.Width; i++)
            {
                for(int j = region.Y; j < region.Y + region.Height; j++)
                {
                    if (i < Width && j < Height)
                    {
                        int index = i * (j * Width);
                        dbm.SetPixel(i - region.X, j - region.Y, GetPixel(i, j));
                    }
                }
            }
            return dbm;
        }

        public void DrawImage(DirectBitmap src, Rectangle destRegion, Rectangle srcRegion, GraphicsUnit srcUnit)
        {

        }
    }
}
