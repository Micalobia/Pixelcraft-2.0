using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixelcraft_2
{
    internal struct BlockData
    {
        public byte id { get; set; }
        public byte data { get; set; }
        public string name { get; set; }
        public string fileName { get; set; }
        public BlockData(byte id, byte data, string name, string fileName)
        {
            this.id = id;
            this.data = data;
            this.name = name;
            this.fileName = fileName;
        }
    }
}
