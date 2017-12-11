using Caroto.Tools;
using Newtonsoft.Json;
using Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.DomainObjects
{
    public class VideoDownloadList
    {
        [JsonProperty("processed")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Processed { get; set; }

        [JsonProperty("videos")]
        public List<VideoDataResponse> Videos { get; set; }
    }
}
