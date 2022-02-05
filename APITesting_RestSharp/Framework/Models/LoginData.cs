using Newtonsoft.Json;

namespace APITesting_RestSharp.Framework.Models
{
    public class LoginData
    {
        [JsonProperty(PropertyName = "email")]
        public string Email;

        [JsonProperty(PropertyName = "password")]
        public string Password;
    }
}
