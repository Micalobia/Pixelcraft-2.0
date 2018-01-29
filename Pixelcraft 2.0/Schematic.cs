using fNbt;

namespace Pixelcraft_2
{
    /// <summary>
    /// Represents a schematic file
    /// </summary>
    struct NbtSchematic
    {
        /// <summary>
        /// The width of the schematic
        /// </summary>
        public NbtShort Width { get; private set; }
        /// <summary>
        /// The height of the schematic
        /// </summary>
        public NbtShort Height { get; private set; }
        /// <summary>
        /// The length of the schematic
        /// </summary>
        public NbtShort Length { get; private set; }
        /// <summary>
        /// Used so it doesn't throw an error when used in Minecraft, otherwise unused
        /// </summary>
        private NbtString Materials { get; set; }
        /// <summary>
        /// The list of block ids the schematic contains
        /// </summary>
        public NbtByteArray Blocks { get; private set; }
        /// <summary>
        /// The list of data values for each block the schematic contains
        /// </summary>
        public NbtByteArray Data { get; private set; }
        /// <summary>
        /// A list of tile entities, used for compatablity
        /// </summary>
        private NbtList TileEntities { get; set; }

        /// <summary>
        /// Creates a new schematic of a specified size
        /// </summary>
        /// <param name="Width">The width of the schematic</param>
        /// <param name="Height">The height of the schematic</param>
        /// <param name="Length">The length of the schematic</param>
        public NbtSchematic(short Width, short Height, short Length)
        {
            this.Width = new NbtShort("Width", Width);
            this.Height = new NbtShort("Height", Height);
            this.Length = new NbtShort("Length", Length);
            Materials = new NbtString("Materials", "Alpha");
            Blocks = new NbtByteArray("Blocks", new byte[Width * Height * Length]);
            Data = new NbtByteArray("Data", new byte[Width * Height * Length]);
            TileEntities = new NbtList("TileEntities", NbtTagType.Compound);
        }

        /// <summary>
        /// Exports the schematic as a .schematic file
        /// </summary>
        /// <param name="fileName">The file location and file name of the .schematic you want to save</param>
        public void Export(string fileName)
        {
            string real = "";
            if (fileName.EndsWith(".schematic")) real = fileName;
            else real = fileName + ".schematic";
            NbtFile file = new NbtFile(_returnCompound());
            file.SaveToFile(real, NbtCompression.GZip);
        }

        /// <summary>
        /// Returns the NbtCompound of the schematic, needed for saving the file.
        /// </summary>
        /// <returns>The NbtCompound of the current schematic</returns>
        private NbtCompound _returnCompound()
        {
            NbtCompound compound = new NbtCompound("Schematic");
            compound.Add(Width);
            compound.Add(Height);
            compound.Add(Length);
            compound.Add(Materials);
            compound.Add(Blocks);
            compound.Add(Data);
            compound.Add(TileEntities);
            return compound;
        }

        /// <summary>
        /// Edits a block at the relative location given, using the given block id and data value
        /// </summary>
        /// <param name="x">The x coordinate of the block</param>
        /// <param name="y">The y coordinate of the block</param>
        /// <param name="z">The z coordinate of the block</param>
        /// <param name="id">The id of the block to be placed</param>
        /// <param name="data">The data value (also known as damage value) of the block to be placed</param>
        public void EditBlock(int x, int y, int z, byte id, byte data)
        {
            int index = (y * Length.Value + z) * Width.Value + x;
            Blocks.Value[index] = id;
            Data.Value[index] = data;
        }
    }
}
