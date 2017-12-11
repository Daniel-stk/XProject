using Caroto.Services;
using Caroto.Tools;
using Gateway;
using Responses;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Caroto.RecurringTasks.Tasks
{
    public class VideoDownloadListComposerTask : RecurringTask
    {
        private static readonly Lazy<VideoDownloadListComposerTask> _instance = new Lazy<VideoDownloadListComposerTask>(() => new VideoDownloadListComposerTask());
        private VideoManagerService _service;

        private VideoDownloadListComposerTask() : base()
        {
            _service = VideoManagerService.Instance;
        }

        public static VideoDownloadListComposerTask Instance { get { return _instance.Value; } }

        public async Task ComposeVideoDownloadList(CancellationToken token)
        {
            if(File.Exists(CarotoSettings.Default.VideoFolder + @"\VideosToDownload.json"))
            {

            }
            else
            {
                try
                {
                    Console.WriteLine("Creando lista de videos a descargar");
                    var list = await _service.CreateVideoDownloadList(Properties.Settings.Default.ApiKey,Properties.Settings.Default.Identidad);
                    JsonFileHandler.WriteJsonFile(CarotoSettings.Default.VideoFolder + @"\VideosToDownload.json",list);
                    Console.WriteLine("Lista de videos creada");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
