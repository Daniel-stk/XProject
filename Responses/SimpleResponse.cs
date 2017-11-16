using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responses
{
    public class SimpleResponse<T> : Response
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
