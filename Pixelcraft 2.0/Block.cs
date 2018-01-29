using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixelcraft_2
{
    internal struct BlockImageInfo
    {
        public Bitmap image { get; set; }
        public Color color { get; set; }
        public string name { get; set; }
        public int colorCode { get; set; }
    }
}
