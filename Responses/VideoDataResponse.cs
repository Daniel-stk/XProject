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

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }
}
