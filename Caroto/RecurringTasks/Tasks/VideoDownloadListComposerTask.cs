using Gateway;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Caroto.RecurringTasks.Tasks
{
    public class VideoDownloadListComposerTask : RecurringTask
    {
        private static readonly Lazy<VideoDownloadListComposerTask> _instance = new Lazy<VideoDownloadListComposerTask>(() => new VideoDownloadListComposerTask());
        private VideoDownloadListComposerTask() : base(){}

        public static VideoDownloadListComposerTask Instance { get { return _instance.Value; } }

        public async Task ComposeVideoDownloadList(CancellationToken token)
        {
            if(File.Exists(CarotoSettings.Default.VideoFolder + @"\VideosToDownload.json"))
            {

            }
            else
            {

            }
        }
    }
}
