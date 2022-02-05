using Newtonsoft.Json;

namespace APITesting_RestSharp.Framework.Models
{
    public class Geo
    {
        [JsonProperty(PropertyName = "lat")]
        public string Lat { get; set; }
        [JsonProperty(PropertyName = "lng")]
        public string Lng { get; set; }
    }
}


