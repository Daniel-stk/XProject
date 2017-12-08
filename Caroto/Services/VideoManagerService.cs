using Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.Services
{
    public class VideoManagerService
    {
        private static readonly Lazy<VideoManagerService> _instance = new Lazy<VideoManagerService>(() => new VideoManagerService());

        public static VideoManagerService Instance { get { return _instance.Value; } }

        private VideoManagerService() { }

        public async Task<List<VideoDataResponse>> CreateVideoDownloadList()
        {
            return null;
        }

        public async Task<List<VideoDataResponse>> UpdateVideoDownloadList()
        {
            return null;
        }
    }
}
