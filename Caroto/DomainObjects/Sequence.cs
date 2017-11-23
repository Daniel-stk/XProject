using Caroto.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroto.DomainObjects
{
    public class Sequence
    {
        [JsonProperty("playlist")]
        public List<string> PlayList { get; set; }
        [JsonProperty("time_to_play")]
        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTime TimeToPlay { get; set; }
        [JsonProperty("total_duration_in_seconds")]
        public string TotalDurationInSeconds { get; set; } 
    }
}
