using DTO;
using Gateway.Exceptions;
using Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Responses.Tools.ResponseExtensions;

namespace Gateway
{
    public class VideoManagerGateway
    {
        private HttpRequestSender _request;
        private WebClient _downloader;
        
        public VideoManagerGateway()
        {
            _request = HttpRequestSender.Instance;
        }

        public async Task<List<VideoDataResponse>> CreateVideoDownloadList(AuthorizationDto dto)
        {
            var response = await _request.GetAsync<VideoDataResponse>("Caroto/API/CrearListaDeVideos", dto,ResponseType.complex);
            if(response != null)
            {
                var complexResponse = response.ConvertToComplexResponse<VideoDataResponse>();
                if (complexResponse.NoResponse == 1)
                {
                    throw new NoResponseException(complexResponse.Message);
                }
                else if (complexResponse.Error == 1)
                {
                    throw new ErrorResponseException(complexResponse.Message);
                }
                else if (complexResponse.Success == 1)
                {
                    return complexResponse.Data;
                }
            }

            return null;
        }

        public async Task<bool> DownloadVideo(Uri uri, string video)
        {
            using (_downloader = new WebClient())
            {
                try
                {
                    await _downloader.DownloadFileTaskAsync(uri, video);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Descarga fallida: Archivo - " + video+ " Exception - "+ex.Message );
                    return false;
                }
            }
            return true;
        }
    }
}
