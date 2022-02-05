using Newtonsoft.Json;

namespace APITesting_RestSharp.Framework.Models
{
    public class AuthenticationData
    {
        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "user")]
        public UserData User { get; set; }
    }
}
