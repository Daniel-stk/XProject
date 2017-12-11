using Newtonsoft.Json;
using System.Collections.Generic;

namespace Responses
{
    public class SequenceResponse
    {
        [JsonProperty("nombre")]
        public string Name { get; set; }

        [JsonProperty("videos")]
        public List<VideoDataResponse> Videos { get; set; }
    }
}