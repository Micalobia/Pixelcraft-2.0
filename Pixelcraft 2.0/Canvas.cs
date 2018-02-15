using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Mycan_Graphics
{
    public class Canvas
    {
        private int _width;
        private int _height;
        private Size _size;
        private int[] _pixels;

        /// <summary>The width of the canvas</summary>
        public int Width
        {
            get => _width; private set
            {
                _width = value;
                _size.Width = _width;
            }
        }
        /// <summary>The height of the canvas</summary>
        public int Height
        {
            get => _height; private set
            {
                _height = value;
                _size.Height = _height;
            }
        }
        /// <summary>The size of the canvas</summary>
        public Size Size
        {
            get => _size; private set
            {
                _size = Size;
                _width = Size.Width;
                _height = Size.Height;
            }
        }

        #region Canvas()
        /// <summary>
        /// Creates a blank canvas with the size specified
        /// </summary>
        /// <param name="width">The width of the canvas</param>
        /// <param name="height">The height of the canvas</param>
        public Canvas(int width, int height)
        {
            _size = new Size(width, height);
            _width = width;
            _height = height;
            _pixels = new int[_width * _height];
            _addEvents();
        }
        /// <summary>
        /// Creates a blank canvas with the size specified
        /// </summary>
        /// <param name="size">The size of the canvas</param>
        public Canvas(Size size)
        {
            Size = size;
            _pixels = new int[_height * _width];
            _addEvents();
        }
        public Canvas(Image image)
        {
            _size = image.Size;
            _height = image.Height;
            _width = image.Width;
            _pixels = new int[_height * _width];
            Bitmap bit = new Bitmap(image);
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    SetPixel(i, j, bit.GetPixel(i, j));
                }
            }
        }
        #endregion

        #region SetPixel()
        /// <summary>
        /// Sets a pixel of a certain color at the point specified
        /// </summary>
        /// <param name="point">The point to draw the pixel at</param>
        /// <param name="color">The color to draw the pixel</param>
        public void SetPixel(Point point, int color) => SetPixel(point.X, point.Y, color);
        /// <summary>
        /// Sets a pixel of a certain color at the point specified
        /// </summary>
        /// <param name="point">The point to draw the pixel at</param>
        /// <param name="color">The color to draw the pixel</param>
        public void SetPixel(Point point, Color color) => SetPixel(point.X, point.Y, color);
        /// <summary>
        /// Sets a pixel of a certain color at the point specified<para/>
        /// Make note that only the lowest 8 bits of <paramref name="r"/>,<paramref name="g"/>, and <paramref name="b"/> will be used in calculation
        /// </summary>
        /// <param name="point">The point to draw the pixel at</param>
        /// <param name="r">Red value from 0 to 255</param>
        /// <param name="g">Blue value from 0 to 255</param>
        /// <param name="b">Green value from 0 to 255</param>
        public void SetPixel(Point point, int r, int g, int b) => _pixels[point.X + point.Y * _width] = CreateColor((byte)(0xff & r), (byte)(0xff & g), (byte)(0xff & b));
        /// <summary>
        /// Sets a pixel of a certain color at the point specified
        /// </summary>
        /// <param name="x">The x-coordinate of the point</param>
        /// <param name="y">The y-coordinate of the point</param>
        /// <param name="color">The color of the point</param>
        public void SetPixel(int x, int y, int color) => _pixels[x + y * _width] = color;
        /// <summary>
        /// Sets the pixel of a certain color at the point specified
        /// </summary>
        /// <param name="x">The x-coordinate of the point</param>
        /// <param name="y">The y-coordinate of the point</param>
        /// <param name="color">The color of the point</param>
        public void SetPixel(int x, int y, Color color) => _pixels[x + y * _width] = color.ToArgb();
        /// <summary>
        /// Sets the pixel of a certain color at the point specified
        /// </summary>
        /// <param name="x">The x-coordinate of the point</param>
        /// <param name="y">The y-coordinate of the point</param>
        /// <param name="r">The red value of the point, from 0 to 255</param>
        /// <param name="g">The green value of the point, from 0 to 255</param>
        /// <param name="b">The blue value of the point, from 0 to 255</param>
        public void SetPixel(int x, int y, int r, int g, int b) => _pixels[x + y * _width] = (0xff << 24) | ((r & 0xff) << 16) | ((g & 0xff) << 8) | (b & 0xff);
        /// <summary>
        /// Sets the pixel of a certain color at the point specified
        /// </summary>
        /// <param name="x">The x-coordinate of the point</param>
        /// <param name="y">The y-coordinate of the point</param>
        /// <param name="a">The alpha value of the point, from 0 to 255</param>
        /// <param name="r">The red value of the point, from 0 to 255</param>
        /// <param name="g">The green value of the point, from 0 to 255</param>
        /// <param name="b">The blue value of the point, from 0 to 255</param>
        public void SetPixel(int x, int y, int a, int r, int g, int b) => _pixels[x + y * _width] = CreateColor((byte)(a & 0xff), (byte)(r & 0xff), (byte)(g & 0xff), (byte)(b & 0xff));
        #endregion
        #region GetPixel()
        public int GetPixel(Point point) => _pixels[point.X + point.Y * _width];
        public int GetPixel(int x, int y) => _pixels[x + y * _width];
        #endregion

        public int AverageColor(bool includeAlpha = false)
        {
            byte[] colorByte = new byte[4];
            long a = 0, r = 0, g = 0, b = 0;
            if (!includeAlpha) a = 255;
            for (int i = 0; i < _pixels.Length; i++)
            {
                Color color = Color.FromArgb(_pixels[i]);
                if (includeAlpha) a += color.A;
                r += color.R;
                g += color.G;
                b += color.B;
            }
            if (includeAlpha) a /= _pixels.LongLength;
            r /= _pixels.LongLength;
            g /= _pixels.LongLength;
            b /= _pixels.LongLength;
            return CreateColor(a, r, g, b);
        }

        #region CreateColor()
        public static int CreateColor(byte r, byte g, byte b) => 0xff << 0x18 | r << 0x10 | g << 0x8 | b;
        public static int CreateColor(byte a, byte r, byte g, byte b) => a << 0x18 | r << 0x10 | g << 0x8 | b;
        public static int CreateColor(int r, int g, int b) => 0xff << 0x18 | (byte)(r & 0xff) << 0x10 | (byte)(g & 0xff) << 0x8 | (byte)(b & 0xff);
        public static int CreateColor(int a, int r, int g, int b) => (byte)(a & 0xff) << 0x18 | (byte)(r & 0xff) << 0x10 | (byte)(g & 0xff) << 0x8 | (byte)(b & 0xff);
        public static int CreateColor(long r, long g, long b) => 0xff << 0x18 | (byte)(r & 0xff) << 0x10 | (byte)(g & 0xff) << 0x8 | (byte)(b & 0xff);
        public static int CreateColor(long a, long r, long g, long b) => (byte)(a & 0xff) << 0x18 | (byte)(r & 0xff) << 0x10 | (byte)(g & 0xff) << 0x8 | (byte)(b & 0xff);
        #endregion
        #region Clear()
        public void Clear(bool transparent = false)
        {
            int color = transparent ? 0 : 0xff << 0x18;
            for (long i = 0; i < _pixels.LongLength; i++)
            {
                _pixels[i] = color;
            }
        }
        public void Clear(int color)
        {
            for (long i = 0; i < _pixels.LongLength; i++)
            {
                _pixels[i] = color;
            }
        }
        public void Clear(Color color) => Clear(color.ToArgb());
        #endregion
        #region Cut()
        public Canvas Cut(Rectangle region) => Cut(region.Left, region.Top, region.Right, region.Bottom);
        public Canvas Cut(Point topleft, Point bottomright) => Cut(topleft.X, topleft.Y, bottomright.X, bottomright.Y);
        public Canvas Cut(int x, int y, int u, int v)
        {
            Canvas c = new Canvas(u - x, v - y);
            for (int i = x; i < u && i < _width; ++i)
            {
                for (int j = y; j < v && j < _height; ++j)
                {
                    c.SetPixel(i - x, j - y, GetPixel(i, j));
                }
            }
            return c;
        }
        #endregion
        #region DrawImage()
        public void DrawImage(Point topleft, Image image) => DrawImage(topleft.X, topleft.Y, new Canvas(image));
        public void DrawImage(int x, int y, Image image) => DrawImage(x, y, new Canvas(image));
        public void DrawImage(Point topleft, Canvas canv) => DrawImage(topleft.X, topleft.Y, canv);
        public void DrawImage(int x, int y, Canvas canv)
        {
            for (int i = x; i < x + canv.Width; ++i)
            {
                if (i >= _width) break;
                for (int j = y; j < y + canv.Height; ++j)
                {
                    if (j >= _height) break;
                    SetPixel(i, j, canv.GetPixel(i - x, j - y));
                }
            }
        }
        #endregion
        #region FillRectangle
        public void FillRectangle(Point topLeft, Point bottomRight, int color) => FillRectangle(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, color);
        public void FillRectangle(Point topLeft, Point bottomRight, Color color) => FillRectangle(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, color.ToArgb());
        public void FillRectangle(Rectangle rectangle, Color color) => FillRectangle(rectangle.Top, rectangle.Left, rectangle.Bottom, rectangle.Right, color.ToArgb());
        public void FillRectangle(Rectangle rectangle, int color) => FillRectangle(rectangle.Top, rectangle.Left, rectangle.Bottom, rectangle.Right, color);
        public void FillRectangle(Point topLeft, Size size, Color color) => FillRectangle(topLeft.X, topLeft.Y, topLeft.X + size.Width, topLeft.Y + size.Height, color.ToArgb());
        public void FillRectangle(Point topLeft, Size size, int color) => FillRectangle(topLeft.X, topLeft.Y, topLeft.X + size.Width, topLeft.Y + size.Height, color);
        public void FillRectangle(int top, int left, int bottom, int right, Color color) => FillRectangle(top, left, bottom, right, color.ToArgb());
        public void FillRectangle(int top, int left, int bottom, int right, int color)
        {
            //Check if the rectangle is valid, might force a swap for this at some point
            if (top > bottom) throw new ArgumentOutOfRangeException(nameof(bottom), string.Format("{0} must be greater than {1}", nameof(bottom), nameof(top)));
            if (left > right) throw new ArgumentOutOfRangeException(nameof(right), string.Format("{0} must be greater than {1}", nameof(right), nameof(left)));
            //Check bounds of the canvas
            if (top < 0 || top > _height) throw new ArgumentOutOfRangeException(nameof(top), string.Format("{0} must be inside the canvas", nameof(top)));
            if (bottom < 0 || bottom > _height) throw new ArgumentOutOfRangeException(nameof(bottom), string.Format("{0} must be inside the canvas", nameof(bottom)));
            if (left < 0 || left > _width) throw new ArgumentOutOfRangeException(nameof(left), string.Format("{0} must be inside the canvas", nameof(left)));
            if (right < 0 || right > _width) throw new ArgumentOutOfRangeException(nameof(right), string.Format("{0} must be inside the canvas", nameof(right)));

            for (int i = top; i < bottom; i++)
            {
                for (int j = left; j < right; j++)
                {
                    _pixels[i + j * _width] = color;
                }
            }
        }
        #endregion
        #region DrawRectangle()
        public void DrawRectangle(Point topLeft, Point bottomRight, int color) => DrawRectangle(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, color);
        public void DrawRectangle(Point topLeft, Point bottomRight, Color color) => DrawRectangle(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y, color.ToArgb());
        public void DrawRectangle(Rectangle rectangle, Color color) => DrawRectangle(rectangle.Top, rectangle.Left, rectangle.Bottom, rectangle.Right, color.ToArgb());
        public void DrawRectangle(Rectangle rectangle, int color) => DrawRectangle(rectangle.Top, rectangle.Left, rectangle.Bottom, rectangle.Right, color);
        public void DrawRectangle(Point topLeft, Size size, Color color) => DrawRectangle(topLeft.X, topLeft.Y, topLeft.X + size.Width, topLeft.Y + size.Height, color.ToArgb());
        public void DrawRectangle(Point topLeft, Size size, int color) => DrawRectangle(topLeft.X, topLeft.Y, topLeft.X + size.Width, topLeft.Y + size.Height, color);
        public void DrawRectangle(int top, int left, int bottom, int right, Color color) => DrawRectangle(top, left, bottom, right, color.ToArgb());
        public void DrawRectangle(int top, int left, int bottom, int right, int color)
        {
            //Check if the rectangle is valid, might force a swap for this at some point
            if (top > bottom) throw new ArgumentOutOfRangeException(nameof(bottom), string.Format("{0} must be greater than {1}", nameof(bottom), nameof(top)));
            if (left > right) throw new ArgumentOutOfRangeException(nameof(right), string.Format("{0} must be greater than {1}", nameof(right), nameof(left)));
            //Check bounds of the canvas
            if (top < 0 || top > _height) throw new ArgumentOutOfRangeException(nameof(top), string.Format("{0} must be inside the canvas", nameof(top)));
            if (bottom < 0 || bottom > _height) throw new ArgumentOutOfRangeException(nameof(bottom), string.Format("{0} must be inside the canvas", nameof(bottom)));
            if (left < 0 || left > _width) throw new ArgumentOutOfRangeException(nameof(left), string.Format("{0} must be inside the canvas", nameof(left)));
            if (right < 0 || right > _width) throw new ArgumentOutOfRangeException(nameof(right), string.Format("{0} must be inside the canvas", nameof(right)));

            for (int i = left; i < right; i++)
            {
                _pixels[i + top * _width] = color;
                _pixels[i + bottom * _width] = color;
            }
            for (int i = top; i < bottom; i++)
            {
                _pixels[left + i * _width] = color;
                _pixels[right + i * _width] = color;
            }
        }
        #endregion
        #region DrawLine()
        public void DrawLine(Point p1, Point p2, Color color) => DrawLine(p1.X, p1.Y, p2.X, p2.Y, color.ToArgb());
        public void DrawLine(Point p1, Point p2, int color) => DrawLine(p1.X, p1.Y, p2.X, p2.Y, color);
        public void DrawLine(int x, int y, int u, int v, Color color) => DrawLine(x, y, u, v, color.ToArgb());
        public void DrawLine(int x, int y, int u, int v, int color)
        {
            int w = u - x;
            int h = v - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                _pixels[x + y * _width] = color;
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }
        #endregion
        #region DrawCircle()
        public void DrawCircle(Point point, int r, int color) => DrawCircle(point.X, point.Y, r, color);
        public void DrawCircle(Point point, int r, Color color) => DrawCircle(point.X, point.Y, r, color.ToArgb());
        public void DrawCircle(int x, int y, int r, Color color) => DrawCircle(x, y, r, color.ToArgb());
        public void DrawCircle(int x, int y, int r, int color)
        {
            int d = (5 - r * 4) / 4;
            int _x = 0;
            int _y = r;

            do
            {
                // ensure index is in range before setting (depends on your image implementation)
                // in this case we check if the pixel location is within the bounds of the image before setting the pixel
                if (x + _x >= 0 && x + _x <= _width - 1 && y + _y >= 0 && y + _y <= _height - 1) _pixels[x + _x + (y + _y) * _width] = color;
                if (x + _x >= 0 && x + _x <= _width - 1 && y - _y >= 0 && y - _y <= _height - 1) _pixels[x + _x + (y - _y) * _width] = color;
                if (x - _x >= 0 && x - _x <= _width - 1 && y + _y >= 0 && y + _y <= _height - 1) _pixels[x - _x + (y + _y) * _width] = color;
                if (x - _x >= 0 && x - _x <= _width - 1 && y - _y >= 0 && y - _y <= _height - 1) _pixels[x - _x + (y - _y) * _width] = color;
                if (x + _y >= 0 && x + _y <= _width - 1 && y + _x >= 0 && y + _x <= _height - 1) _pixels[x + _y + (y + _x) * _width] = color;
                if (x + _y >= 0 && x + _y <= _width - 1 && y - _x >= 0 && y - _x <= _height - 1) _pixels[x + _y + (y - _x) * _width] = color;
                if (x - _y >= 0 && x - _y <= _width - 1 && y + _x >= 0 && y + _x <= _height - 1) _pixels[x - _y + (y + _x) * _width] = color;
                if (x - _y >= 0 && x - _y <= _width - 1 && y - _x >= 0 && y - _x <= _height - 1) _pixels[x - _y + (y - _x) * _width] = color;
                if (d < 0)
                {
                    d += 2 * _x + 1;
                }
                else
                {
                    d += 2 * (_x - _y) + 1;
                    _y--;
                }
                _x++;
            } while (_x <= _y);
        }
        #endregion
        #region FillCircle()
        public void FillCircle(Point point, int r, int color) => FillCircle(point.X, point.Y, r, color);
        public void FillCircle(Point point, int r, Color color) => FillCircle(point.X, point.Y, r, color.ToArgb());
        public void FillCircle(int x, int y, int r, Color color) => FillCircle(x, y, r, color.ToArgb());
        public void FillCircle(int x, int y, int r, int color)
        {
            int _x = r - 1;
            int _y = 0;
            int dx = 1;
            int dy = 1;
            int err = dx - (r << 1);

            while (_x >= _y)
            {
                DrawLine(x + _x, y + _y, x - _x, y + _y, color); //03
                DrawLine(x + _y, y + _x, x - _y, y + _x, color); //12
                DrawLine(x - _x, y - _y, x + _x, y - _y, color); //47
                DrawLine(x - _y, y - _x, x + _y, y - _x, color); //56

                if (err <= 0)
                {
                    _y++;
                    err += dy;
                    dy += 2;
                }
                else
                {
                    x--;
                    dx += 2;
                    err += dx - (r << 1);
                }
            }
        }
        #endregion
        #region FloodFill()
        public void FloodFill(Point point, Color color) => FloodFill(point.X, point.Y, color.ToArgb());
        public void FloodFill(Point point, int color) => FloodFill(point.X, point.Y, color);
        public void FloodFill(int x, int y, Color color) => FloodFill(x, y, color.ToArgb());
        public void FloodFill(int x, int y, int color)
        {
            Stack<Point> pixels = new Stack<Point>();
            int targetColor = _pixels[x + y * _width];
            pixels.Push(new Point(x, y));

            while (pixels.Count > 0)
            {
                Point a = pixels.Pop();
                if (a.X < _width && a.X > 0 &&
                        a.Y < _height && a.Y > 0)//make sure we stay within bounds
                {

                    if (_pixels[a.X + a.Y * _width] == targetColor)
                    {
                        _pixels[a.X + a.Y * _height] = color;
                        pixels.Push(new Point(a.X - 1, a.Y));
                        pixels.Push(new Point(a.X + 1, a.Y));
                        pixels.Push(new Point(a.X, a.Y - 1));
                        pixels.Push(new Point(a.X, a.Y + 1));
                    }
                }
            }
            return;
        }
        #endregion

        public Image GetImage()
        {
            byte[] result = new byte[_pixels.Length * sizeof(int)];
            int[] bits = new int[_pixels.LongLength];
            GCHandle handle = GCHandle.Alloc(bits, GCHandleType.Pinned);
            Bitmap image = new Bitmap(_width, _height, _width * 4, PixelFormat.Format32bppPArgb, handle.AddrOfPinnedObject());
            for (long i = 0; i < _pixels.LongLength; i++)
            {
                bits[i] = _pixels[i];
            }
            handle.Free();
            return image;
        }

        private void _addEvents()
        {

        }

        #region Object overrides
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => base.ToString();
        #endregion
    }
}
