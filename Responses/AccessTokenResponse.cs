using Newtonsoft.Json;

namespace Responses
{
    public class AccessTokenResponse
    {  
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
