using Newtonsoft.Json;

namespace APITesting_RestSharp.Framework.Models
{
    public class UserData 
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}