using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pixelcraft_2
{
    internal partial class Work
    {
        internal Thread _convertThread;
        internal Thread _textureThread;

        #region Convert Thread

        public void ConvertImageThread(Bitmap original, int width, int height)
        {
            InterruptConvertThread();
            _convertThread = new Thread(() => Convert(original, width, height));
            _convertThread.Start();
        }

        public void InterruptConvertThread()
        {
            try
            {
                if (_convertThread != null && _convertThread.IsAlive) _convertThread.Abort();
            }
            catch
            {

            }
        }

        public event ConvertImageEnd ConvertThreadEnd;

        #endregion

        #region Texture Thread

        public void LoadTextureThread(string directory)
        {
            InterruptTextureThread();
            _textureThread = new Thread(() => LoadTexture(directory));
            _textureThread.Start();
        }

        public void InterruptTextureThread()
        {
            try
            {
                if (_textureThread != null && _textureThread.IsAlive) _textureThread.Abort();
            }
            catch
            {

            }
        }

        public event LoadTextureEnd TextureThreadEnd;

        #endregion

        public event TickProgress Tick;
    }
}
