using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using fNbt;

namespace Pixelcraft_2
{
    internal class Work
    {

        private static Work _work;
        private readonly string textureLocation = @"D:\Pixelcraft\Textures\";
        private readonly string textureExtraction = @"D:\Pixelcraft\TextureExtract\";

        private NbtSchematic _schematic;

        public NbtSchematic GetSchematic()
        {
            return _schematic;
        }

        public Thread ConvertImageThread(Bitmap original, int width, int height, bool maxImage)
        {
            var t = new Thread(() => ConvertImage(original, width, height, maxImage)); //ConvertImage(ppb, original));
            t.Start();
            return t;
        }

        private string blockPath
        {
            get
            {
                string s = "";
                s += textureExtraction;
                s += currentTexture;
                s += "\\";
                s += @"assets\minecraft\textures\blocks\";

                return s;
            }
        }

        private string currentTexture = @"1.12.2";
        Bitmap[] blockPics;
        BlockData[] data;
        Color[] colors;
        public string blocksNeeded = string.Empty;
        int[] colorCodes;
        private int blockSize = 16;
        private Work()
        {
            Console.WriteLine("Getting blockdata");
            data = BlockDataCreate;
            Console.WriteLine("Loading texture");
            LoadTexture();
            Console.WriteLine("Creating thread object");
        }

        public static Work GetWork() => _work = _work == null ? new Work() : _work;

        /// <summary>
        /// Loads images from a directory that are in the blocklist
        /// </summary>
        /// <param name="directory">The directory you are loading images from</param>
        /// <returns>An array of images loaded from the directory chosen</returns>
        private Bitmap[] LoadImages(string directory)
        {
            Bitmap[] images = new Bitmap[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                Bitmap bm = Image.FromFile(directory + data[i].fileName) as Bitmap;
                images[i] = bm.Clone() as Bitmap;
            }
            return images;
        }

        public void LoadTexture()
        {
            currentTexture = @"1.12.2";
            string directory = string.Format("{0}{1}\\", textureExtraction, currentTexture);
            blockPics = new Bitmap[data.Length];
            colorCodes = new int[data.Length];
            colors = new Color[data.Length];
            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    blockPics[i] = Image.FromFile(blockPath + data[i].fileName) as Bitmap;
                    colors[i] = AverageColor(blockPics[i]);
                    colorCodes[i] = colors[i].ToArgb();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Couldn't find images;Either invalid or incomplete texture pack. Loading defualt...");
                LoadTexture(@"1.12.2");
            }
        }

