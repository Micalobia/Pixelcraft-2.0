using Mycan_Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Pixelcraft_2
{
    internal partial class Work : IDisposable
    {

        private static Work _work;
        private CheckPanel _checkPanel = new CheckPanel();
        #pragma warning disable IDE1006 // Naming Styles
        private const string TEXTURE_EXTRACT = @"D:\Pixelcraft\TextureExtract\";
        #pragma warning restore IDE1006 // Naming Styles

        private NbtSchematic _schematic;

        public NbtSchematic GetSchematic() => _schematic;

        private string BlockPath
        {
            get
            {
                string s = "";
                s += TEXTURE_EXTRACT;
                s += _currentTexture;
                s += "\\";
                s += @"assets\minecraft\textures\blocks\";

                return s;
            }
        }

        private string _currentTexture = @"1.12.2";
        Canvas[] _blockPics;
        BlockDataCollection _data;
        Color[] _colors;
        public string blocksNeeded = string.Empty;
        int[] _colorCodes;
        private int _blockSize = 16;

        private Work()
        {
            Console.WriteLine("Getting blockdata");
            _data = BlockDataCreate;
            Console.WriteLine("Loading texture");
            LoadTexture();
            Console.WriteLine("Creating thread object");
            _checkPanel.Set(_data);
        }


        public static Work GetWork() => _work = _work ?? new Work();

        /// <summary>
        /// Loads images from a directory that are in the blocklist
        /// </summary>
        /// <param name="directory">The directory you are loading images from</param>
        /// <returns>An array of images loaded from the directory chosen</returns>
        private Bitmap[] LoadImages(string directory)
        {
            var all = _data.All;
            Bitmap[] images = new Bitmap[all.Count];
            for (int i = 0; i < all.Count; i++)
            {
                Bitmap bm = Image.FromFile(directory + all[i].fileName) as Bitmap;
                images[i] = new Bitmap(bm);
            }
            return images;
        }

        public void LoadTexture()
        {
            _currentTexture = @"1.12.2";
            string directory = string.Format("{0}{1}\\", TEXTURE_EXTRACT, _currentTexture);
            var all = _data.All;
            _blockPics = new Canvas[all.Count];
            _colorCodes = new int[all.Count];
            _colors = new Color[all.Count];
            try
            {
                for (int i = 0; i < all.Count; i++)
                {
                    Image image = Image.FromFile(BlockPath + all[i].fileName);
                    _blockPics[i] = new Canvas(image);
                    image.Dispose();
                    _colorCodes[i] = _blockPics[i].AverageColor();
                    _colors[i] = Color.FromArgb(_colorCodes[i]);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Couldn't find images; Either invalid or incomplete texture pack. Loading defualt...");
                LoadTexture(@"1.12.2");
            }
        }

        /// <summary>
        /// Loads a texture from a directory chosen
        /// </summary>
        /// <param name="directory">The directory of the zip file to load</param>
        public void LoadTexture(string directory)
        {
            string name = directory.Split('\\').Last();
            name = name.Substring(0, name.Length - 4);
            _currentTexture = name;
            if (_blockPics != null)
            {
                for (int i = 0; i < _blockPics.Length; i++)
                {
                    _blockPics[i] = null;
                }
            }
            Console.WriteLine("Deleting old stuff...");
            while (Directory.Exists(TEXTURE_EXTRACT + name + "\\"))
            {
                if (Directory.Exists(TEXTURE_EXTRACT + name + "\\"))
                    Directory.Delete(TEXTURE_EXTRACT + name + "\\", true);
            }
            Console.WriteLine("Finished deleting");
            Console.WriteLine("Extraction started...");
            try
            {
                ZipFile.ExtractToDirectory(directory, TEXTURE_EXTRACT + name + "\\");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Zip file not found");
                Console.WriteLine("Reverting to defualt...");
                LoadTexture();
                return;
            }
            Console.WriteLine("Extraction ended");
            var all = _data.All;
            _colorCodes = new int[all.Count];
            _colors = new Color[all.Count];
            _blockPics = new Canvas[all.Count];
            try
            {
                for (int i = 0; i < all.Count; i++)
                {
                    _blockPics[i] = new Canvas(Image.FromFile(BlockPath + all[i].fileName));
                    _colors[i] = Color.FromArgb(_colorCodes[i]);
                    _colorCodes[i] = _blockPics[i].AverageColor();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Couldn't find images; Either invalid or incomplete texture pack. Loading defualt...");
                LoadTexture();
            }
            TextureThreadEnd.Invoke(this, new EventArgs());
        }

        /// <summary>       
        /// Cuts a section of a Bitmap out and returns it as it's own bitmap
        /// </summary>
        /// <param name="original">The original image to cut from</param>
        /// <param name="region">The region to cut</param>
        /// <returns>The cut region</returns>
        //private Bitmap Cut(Bitmap original, Rectangle region)
        //{
        //    Bitmap clone = new Bitmap(original);
        //    Bitmap cutImage = new Bitmap(region.Width, region.Height);
        //    Rectangle paste = new Rectangle(0, 0, region.Width, region.Height);
        //    using (Graphics g = Graphics.FromImage(cutImage))
        //    {
        //        g.DrawImage(clone, paste, region, GraphicsUnit.Pixel);
        //    }
        //    clone.Dispose();
        //    return cutImage;
        //}

        //public Color AverageColor(Bitmap source)
        //{
        //    int width = source.Width;
        //    int height = source.Height;
        //    long[] totals = new long[] { 0, 0, 0, 0 };
        //    DirectBitmap dbm = new DirectBitmap(source);
        //    int[] array = new int[dbm.Bits.Length];
        //    dbm.Bits.CopyTo(array, 0);
        //    dbm.Dispose();
        //    long a = 0, r = 0, g = 0, b = 0;
        //    int index = 0;
        //    long pixels = 0;
        //    for (int i = 0; i < width; i++)
        //    {
        //        for (int j = 0; j < height; j++)
        //        {
        //            pixels++;
        //            index = i + width * j;
        //            a += (array[index] & 0xff000000) >> 24;
        //            r += (array[index] & 0x00ff0000) >> 16;
        //            g += (array[index] & 0x0000ff00) >> 8;
        //            b += (array[index] & 0x000000ff);
        //        }
        //    }
        //    array = null;
        //    return Color.FromArgb((int)(a / pixels), (int)(r / pixels), (int)(g / pixels), (int)(b / pixels));
        //}
        /*
        #region test
        /// <summary>
        /// Ignore this, this is me messing with filters. Pretty fun tbh
        /// </summary>
        /// <param name="source"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>

        public Bitmap Test(Bitmap source, int threshold = 32)
        {
            int width = source.Width;
            int height = source.Height;
            long[] totals = new long[] { 0, 0, 0, 0 };
            long pixels = 0;
            DirectBitmap dSource = new DirectBitmap(source);
            DirectBitmap dReturn = new DirectBitmap(source.Size);
            for (int i = 0; i < width; i += 1)
            {
                for (int j = 0; j < height; j += 1)
                {
                    Color color = dSource.GetPixel(i, j);
                    dReturn.SetPixel(i, j, color.Invert().Shift(ColorShift.BRG));
                    pixels++;
                }
                double val = ((double)pixels / width / height) * 1000;
                val = val > 1000 ? 1000 : val;
                Console.WriteLine(val);
                Tick.Invoke(this, new UpdateEventArgs(val));
            }
            dSource.Dispose();
            Bitmap bit = dReturn.Bitmap.Clone() as Bitmap;
            dReturn.Dispose();
            return bit;
        }

        public Bitmap Test2(Bitmap source, int threshold = 32)
        {
            int width = source.Width;
            int height = source.Height;
            long[] totals = new long[] { 0, 0, 0, 0 };
            long pixels = 0;
            DirectBitmap dSource = new DirectBitmap(source);
            DirectBitmap dReturn = new DirectBitmap(source.Size);
            for (int i = 0; i < width; i += 1)
            {
                for (int j = 0; j < height; j += 1)
                {
                    dReturn.SetPixel(i, j, dSource.GetPixel(i, j).Invert());
                    //Console.WriteLine("{0}, {1}, {2}, {3}", sa, sr, sg, sb);
                    //int average = (int)((sr + sg + sb) / 3);
                    //dReturn.SetPixel(i, j, Color.FromArgb(255, (int)average < threshold ? 0 : (int)sr * 2, (int)average < threshold ? 0 : (int)sg * 2, (int)average < threshold ? 0 : (int)sb * 2));
                    pixels++;
                }
                Console.WriteLine((double)pixels / width / height);
            }
            dSource.Dispose();
            Bitmap bit = dReturn.Bitmap.Clone() as Bitmap;
            dReturn.Dispose();
            return bit;
        }

        public Bitmap Test3(Bitmap source, int threshold = 32, int size = 7)
        {
            int width = source.Width;
            int height = source.Height;
            int cube = size * 2 + 1;
            long[] totals = new long[] { 0, 0, 0, 0 };
            long pixels = 0;
            DirectBitmap dSource = new DirectBitmap(source);
            DirectBitmap dReturn = new DirectBitmap(source.Size);
            for (int i = size; i < width - size; i += 1)
            {
                for (int j = size; j < height - size; j += 1)
                {
                    totals = new long[] { 0, 0, 0, 0 };
                    Color[] colors = new Color[cube * cube];
                    for (int k = 0; k < cube; k++)
                    {
                        for (int l = 0; l < cube; l++)
                        {
                            colors[k * cube + l] = dSource.GetPixel(i - (k - size), j - (l - size));
                        }
                    }
                    for (int k = 0; k < colors.Length; k++)
                    {
                        totals[0] += colors[k].A;
                        totals[1] += colors[k].R;
                        totals[2] += colors[k].G;
                        totals[3] += colors[k].B;
                    }
                    List<int> a = new List<int>();
                    List<int> r = new List<int>();
                    List<int> g = new List<int>();
                    List<int> b = new List<int>();
                    foreach (Color c in colors)
                    {
                        a.Add(c.A);
                        r.Add(c.R);
                        g.Add(c.G);
                        b.Add(c.B);
                    }
                    double sa = a.StandardDeviation();
                    double sr = r.StandardDeviation();
                    double sg = g.StandardDeviation();
                    double sb = b.StandardDeviation();
                    //Console.WriteLine("{0}, {1}, {2}, {3}", sa, sr, sg, sb);
                    int average = (int)((sr + sg + sb) / 3);
                    dReturn.SetPixel(i, j, Color.FromArgb(average, average, average));
                    pixels++;
                }
                double val = ((double)pixels / width / height) * 1000;
                val = val > 1000 ? 1000 : val;
                Console.WriteLine(val);
                Tick.Invoke(this, new UpdateEventArgs(val));
            }
            dSource.Dispose();
            Bitmap bit = dReturn.Bitmap.Clone() as Bitmap;
            dReturn.Dispose();
            return bit;
        }

        #endregion
        */
        /// <summary>
        /// Copys the region of one image onto a region of another
        /// </summary>
        /// <param name="srcBitmap">The bitmap to copy from</param>
        /// <param name="srcRegion">The region to copy from</param>
        /// <param name="destBitmap">The bitmap to copy to</param>
        /// <param name="destRegion">The region to copy to</param>
        private void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
        {
            using (Graphics grD = Graphics.FromImage(destBitmap))
            {
                grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
        }

        /// <summary>
        /// Finds the block with the nearest color code to the one provided
        /// </summary>
        /// <param name="color">The color code you want to get a block for</param>
        /// <returns>The block that is closest to the color code</returns>
        private BlockImageInfo FindBlock(Color color, BlockData[] all)
        {
            int index = -1;
            double[] error = new double[_colorCodes.Length];
            for (int i = 0; i < _colorCodes.Length; i++)
            {
                error[i] = 0;
                error[i] = Error(color, _colors[i]);
                //error[i] += Error(color.R, colors[i].R);
                //error[i] += Error(color.G, colors[i].G);
                //error[i] += Error(color.B, colors[i].B);
            }
            Array.IndexOf(error, error.Min());
            for (int i = 0; i < error.Length; i++)
            {
                if (error.Min() == error[i])
                {
                    index = i;
                }
                if (index >= 0) break;
            }
            if (index == -1) index = 0;
            BlockImageInfo block = new BlockImageInfo
            {
                Name = all[index].name,
                Color = _colors[index],
                Image = _blockPics[index],
                ColorCode = _colorCodes[index]
            };
            return block;
        }

        public BlockDataCollection BlockDataCreate
        {
            get
            {
                BlockDataCollection collection = new BlockDataCollection();

                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.WHITE, "White Concrete", "concrete_white.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.ORANGE, "Orange Concrete", "concrete_orange.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.MAGENTA, "Magenta Concrete", "concrete_magenta.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.LIGHT_BLUE, "Light Blue Concrete", "concrete_light_blue.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.YELLOW, "Yellow Concrete", "concrete_yellow.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.LIGHT_GREEN, "Lime Concrete", "concrete_lime.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.PINK, "Pink Concrete", "concrete_pink.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.GRAY, "Gray Concrete", "concrete_gray.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.LIGHT_GRAY, "Light Gray Concrete", "concrete_silver.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.CYAN, "Cyan Concrete", "concrete_cyan.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.PURPLE, "Purple Concrete", "concrete_purple.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.BLUE, "Blue Concrete", "concrete_blue.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.BROWN, "Brown Concrete", "concrete_brown.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.DARK_GREEN, "Green Concrete", "concrete_green.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.RED, "Red Concrete", "concrete_red.png"), "Concrete");
                collection.Add(new BlockData(BlockType.CONCRETE, ConcreteColor.BLACK, "Black Concrete", "concrete_black.png"), "Concrete");

                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.WHITE, "White Concrete Powder", "concrete_powder_white.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.ORANGE, "Orange Concrete Powder", "concrete_powder_orange.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.MAGENTA, "Magenta Concrete Powder", "concrete_powder_magenta.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.LIGHT_BLUE, "Light Blue Concrete Powder", "concrete_powder_light_blue.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.YELLOW, "Yellow Concrete Powder", "concrete_powder_yellow.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.LIGHT_GREEN, "Lime Concrete Powder", "concrete_powder_lime.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.PINK, "Pink Concrete Powder", "concrete_powder_pink.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.GRAY, "Gray Concrete Powder", "concrete_powder_gray.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.LIGHT_GRAY, "Light Gray Concrete Powder", "concrete_powder_silver.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.CYAN, "Cyan Concrete Powder", "concrete_powder_cyan.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.PURPLE, "Purple Concrete Powder", "concrete_powder_purple.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.BLUE, "Blue Concrete Powder", "concrete_powder_blue.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.BROWN, "Brown Concrete Powder", "concrete_powder_brown.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.DARK_GREEN, "Green Concrete Powder", "concrete_powder_green.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.RED, "Red Concrete Powder", "concrete_powder_red.png"), "Concrete Powder");
                collection.Add(new BlockData(BlockType.CONCRETE_POWDER, ConcreteColor.BLACK, "Black Concrete Powder", "concrete_powder_black.png"), "Concrete Powder");

                collection.Add(new BlockData(BlockType.PLANKS, PlankType.OAK, "Oak Planks", "planks_oak.png"), "Planks");
                collection.Add(new BlockData(BlockType.PLANKS, PlankType.SPRUCE, "Spruce Planks", "planks_spruce.png"), "Planks");
                collection.Add(new BlockData(BlockType.PLANKS, PlankType.BIRCH, "Birch Planks", "planks_birch.png"), "Planks");
                collection.Add(new BlockData(BlockType.PLANKS, PlankType.JUNGLE, "Jungle Planks", "planks_jungle.png"), "Planks");
                collection.Add(new BlockData(BlockType.PLANKS, PlankType.DARK_OAK, "Dark Oak Planks", "planks_big_oak.png"), "Planks");
                collection.Add(new BlockData(BlockType.PLANKS, PlankType.ACACIA, "Acacia Planks", "planks_acacia.png"), "Planks");

                collection.Add(new BlockData(BlockType.STONE, StoneType.STONE, "Stone", "stone.png"), "Stones");
                collection.Add(new BlockData(BlockType.STONE, StoneType.ANDESITE, "Andesite", "stone_andesite.png"), "Stones");
                collection.Add(new BlockData(BlockType.STONE, StoneType.DIORITE, "Diorite", "stone_diorite.png"), "Stones");
                collection.Add(new BlockData(BlockType.STONE, StoneType.GRANITE, "Granite", "stone_granite.png"), "Stones");

                collection.Add(new BlockData(BlockType.STONE_BRICK, StoneBrickType.STONE_BRICK, "Stone Brick", "stonebrick.png"));
                collection.Add(new BlockData(BlockType.PACKED_ICE, 0, "Packed Ice", "ice_packed.png"));
                collection.Add(new BlockData(BlockType.DIRT, DirtType.DIRT, "Dirt", "dirt.png"));
                collection.Add(new BlockData(BlockType.CLAY, 0, "Clay", "clay.png"));
                collection.Add(new BlockData(BlockType.SANDSTONE, SandstoneType.SMOOTH_SANDSTONE, "Smooth Sandstone", "sandstone_smooth.png"));
                collection.Add(new BlockData(BlockType.RED_SANDSTONE, SandstoneType.SMOOTH_SANDSTONE, "Smooth Red Sandstone", "red_sandstone_smooth.png"));

                collection.Add(new BlockData(BlockType.WOOL, WoolColor.BLACK, "Black Wool", "wool_colored_red.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.RED, "Red Wool", "wool_colored_red.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.WHITE, "White Wool", "wool_colored_white.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.ORANGE, "Orange Wool", "wool_colored_orange.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.MAGENTA, "Magenta Wool", "wool_colored_magenta.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.LIGHT_BLUE, "Light Blue Wool", "wool_colored_light_blue.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.YELLOW, "Yellow Wool", "wool_colored_yellow.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.LIGHT_GREEN, "Lime Wool", "wool_colored_lime.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.PINK, "Pink Wool", "wool_colored_pink.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.GRAY, "Gray Wool", "wool_colored_gray.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.LIGHT_GRAY, "Light Gray Wool", "wool_colored_silver.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.CYAN, "Cyan Wool", "wool_colored_cyan.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.PURPLE, "Purple Wool", "wool_colored_purple.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.BLUE, "Blue Wool", "wool_colored_blue.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.BROWN, "Brown Wool", "wool_colored_brown.png"), "Wool");
                collection.Add(new BlockData(BlockType.WOOL, WoolColor.DARK_GREEN, "Green Wool", "wool_colored_white.png"), "Wool");


                return collection;
            }
        }

        public CheckPanel CheckPanel { get => _checkPanel; set => _checkPanel = value; }

        /// <summary>
        /// Gets the percent error between two values
        /// </summary>
        /// <param name="goal">The number you are going for</param>
        /// <param name="guess">The number you are guessing</param>
        /// <returns>The percent error between your goal and your guess</returns>
        private double Error(double goal, double guess)
        {
            try { return Math.Abs((goal - guess) / goal); }
            catch { }
            try { return Math.Abs((goal - guess) / guess); }
            catch { }
            return 0;
        }

        private double Error(Color goal, Color guess)
        {
            double d =
                Math.Sqrt(
                    (goal.R - guess.R).Q() +
                    (goal.G - guess.G).Q() +
                    Math.Pow(goal.B - guess.B, 2));
            return d;
        }

        /// <summary>
        /// Grabs an image and creates a schematic and an image out of it
        /// </summary>
        /// <param name="bitmap">The images to create things out of</param>
        /// <param name="width">The width in blocks of the results (-1 to ignore)</param>
        /// <param name="height">The height in blocks of the results (-1 to ignore)</param>
        /// <param name="maxImage">Determines if you want the image to be the highest quality managable</param>
        /// <returns>The image converted to minecraft blocks</returns>
        /// <exception cref="ArgumentOutOfRangeException">Width or Height cannot be zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The image cannot be too large</exception>
        public Canvas ConvertImage(Bitmap bitmap, int width, int height, bool maxImage)
        {
            if (width == 0 || height == 0)
            {
                throw new ArgumentOutOfRangeException("", "Width or Height cannot be zero");
            }

            int ppb = 4;
            if (height < 0)
            {
                if (width < 0)
                {
                    ppb = 1;
                }
                else
                {
                    ppb = bitmap.Width / width;
                }
            }
            else
            {
                ppb = bitmap.Height / height;
            }
            return ConvertImage(bitmap, ppb, maxImage);
        }

        public Canvas Convert(Bitmap image, int width, int height)
        {
            if (width == 0 || height == 0) throw new ArgumentOutOfRangeException("", "Width or Height cannot be zero");
            int blockSize = 0;
            if (height < 0)
                if (width < 0)
                    blockSize = 1;
                else
                    blockSize = image.Width / width;
            else
                blockSize = image.Height / height;
            return Convert(image, blockSize);
        }

        public Canvas Convert(Bitmap image, int pixelBlockSize)
        {
            //Creates a Canvas out of the image given
            Canvas canv = new Canvas(image);
            image.Dispose();
            //Check to see if the texture pack has all the same square size
            int width = _blockPics[0].Width;
            int height = _blockPics[0].Height;
            foreach (Canvas c in _blockPics)
            {
                if (c.Width != width) throw new ArgumentOutOfRangeException("Inconsitant width");
                if (c.Height != height) throw new ArgumentOutOfRangeException("Inconsitant height");
            }
            _blockSize = width == height ? width : throw new ArgumentOutOfRangeException("Texture pack does not contain square images");
            //Create image to draw on
            Canvas blockImage = new Canvas(canv.Width * _blockSize / pixelBlockSize, canv.Height * _blockSize / pixelBlockSize);
            //
            BlockData[] enabled = _data.Enabled.ToArray();
            _schematic = new NbtSchematic((short)((canv.Width / pixelBlockSize + 1) % 65536), 1, (short)((canv.Height / pixelBlockSize + 1) % 65536));
            long progress = 0;
            for (int i = 0; i < canv.Width; i += pixelBlockSize)
            {
                for (int j = 0; j < canv.Height; j += pixelBlockSize)
                {
                    ++progress;
                    int m = i / pixelBlockSize;
                    int n = j / pixelBlockSize;
                    Rectangle cutRegion = new Rectangle(i, j, pixelBlockSize, pixelBlockSize);
                    Canvas cut = canv.Cut(cutRegion);
                    BlockImageInfo info = FindBlock(Color.FromArgb(cut.AverageColor()), enabled);
                    BlockData dat = enabled.FirstOrDefault(x => x.name == info.Name);
                    blockImage.DrawImage(i * _blockSize / pixelBlockSize, j * _blockSize / pixelBlockSize, info.Image);
                    _schematic.EditBlock(m, 0, n, dat.id, dat.data);
                }
                Tick.Invoke(this, new UpdateEventArgs((progress + 0.0f) / (canv.Width * canv.Height / pixelBlockSize / pixelBlockSize)));
            }
            ConvertThreadEnd.Invoke(this, new ConvertImageEndArgs(blockImage));
            return blockImage;
        }

        /// <summary>
        /// Grabs an image and creates a schematic and another image out of it
        /// </summary>
        /// <param name="ppb">Pixels per block. 3 is a 3x3 area per block and so on</param>
        /// <param name="original">The original image you're working with</param>
        /// <returns>The image converted to minecraft blocks</returns>
        /// <remarks>Use <seealso cref="ConvertImage(Bitmap, int, int, bool)"/></remarks>
        public Canvas ConvertImage(Bitmap original, int ppb, bool maxImage)
        {
            //Initilize things
            Canvas clone = new Canvas(original); //Clones it to prevent bitlock errors
            int width = _blockPics[0].Width;
            int height = _blockPics[0].Height;
            foreach (Canvas c in _blockPics)
            {
                if (c.Width != width) throw new InvalidOperationException("Inconsistant width");
                if (c.Height != height) throw new InvalidOperationException("Inconsistant height");
            }
            _blockSize = width == height ? width : throw new InvalidOperationException("Images not square tf boi");
            Canvas blockImage = null;
            bool worked = false;
            while (!worked)
            {
                try
                {
                    blockImage = new Canvas(_blockSize * clone.Width / ppb, _blockSize * clone.Height / ppb);
                    worked = true;
                }
                catch
                {
                    if (_blockSize == 1) throw;
                    _blockSize /= 2;
                    worked = false;
                }
            }
            Canvas cut = null;
            Point coord;
            double progress = 0;
            Rectangle blockRec;
            blocksNeeded = "";
            BlockData[] enabled = _data.Enabled.ToArray();
            NbtSchematic schematic = new NbtSchematic((short)((original.Width / ppb + 1) % 65536), 1, (short)((original.Height / ppb + 1) % 65536));
            for (int i = 0; i < clone.Width; i += ppb)
            {
                for (int j = 0; j < clone.Height; j += ppb)
                {
                    ++progress;
                    int m = i / ppb;
                    int n = j / ppb;
                    coord = new Point(i, j);
                    cut = clone.Cut(coord, new Point(coord.X + ppb, coord.Y + ppb));
                    int average = cut.AverageColor();
                    BlockImageInfo block;
                    average = (int)(average | 0xff000000);
                    block = FindBlock(Color.FromArgb(average), enabled);
                    BlockData dat = enabled.FirstOrDefault(x => x.name == block.Name);
                    schematic.EditBlock(m, 0, n, dat.id, dat.data);
                    blockRec = new Rectangle(0, 0, block.Image.Width, block.Image.Height);
                    blockImage.DrawImage(i, j, block.Image);
                }

                Tick.Invoke(this, new UpdateEventArgs(progress / (original.Width * original.Height / ppb / ppb)));
            }

            ConvertThreadEnd.Invoke(this, new ConvertImageEndArgs(blockImage));
            _schematic = schematic;

            return blockImage;
        }

        public override string ToString() => string.Format("Current texture is {0}", _currentTexture);

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public void Dispose()
        {
            _blockPics = null;
            try
            {
                InterruptConvertThread();
                InterruptTextureThread();
            }
            catch
            {

            }
            _convertThread = null;
            _textureThread = null;
            GC.Collect();
        }
    }
}
