using Caroto.DomainObjects;
using Caroto.Services;
using Caroto.Tools;
using Gateway;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Caroto.RecurringTasks.Tasks
{
    public class VideoDownloadTask : RecurringTask
    {
        private static readonly Lazy<VideoDownloadTask> _instance = new Lazy<VideoDownloadTask>(() => new VideoDownloadTask());
        private AuthorizationService _authService;
        private VideoManagerService _videoService;
        private VideoDownloadTask():base()
        {
            _authService = AuthorizationService.Instance;
            _videoService = VideoManagerService.Instance;       
        }

        public static VideoDownloadTask Instance { get { return _instance.Value; } }

        public async Task DownloadVideos(CancellationToken token)
        {
            if(File.Exists(CarotoSettings.Default.VideoFolder + @"\VideosToDownload.json"))
            {
                try
                { 
                    var videosToDownloadList = JsonFileHandler.ReadJsonFile<VideoDownloadList>(CarotoSettings.Default.VideoFolder + @"\VideosToDownload.json");
                    if (!videosToDownloadList.Processed)
                    {
                        Console.WriteLine("Iniciando descarga de videos");
                        var folder = await _authService.ServerFolder(Properties.Settings.Default.ApiKey, Properties.Settings.Default.Identidad);
                        var success = await _videoService.DownloadVideoFiles(CarotoSettings.Default.BaseAddress + @"/data/" + folder + @"/videos/",videosToDownloadList.Videos);
                        videosToDownloadList.Processed = success;
                        JsonFileHandler.WriteJsonFile(CarotoSettings.Default.VideoFolder + @"\VideosToDownload.json", videosToDownloadList);
                        await _bufferBlock.SendAsync("Download Operation Done");
                        Console.WriteLine("Fin descarga de videos");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Lista de videos a descargar aun no esta generada");
            }
        }
    }
}