        public void LoadTexture(string name)
        {
            currentTexture = name;
            string directory = "";
            directory += textureLocation;
            directory += name;
            directory += ".zip";
            if (blockPics != null)
            {
                for (int i = 0; i < blockPics.Length; i++)
                {
                    if (blockPics[i] != null)
                        blockPics[i].Dispose();
                    blockPics[i] = null;
                }
            }
            Console.WriteLine("Deleting old stuff...");
            while (Directory.Exists(textureExtraction + name + "\\"))
            {
                if (Directory.Exists(textureExtraction + name + "\\"))
                    Directory.Delete(textureExtraction + name + "\\", true);
            }
            Console.WriteLine("Finished deleting");
            Console.WriteLine("Extraction started...");
            try
            {
                ZipFile.ExtractToDirectory(directory, textureExtraction + name + "\\");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Zip file note found");
                Console.WriteLine("Reverting to defualt...");
                LoadTexture(@"1.12.2");
                return;
            }
            Console.WriteLine("Extraction ended");
            colorCodes = new int[data.Length];
            colors = new Color[data.Length];
            blockPics = new Bitmap[data.Length];
            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    blockPics[i] = Image.FromFile(blockPath + data[i].fileName) as Bitmap;
                    colors[i] = AverageColor(blockPics[i]);
                    colorCodes[i] = colors[i].ToArgb();
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Couldn't find images;Either invalid or incomplete texture pack. Loading defualt...");
                LoadTexture(@"1.12.2");
            }
        }

        /// <summary>       
        /// Cuts a section of a Bitmap out and returns it as it's own bitmap
        /// </summary>
        /// <param name="original">The original image to cut from</param>
        /// <param name="region">The region to cut</param>
        /// <returns>The cut region</returns>
        private Bitmap Cut(Bitmap original, Rectangle region)
        {
            Bitmap clone = original.Clone() as Bitmap;
            Bitmap cutImage = new Bitmap(region.Width, region.Height);
            Rectangle paste = new Rectangle(0, 0, region.Width, region.Height);
            using (Graphics g = Graphics.FromImage(cutImage))
            {
                g.DrawImage(clone, paste, region, GraphicsUnit.Pixel);
            }
            return cutImage;
        }

        //TODO: I need to test if extracting the array from the direct bitmap and discarding it would be faster, either pinning just the array to ram or not, and leaving the image out of the equation, because once you
        //have all the colors the image is useless. DirectBitmap can be used elsewhere, but not super useful here
        /// <summary>
        /// Returns the average color of a bitmap
        /// </summary>
        /// <param name="source">The bitmap to average</param>
        /// <returns>The average color of the bitmap</returns>
        /// <remarks>This function pins the image to ram to increase the speed by 750%, so careful to make sure you have enough ram and/or the image isnt too big</remarks>
        public Color AverageColor(Bitmap source)
        {
            int width = source.Width;
            int height = source.Height;
            long[] totals = new long[] { 0, 0, 0, 0 };
            long pixels = 0;
            DirectBitmap dbm = new DirectBitmap(source);
            for (int i = 0; i < width; i += 1)
            {
                for (int j = 0; j < height; j += 1)
                {
                    Color color = dbm.GetPixel(i, j);
                    totals[0] += color.A;
                    totals[1] += color.R;
                    totals[2] += color.G;
                    totals[3] += color.B;
                    pixels++;
                }
            }
            int a = (int)(totals[0] / pixels);
            int r = (int)(totals[1] / pixels);
            int g = (int)(totals[2] / pixels);
            int b = (int)(totals[3] / pixels);
            dbm.Dispose();

            return Color.FromArgb(a, r, g, b);
        }

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
        private BlockImageInfo FindBlock(Color color)
        {
            int index = -1;
            double[] error = new double[colorCodes.Length];
            for (int i = 0; i < colorCodes.Length; i++)
            {
                error[i] = 0;
                error[i] = Error(color, colors[i]);
                //error[i] += Error(color.R, colors[i].R);
                //error[i] += Error(color.G, colors[i].G);
                //error[i] += Error(color.B, colors[i].B);
            }
            for (int i = 0; i < error.Length; i++)
            {
                if (error.Min() == error[i])
                {
                    index = i;
                }
                if (index >= 0) break;
            }
            if (index == -1) index = 0;
            BlockImageInfo block = new BlockImageInfo();
            block.name = data[index].name;
            block.color = colors[index];
            block.image = blockPics[index];
            block.colorCode = colorCodes[index];
            return block;
        }

        public BlockData[] BlockDataCreate
        {
            get
            {
                List<BlockData> bd = new List<BlockData>();
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.WHITE, "White Wool", "wool_colored_white.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.ORANGE, "Orange Wool", "wool_colored_orange.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.MAGENTA, "Magenta Wool", "wool_colored_magenta.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.LIGHT_BLUE, "Light Blue Wool", "wool_colored_light_blue.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.YELLOW, "Yellow Wool", "wool_colored_yellow.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.LIGHT_GREEN, "Lime Wool", "wool_colored_lime.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.PINK, "Pink Wool", "wool_colored_pink.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.GRAY, "Gray Wool", "wool_colored_gray.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.LIGHT_GRAY, "Light Gray Wool", "wool_colored_silver.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.CYAN, "Cyan Wool", "wool_colored_cyan.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.PURPLE, "Purple Wool", "wool_colored_purple.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.BLUE, "Blue Wool", "wool_colored_blue.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.BROWN, "Brown Wool", "wool_colored_brown.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.DARK_GREEN, "Green Wool", "wool_colored_white.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.RED, "White Wool", "wool_colored_white.png"));
                bd.Add(new BlockData(BlockType.WOOL, (int)WoolColor.BLACK, "White Wool", "wool_colored_white.png"));

                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.WHITE, "White Concrete", "concrete_white.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.ORANGE, "Orange Concrete", "concrete_orange.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.MAGENTA, "Magenta Concrete", "concrete_magenta.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.LIGHT_BLUE, "Light Blue Concrete", "concrete_light_blue.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.YELLOW, "Yellow Concrete", "concrete_yellow.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.LIGHT_GREEN, "Lime Concrete", "concrete_lime.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.PINK, "Pink Concrete", "concrete_pink.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.GRAY, "Gray Concrete", "concrete_gray.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.LIGHT_GRAY, "Light Gray Concrete", "concrete_silver.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.CYAN, "Cyan Concrete", "concrete_cyan.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.PURPLE, "Purple Concrete", "concrete_purple.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.BLUE, "Blue Concrete", "concrete_blue.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.BROWN, "Brown Concrete", "concrete_brown.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.DARK_GREEN, "Green Concrete", "concrete_green.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.RED, "Red Concrete", "concrete_red.png"));
                bd.Add(new BlockData(BlockType.CONCRETE, (int)ConcreteColor.BLACK, "Black Concrete", "concrete_black.png"));

                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.WHITE, "White Concrete Powder", "concrete_powder_white.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.ORANGE, "Orange Concrete Powder", "concrete_powder_orange.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.MAGENTA, "Magenta Concrete Powder", "concrete_powder_magenta.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.LIGHT_BLUE, "Light Blue Concrete Powder", "concrete_powder_light_blue.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.YELLOW, "Yellow Concrete Powder", "concrete_powder_yellow.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.LIGHT_GREEN, "Lime Concrete Powder", "concrete_powder_lime.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.PINK, "Pink Concrete Powder", "concrete_powder_pink.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.GRAY, "Gray Concrete Powder", "concrete_powder_gray.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.LIGHT_GRAY, "Light Gray Concrete Powder", "concrete_powder_silver.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.CYAN, "Cyan Concrete Powder", "concrete_powder_cyan.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.PURPLE, "Purple Concrete Powder", "concrete_powder_purple.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.BLUE, "Blue Concrete Powder", "concrete_powder_blue.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.BROWN, "Brown Concrete Powder", "concrete_powder_brown.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.DARK_GREEN, "Green Concrete Powder", "concrete_powder_green.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.RED, "Red Concrete Powder", "concrete_powder_red.png"));
                bd.Add(new BlockData(BlockType.CONCRETE_POWDER, (int)ConcreteColor.BLACK, "Black Concrete Powder", "concrete_powder_black.png"));

                bd.Add(new BlockData(BlockType.PLANKS, (int)PlankType.OAK, "Oak Planks", "planks_oak.png"));
                bd.Add(new BlockData(BlockType.PLANKS, (int)PlankType.SPRUCE, "Spruce Planks", "planks_spruce.png"));
                bd.Add(new BlockData(BlockType.PLANKS, (int)PlankType.BIRCH, "Birch Planks", "planks_birch.png"));
                bd.Add(new BlockData(BlockType.PLANKS, (int)PlankType.JUNGLE, "Jungle Planks", "planks_jungle.png"));
                bd.Add(new BlockData(BlockType.PLANKS, (int)PlankType.DARK_OAK, "Dark Oak Planks", "planks_big_oak.png"));
                bd.Add(new BlockData(BlockType.PLANKS, (int)PlankType.ACACIA, "Acacia Planks", "planks_acacia.png"));

                bd.Add(new BlockData(BlockType.CLAY, 0, "Clay", "clay.png"));
                bd.Add(new BlockData(BlockType.SANDSTONE, (int)SandstoneType.SMOOTH_SANDSTONE, "Smooth Sandstone", "sandstone_smooth.png"));
                bd.Add(new BlockData(BlockType.RED_SANDSTONE, (int)SandstoneType.SMOOTH_SANDSTONE, "Smooth Red Sandstone", "red_sandstone_smooth.png"));

                return bd.ToArray();
            }
        }

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
                    Math.Pow(goal.R - guess.R, 2) +
                    Math.Pow(goal.G - guess.G, 2) +
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
        public Bitmap ConvertImage(Bitmap bitmap, int width, int height, bool maxImage)
        {
            if (width == 0 || height == 0)
            {
                throw new ArgumentOutOfRangeException("Width or Height cannot be zero");
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
            #region gold
            /*
            //Iteration width and height
            int iWidth = 0, iHeight = 0;
            //Graphics things
            Point paste;
            Point copy;
            //Rectangle blockRegion;
            //#region limitless
            //if(width < 0 && height < 0)
            //{
            //    bool created = false;
            //    while(!created)
            //    {
            //        try
            //        {
            //            blockImage = new Bitmap(bitmap.Width * blockSize, bitmap.Height * blockSize);
            //            iWidth = bitmap.Width;
            //            iHeight = bitmap.Height;
            //            created = true;
            //        }
            //        catch
            //        {
            //            created = false;
            //            if (blockSize == 1) throw new ArgumentOutOfRangeException("The image provided is too large; Please lower the resolution and try again");
            //            blockSize /= 2;
            //        }
            //    }
            //}
            //#endregion
            //#region heightLocked
            //if (width < 0 && height > 0)
            //{
            //    bool created = false;
            //    while(!created)
            //    {
            //        try
            //        {
            //            int ppb = bitmap.Height / height;
            //            int newWidth = bitmap.Width / ppb;
            //            iWidth = newWidth;
            //            iHeight = height;
            //            blockImage = new Bitmap(newWidth * blockSize, height * blockSize);
            //            created = true;
            //        }
            //        catch
            //        {
            //            created = false;
            //            if (blockSize == 1) throw new ArgumentOutOfRangeException("The image provided is too large; Please lower the resolution and try again");
            //            blockSize /= 2;
            //        }
            //    }
            //}

            //#endregion
            //#region widthLocked
            //if (width > 0 && height < 0)
            //{
            //    bool created = false;
            //    while (!created)
            //    {
            //        try
            //        {
            //            int ppb = bitmap.Width / width;
            //            int newHeight = bitmap.Height / ppb;
            //            iWidth = width;
            //            iHeight = newHeight;
            //            blockImage = new Bitmap(width * blockSize, newHeight * blockSize);
            //            created = true;
            //        }
            //        catch
            //        {
            //            created = false;
            //            if (blockSize == 1) throw new ArgumentOutOfRangeException("The image provided is too large; Please lower the resolution and try again");
            //            blockSize /= 2;
            //        }
            //    }
            //}
            //#endregion
            //#region locked
            //if (width > 0 && height > 0)
            //{
            //    bool created = false;
            //    while(!created)
            //    {
            //        try
            //        {
            //            blockImage = new Bitmap(width * blockSize, height * blockSize);
            //            iWidth = width;
            //            iHeight = height;
            //            created = true;
            //        }
            //        catch
            //        {
            //            created = false;
            //            if (blockSize == 1) throw new ArgumentOutOfRangeException("The image provided is too large; Please lower the resolution and try again");
            //            blockSize /= 2;
            //        }
            //    }
            //}
            //#endregion
            //int progress = 0;
            //int wppb = bitmap.Width / iWidth;
            //int hppb = bitmap.Height / iHeight;
            //_schematic = new NbtSchematic((short)iWidth, 1, (short)iHeight);
            //for(int i = 0; i < iWidth; i++)
            //{
            //    for (int j = 0; j < iHeight; j++)
            //    {
            //        progress++;
            //        copy = new Point(i * wppb, j * hppb);
            //        paste = new Point(i * blockSize, j * blockSize);
            //        Color average = AverageColor(Cut(clone, new Rectangle(copy, new Size(wppb, hppb))));
            //        BlockImageInfo blockImageInfo;
            //        average = Color.FromArgb(255, average);
            //        blockImageInfo = FindBlock(average);
            //        BlockData blockData = data.First(x => x.name == blockImageInfo.name);
            //        _schematic.EditBlock(i, 0, j, blockData.id, blockData.data);
            //        blockRegion = new Rectangle(0, 0, blockImageInfo.image.Width, blockImageInfo.image.Height);
            //        Rectangle pasteRec = new Rectangle(paste.X * blockSize, paste.Y * blockSize, blockSize, blockSize);
            //        CopyRegionIntoImage(blockImageInfo.image.Clone() as Bitmap, blockRegion, ref blockImage, pasteRec);
            //    }
            //    Tick.Invoke(null, new UpdateEventArgs(progress / (iWidth * iHeight)));
            //}
            //clone.Dispose();
            //ThreadEnd.Invoke(null, new ConvertImageEndArgs(blockImage));
            //return blockImage;
            */
            #endregion
        }

        /// <summary>
        /// Grabs an image and creates a schematic and another image out of it
        /// </summary>
        /// <param name="ppb">Pixels per block. 3 is a 3x3 area per block and so on</param>
        /// <param name="original">The original image you're working with</param>
        /// <returns>The image converted to minecraft blocks</returns>
        /// <remarks>Use <seealso cref="ConvertImage(Bitmap, int, int, bool)"/></remarks>
        public Bitmap ConvertImage(Bitmap original, int ppb, bool maxImage)
        {
            //Initilize things
            blockSize = maxImage ? 256 : 16;
            Bitmap clone = original.Clone() as Bitmap; //Clones it to prevent bitlock errors
            Bitmap blockImage = null;
            bool worked = false;
            while (!worked)
            {
                try
                {
                    blockImage = new Bitmap((int)(original.Width * ((double)blockSize / ppb)), (int)(original.Height * ((double)blockSize / ppb)));
                    worked = true;
                }
                catch
                {
                    if (blockSize == 1) throw;
                    blockSize /= 2;
                    worked = false;
                }
            }
            Bitmap cut = null;
            Point coord;
            double progress = 0;
            Rectangle blockRec;
            blocksNeeded = "";
            NbtSchematic schematic = new NbtSchematic((short)((original.Width / ppb + 1) % 65536), 1, (short)((original.Height / ppb + 1) % 65536));
            for (int i = 0; i < original.Width; i += ppb)
            {
                for (int j = 0; j < original.Height; j += ppb)
                {
                    progress++;
                    int m = i / ppb;
                    int n = j / ppb;
                    coord = new Point(i, j);
                    cut = Cut(clone, new Rectangle(coord, new Size(ppb, ppb)));
                    Color average = AverageColor(cut);
                    BlockImageInfo block;
                    average = Color.FromArgb(255, average);
                    block = FindBlock(average);
                    BlockData dat = data.First(x => x.name == block.name);
                    schematic.EditBlock(m, 0, n, dat.id, dat.data);
                    blockRec = new Rectangle(0, 0, block.image.Width, block.image.Height);
                    CopyRegionIntoImage(block.image.Clone() as Bitmap, blockRec, ref blockImage, new Rectangle(coord.X * (blockSize / ppb), coord.Y * (blockSize / ppb), blockSize, blockSize));
                }

                Tick.Invoke(null, new UpdateEventArgs(progress / (original.Width * original.Height / ppb / ppb)));
            }

            clone.Dispose();
            cut.Dispose();

            ThreadEnd.Invoke(null, new ConvertImageEndArgs(blockImage));
            _schematic = schematic;

            return blockImage;
        }

        public override string ToString() => base.ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public event TickProgress Tick;

        public event ConvertImageEnd ThreadEnd;
    }

    public delegate void TickProgress(object source, UpdateEventArgs e);

    public delegate void ConvertImageEnd(object source, ConvertImageEndArgs e);

    public class UpdateEventArgs : EventArgs
    {
        private double _percent;
        public UpdateEventArgs(double percent) => _percent = percent;
        public double Percent => _percent;
    }

    public class ConvertImageEndArgs : EventArgs
    {
        private Bitmap image;
        public ConvertImageEndArgs(Bitmap image) => this.image = image;
        public Bitmap GetBitmap() => image;
    }
}
