using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.EventHandlers
{
    public class VideoDownloadEventArgs : EventArgs
    {
        private int _videoCount;

        public VideoDownloadEventArgs(int videoCount)
        {
            _videoCount = videoCount;
        }

        public int VideoCount { get { return _videoCount; } }
    }
}
