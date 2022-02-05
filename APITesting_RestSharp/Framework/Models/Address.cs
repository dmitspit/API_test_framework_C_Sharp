using Newtonsoft.Json;

namespace APITesting_RestSharp.Framework.Models
{
    public class Address
    {
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "suite")]
        public string Suite { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty(PropertyName = "geo")]
        public Geo Geo { get; set; }
    }
}


