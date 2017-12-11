using Caroto.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responses
{
    public class ProgrammingResponse
    {
        [JsonProperty("secuencia_id")]
        public int SequenceId { get; set; }

        [JsonProperty("fecha_comienzo")]
        [JsonConverter(typeof(DateConverter))]
        public DateTime StartDate { get; set; }

        [JsonProperty("comienzo")]
        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTime Start { get; set; }

        [JsonProperty("fin")]
        [JsonConverter(typeof(TimeJsonConverter))]
        public DateTime End { get; set; }

        [JsonProperty("dia")]
        public int Day { get; set; }

        [JsonProperty("repetir")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Loop { get; set; }

        [JsonProperty("secuencia")]
        public SequenceResponse Sequence { get; set; }

    }
}
