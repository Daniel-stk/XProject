using Caroto.DomainObjects;
using Caroto.Exceptions;
using DTO;
using Gateway;
using Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.Services
{
    public class VideoManagerService
    {
        private static readonly Lazy<VideoManagerService> _instance = new Lazy<VideoManagerService>(() => new VideoManagerService());
        private VideoManagerGateway _gateway;

        public static VideoManagerService Instance { get { return _instance.Value; } }

        private VideoManagerService()
        {
            _gateway = new VideoManagerGateway();
        }

        public async Task<VideoDownloadList> CreateVideoDownloadList(string apiKey,string identidad)
        {
            var dto = new AuthorizationDto() { ApiKey = apiKey, Identidad = identidad };
            var videoData = await _gateway.CreateVideoDownloadList(dto);
            if (videoData == null)
            {
                throw new NullResponseException("No hubo respuesta exitosa de parte del servidor");
            }

            if (videoData.Any())
            {
                var list = new VideoDownloadList() { Processed = false, Videos = videoData };
                return list;
            }
            else
            {
                throw new NullResponseException("Creación de lista de videos fallida");
            }
        }

        public async Task<List<VideoDataResponse>> GetVideoDownloadListData(string apiKey,string identidad)
        {
            var dto = new AuthorizationDto() { ApiKey = apiKey, Identidad = identidad };
            var videoData = await _gateway.CreateVideoDownloadList(dto);
            if (videoData == null)
            {
                throw new NullResponseException("No hubo respuesta exitosa de parte del servidor");
            }

            if (videoData.Any())
            {
                return videoData;
            }
            else
            {
                throw new NullResponseException("Creación de lista de videos fallida");
            }
        }

        public int GetCountVideosOnFolder()
        {
            if(Directory.Exists(CarotoSettings.Default.VideoFolder + @"\videos\"))
            {
                return Directory.GetFiles(CarotoSettings.Default.VideoFolder + @"\videos\", "*.mp4").Length;
            }
            return 0;
        }

        public async Task<bool> DownloadVideoFiles(string url,List<VideoDataResponse> videos)
        {
            foreach(var video in videos)
            {
                if(!Directory.Exists(CarotoSettings.Default.VideoFolder + @"\videos\"))
                {
                    Directory.CreateDirectory(CarotoSettings.Default.VideoFolder + @"\videos\");
                }
                if(!File.Exists(CarotoSettings.Default.VideoFolder + @"\videos\" + video.File + ".mp4"))
                {
                    var uri = new Uri(url + video.File);
                    var success = await _gateway.DownloadVideo(uri, CarotoSettings.Default.VideoFolder + @"\videos\" + video.File + ".mp4");
                    if (success)
                    {
                        Console.WriteLine("Descarga éxitosa - " + video.Name);
                    }
                }
                else
                {
                    Console.WriteLine("El video ya fue descargado previamente");
                }
            }
            var videosOnFolder = Directory.GetFiles(CarotoSettings.Default.VideoFolder + @"\videos\","*.mp4");
            CleanVideoFolder(videosOnFolder, videos);
            return videosOnFolder.Length == videos.Count;
        }

        private void CleanVideoFolder(string[] videosOnFolder, List<VideoDataResponse> downloadedVideos)
        {
            foreach(var downloadedVideo in downloadedVideos)
            {
                videosOnFolder = videosOnFolder.Where(video => !video.Equals(CarotoSettings.Default.VideoFolder +"\\videos\\"+ downloadedVideo.File + ".mp4") ).ToArray();
            }
            if (videosOnFolder.Any())
            {
                foreach(var videoToDelete in videosOnFolder)
                {
                    File.Delete(videoToDelete);
                }
            }
        }
    }
}
