using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responses
{
    public class VideoDataResponse
    {
        [JsonProperty("video_id")]
        public int VideoId { get; set; }

        [JsonProperty("nombre")]
        public string Name { get; set; }
        
        [JsonProperty("archivo")]
        public string File { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty("duracion")]
        public float Duration { get; set; }
    }
}
