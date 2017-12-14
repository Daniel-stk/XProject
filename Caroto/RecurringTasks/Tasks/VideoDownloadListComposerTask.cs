using Caroto.DomainObjects;
using Caroto.Services;
using Caroto.Tools;
using Gateway;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Responses;
using System;
using System.IO;
using System.Security.Cryptography;
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
                try
                {
                    var videoDownloadServer = await _service.GetVideoDownloadListData(Properties.Settings.Default.ApiKey, Properties.Settings.Default.Identidad);
                    var videoDownloadLocal = JsonFileHandler.ReadJsonFile<VideoDownloadList>(CarotoSettings.Default.VideoFolder + @"\VideosToDownload.json");
                    var local = JArray.FromObject(videoDownloadLocal.Videos);
                    var server = JArray.FromObject(videoDownloadServer);
                    if (IsVideoListUpdated(local.ToString(), server.ToString()))
                    {
                        Console.WriteLine("Actualizando lista de videos a descargar");
                        var list = await _service.CreateVideoDownloadList(Properties.Settings.Default.ApiKey, Properties.Settings.Default.Identidad);
                        JsonFileHandler.WriteJsonFile(CarotoSettings.Default.VideoFolder + @"\VideosToDownload.json", list);
                        Console.WriteLine("Actualizando de videos creada");
                    }
                }
                catch(Exception ex)
                {
#if DEBUG
                    FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
                }
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
#if DEBUG
                    FileLogger.Instance.Log("Origen -" + GetType().ToString() + " Tipo - " + ex.GetType().ToString() + "Mensaje - " + ex.Message + " Fecha - " + DateTime.Now.ToString(), LogType.Error);
#endif
                }
            }
        }

        private bool IsVideoListUpdated(string local,string server)
        {
            using (var md5 = MD5.Create())
            {
                var localBytes = System.Text.Encoding.ASCII.GetBytes(local.ToString());
                var serverBytes = System.Text.Encoding.ASCII.GetBytes(server.ToString());
                var localHash = md5.ComputeHash(localBytes);
                var serverHash = md5.ComputeHash(serverBytes);
                var localHashString = BitConverter.ToString(localHash).Replace("-", "").ToLowerInvariant();
                var serverHashString = BitConverter.ToString(serverHash).Replace("-", "").ToLowerInvariant();

                return localHashString.CompareTo(serverHashString) != 0;                
            }
        }
    }
}
