using Newtonsoft.Json;
using System.Collections.Generic;

namespace Responses
{
    public class ComplexResponse<T> : Response
    {
        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
