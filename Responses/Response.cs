using Newtonsoft.Json;

namespace Responses
{
    public class Response
    {
        [JsonProperty("error")]
        public int Error { get; set; }
        [JsonProperty("no_response")]
        public int NoResponse { get; set; }
        [JsonProperty("success")]
        public int Success { get; set; }
        [JsonProperty("msg")]
        public string Message { get; set; }
    }
}
